using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUB {
    public class TenantBilling {
        int parkId, parkSpaceId, parkNumber;
        String spaceId;
        Object[] meterRead;

        private static int minTier = 1;
        private static int maxTier = 5;

        public TenantBilling(int parkId, int parkSpaceId) {
            this.parkId = parkId;
            this.parkSpaceId = parkSpaceId;
            this.parkNumber = (int)DatabaseControl.getSingleRecord(new String[] { "ParkNumber" }, DatabaseControl.parkTable, "ParkID=@value0",
            new Object[] { parkId })[0];
            this.spaceId = DatabaseControl.getSingleRecord(new String[] {"SpaceID"}, DatabaseControl.spaceTable, "ParkID=@value0 AND ParkSpaceID=@value1",
            new Object[] { parkId, parkSpaceId })[0].ToString();

            ArrayList reads = DatabaseControl.getMultipleRecord(DatabaseControl.meterReadsColumns, DatabaseControl.meterReadsTable,
                "ParkID=@value0 AND SpaceID=@value1 ORDER BY MeterReadID DESC", new Object[] { parkId, spaceId });
            meterRead = (Object[])reads[0];
        }

        public TenantBilling(int parkId, int parkSpaceId, Object[] currRead) {
            this.parkId = parkId;
            this.parkSpaceId = parkSpaceId;
            meterRead = currRead;
        }

        public void generateBill() {
            Object[] tenant = getTenantAddr();
            Object[] park = getParkAddr();
            Object[] ele = calculateEleTotalCost();
            Object[] gas = calculateGasTotalCost();
            Object[] wat = calculateWatTotalCost();
            Object[] usage = getReadData(0.0M, (decimal)ele[ele.Length-1], 0.0M);
            PdfControl.createBillPdf(parkNumber, spaceId, tenant, park, usage, gas, ele, wat);
        }

        public Object[] getTenantAddr() {
            String[] fields = { "Tenant", "Address1", "Address2", "City", "State", "Zip" };
            String condition = "ParkSpaceID=@value0";
            return DatabaseControl.getSingleRecord(fields,DatabaseControl.spaceTable, condition, new Object[] { this.parkSpaceId });
        }

        public Object[] getParkAddr() {
            String[] fields = { "ParkName", "Address", "City", "State", "ZipCode" };
            String condition = "ParkID=@value0";
            return DatabaseControl.getSingleRecord(fields, DatabaseControl.parkTable, condition, new Object[] { this.parkId });
        }

        public Object[] getReadData(decimal gasTotal, decimal eleTotal, decimal watTotal) {
            Object[] usages = new Object[] { null, (int)meterRead[3] - (int)meterRead[6], meterRead[9],
                                             null, (int)meterRead[4] - (int)meterRead[7], meterRead[10],
                                             null, (int)meterRead[5] - (int)meterRead[8], meterRead[11] };
            Object[] gas = new Object[] { "GAS", ((DateTime)meterRead[2]).ToString("MM/dd"), ((DateTime)meterRead[2]).ToString("MM/dd"), meterRead[6], meterRead[3], null,
                (int)meterRead[3] - (int)meterRead[6], gasTotal.ToString("C2") };
            Object[] ele = new Object[] { "ELE", ((DateTime)meterRead[2]).ToString("MM/dd"), ((DateTime)meterRead[2]).ToString("MM/dd"), meterRead[7], meterRead[4], null,
                (int)meterRead[4] - (int)meterRead[7], eleTotal.ToString("C2") };
            Object[] wat = new Object[] { "WAT", ((DateTime)meterRead[2]).ToString("MM/dd"), ((DateTime)meterRead[2]).ToString("MM/dd"), meterRead[8], meterRead[5], null,
                (int)meterRead[5] - (int)meterRead[8], watTotal.ToString("C2") };
            return new Object[] { usages, gas, ele, wat };
        }

        public Object[] calculateGasTotalCost() {
            int utilRateId, tierSetId, numDays, numMed;
            char season, zone, serviceType;
            String rateType;
            decimal usage, baseline, medAllowance;

            int gasCompanyId = (int)DatabaseControl.getSingleRecord(new String[] { "GasCompanyID" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0];

            Object[] utilCompany = DatabaseControl.getSingleRecord(new String[] { "UtilityRateID", "HasWinter", "WinterStartDate", "WinterEndDate", "TierSetID" }, DatabaseControl.utilRateTable,
                "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { gasCompanyId });

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[4];
            if ((bool)utilCompany[1]) {
                DateTime winterStart = (DateTime)utilCompany[1];
                DateTime winterEnd = (DateTime)utilCompany[2];
                if (winterStart < DateTime.Today && winterEnd > DateTime.Today) { season = 'W'; }
                else { season = 'S'; }
            } else {
                season = ' ';
            }
            zone = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "GasZone" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0]);
            rateType = DatabaseControl.getSingleRecord(new String[] { "GasStatus" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0].ToString();
            if (rateType == "") { rateType = "Regular"; }
            serviceType = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "GasType" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0]);
            numMed = (int)DatabaseControl.getSingleRecord(new String[] { "MedStatus" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];

            usage = Convert.ToDecimal(meterRead[1]);
            numDays = (int)(((DateTime)meterRead[2]).Date - ((DateTime)meterRead[2]).Date).TotalDays;
            baseline = getBaselineAllowance(utilRateId, season, serviceType, zone);
            medAllowance = getBaselineAllowance(utilRateId, 'M', 'M', 'M');

            Object[] custCharge = calculateBasicCost(utilRateId, serviceType, rateType, numDays);

            List<Object[]> generation = calculateTierCost(utilRateId, 'G', rateType, ' ', tierSetId, usage, baseline * numDays + numMed * medAllowance);
            decimal generationTotal = 0.0M;
            foreach (Object[] item in generation) {
                generationTotal += (decimal)item[1];
            }

            List<Object[]> surcharge = calculateSurcharges(utilRateId, "All", numDays, usage);
            surcharge.AddRange(calculateSurcharges(utilRateId, rateType, numDays, usage));
            decimal surchargeTotal = 0.0M;
            foreach (Object[] item in surcharge) {
                surchargeTotal += (decimal)item[1];
            }

            List<Object[]> tax = calculateTaxCost('G', new String[] { "LocalTax", "CountyTax", "StateTax", "RegTax" }, usage);
            decimal taxTotal = 0.0M;
            foreach (Object[] item in tax) {
                taxTotal += (decimal)item[1];
            }

            String careRates;
            if (numMed > 0) { careRates = "CARE RATES APPLIED"; } else { careRates = ""; }
            Object[] pdfGas = new Object[] { (baseline * numDays + numMed * medAllowance), custCharge, generation, generationTotal, 
                surcharge, tax, careRates, (decimal)custCharge[1] + generationTotal + surchargeTotal + taxTotal };

            return pdfGas;
        }

        public Object[] calculateEleTotalCost() {
            int utilRateId, tierSetId, numDays, usage, numMed; char serviceType, season, zone; String rateType; decimal baseline, medAllowance;

            int eleCompanyId = (int)DatabaseControl.getSingleRecord(new String[] { "ElectricityCompanyID" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0];

            Object[] utilCompany = DatabaseControl.getSingleRecord(new String[] { "UtilityRateID", "HasWinter", "WinterStartDate", "WinterEndDate", "TierSetID" }, DatabaseControl.utilRateTable,
                "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { eleCompanyId });

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[4];
            if ((bool)utilCompany[1]) {
                DateTime winterStart = (DateTime)utilCompany[2];
                DateTime winterEnd = (DateTime)utilCompany[3];
                if (winterStart < DateTime.Today && winterEnd > DateTime.Today) { season = 'W'; }
                else { season = 'S'; }
            } else {
                season = ' ';
            }

            zone = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "EleZone" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0]);
            rateType = DatabaseControl.getSingleRecord(new String[] { "EleStatus" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0].ToString();
            if (rateType == "") { rateType = "Regular"; }
            serviceType = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "EleType" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0]);
            numMed = (int)DatabaseControl.getSingleRecord(new String[] { "MedStatus" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];

            usage = (int)meterRead[4] - (int)meterRead[7];
            numDays = (int)(((DateTime)meterRead[2]).Date - ((DateTime)meterRead[2]).Date).TotalDays + 1;
            baseline = getBaselineAllowance(utilRateId, season, serviceType, zone);
            medAllowance = getBaselineAllowance(utilRateId, 'M', 'M', 'M');

            Console.WriteLine("Electric  \r\nUsage:{0}  Days:{1}  Baseline:{2}  MedAllowance:{3}", usage, numDays,
                (baseline * numDays + numMed * medAllowance).ToString("G29"), numMed);
            Console.WriteLine("Status: {0}   Zone: {1}   Season: {2}  Service: {3}", rateType, zone, season, serviceType);
            Console.WriteLine("PrevDate: {0}   CurrDate: {1}   PrevRead: {2}   CurrRead: {3}\r\n", ((DateTime)meterRead[2]).Date.ToShortDateString(), ((DateTime)meterRead[2]).Date.ToShortDateString(),
                meterRead[4], meterRead[7]);

            Object[] custCharge = calculateBasicCost(utilRateId, serviceType, rateType, numDays);

            List<Object[]> delivery = calculateTierCost(utilRateId, 'D', rateType, season, tierSetId, usage, baseline * numDays + numMed * medAllowance);
            decimal deliveryTotal = 0.0M;
            foreach (Object[] item in delivery) {
                deliveryTotal += (decimal)item[1];
            }

            List<Object[]> generation = calculateTierCost(utilRateId, 'G', rateType, season, tierSetId, usage, baseline * numDays + numMed * medAllowance);
            decimal generationTotal = 0.0M;
            foreach (Object[] item in generation) {
                generationTotal += (decimal)item[1];
            }

            List<Object[]> surcharge = calculateSurcharges(utilRateId, "All", numDays, usage);
            surcharge.AddRange(calculateSurcharges(utilRateId, rateType, numDays, usage));
            decimal surchargeTotal = 0.0M;
            foreach (Object[] item in surcharge) {
                surchargeTotal += (decimal)item[1];
            }

            List<Object[]> tax = calculateTaxCost('E', new String[] { "LocalTax", "CountyTax", "StateTax", "RegTax" }, usage);
            decimal taxTotal = 0.0M;
            foreach (Object[] item in tax) {
                taxTotal += (decimal)item[1];
            }

            String careRates;
            if (numMed > 0) { careRates = "CARE RATES APPLIED"; } else { careRates = ""; }
            Object[] pdfEle = new Object[] { (baseline * numDays + numMed * medAllowance), custCharge, delivery, deliveryTotal, generation, generationTotal, 
                surcharge, tax, careRates, (decimal)custCharge[1] + deliveryTotal + generationTotal + surchargeTotal + taxTotal };
            return pdfEle;
        }

        public Object[] calculateWatTotalCost() {
            int utilRateId, tierSetId, numDays, usage, numMed; char serviceType, season, zone; String rateType; decimal baseline, medAllowance;

            int watCompanyId = (int)DatabaseControl.getSingleRecord(new String[] { "WaterCompanyID" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0];

            Object[] utilCompany = DatabaseControl.getSingleRecord(new String[] { "UtilityRateID", "TierSetID" }, DatabaseControl.utilRateTable,
                "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { watCompanyId });

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[1];
            season = ' ';

            rateType = DatabaseControl.getSingleRecord(new String[] { "WatStatus" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0].ToString();
            if (rateType == "") { rateType = "Regular"; }
            serviceType = 'W';
            numMed = (int)DatabaseControl.getSingleRecord(new String[] { "MedStatus" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];

            usage = (int)meterRead[4] - (int)meterRead[7];
            numDays = (int)(((DateTime)meterRead[2]).Date - ((DateTime)meterRead[2]).Date).TotalDays + 1;
            baseline = getBaselineAllowance(utilRateId, 'S', serviceType, ' ');

            Object[] custCharge = calculateBasicCost(utilRateId, serviceType, rateType, numDays);

            List<Object[]> generation = calculateTierCost(utilRateId, 'G', rateType, season, tierSetId, usage, baseline * numDays);
            decimal generationTotal = 0.0M;
            foreach (Object[] item in generation) {
                generationTotal += (decimal)item[1];
            }

            List<Object[]> surcharge = calculateSurcharges(utilRateId, "All", numDays, usage);
            surcharge.AddRange(calculateSurcharges(utilRateId, rateType, numDays, usage));
            decimal surchargeTotal = 0.0M;
            foreach (Object[] item in surcharge) {
                surchargeTotal += (decimal)item[1];
            }

            List<Object[]> tax = calculateTaxCost('E', new String[] { "LocalTax", "CountyTax", "StateTax", "RegTax" }, usage);
            decimal taxTotal = 0.0M;
            foreach (Object[] item in tax) {
                taxTotal += (decimal)item[1];
            }

            Object[] pdfWat = new Object[] { baseline * numDays, custCharge, generation, generationTotal, surcharge, tax, (decimal)custCharge[1] + generationTotal + surchargeTotal + taxTotal };
            return pdfWat;
        }

        public Object[] calculateBasicCost(int utilRateId, char serviceType, String rateType, int numDays) {
            decimal rate = getBasicRate(utilRateId, rateType, serviceType);
            return new Object[] { String.Format("Cust Charge: {0}x{1}", numDays, rate.ToString("G29")), numDays * rate };
        }

        public List<Object[]> calculateTierCost(int utilRateId, char chargeType, String rateType, char season, int tierSetId, decimal usage, decimal baseline) {
            List<Object[]> obj = new List<Object[]>();
            //decimal totalCost = 0.0M;
            for (int i = minTier; i <= maxTier; i++) {
                Object[] tierPercentages = getTierPercentage(tierSetId);
                decimal tierLimit = baseline * (decimal)tierPercentages[i];
                Object[] rate = getTierRate(utilRateId, chargeType, rateType, season);
                if (usage > tierLimit && (decimal)tierPercentages[i] != -1.00M) {
                    //totalCost += ((decimal)rate[i-1] * tierLimit);
                    usage -= tierLimit;
                    obj.Add(new Object[] { String.Format("Tier{0}: {1}x{2}", i, tierLimit.ToString("G29"), ((decimal)rate[i - 1]).ToString("G29")), tierLimit * (decimal)rate[i - 1] });
                } else {
                    //totalCost += ((decimal)rate[i-1] * usage);
                    obj.Add(new Object[] { String.Format("Tier{0}: {1}x{2}", i, usage.ToString("G29"), ((decimal)rate[i - 1]).ToString("G29")), usage * (decimal)rate[i - 1] });
                    break;
                }
            }
            return obj;
        }

        public List<Object[]> calculateTaxCost(char utilType, String[] tax, decimal usage) {
            List<Object[]> obj = new List<Object[]>();
            //decimal total = 0.0M;
            foreach (String taxType in tax) {
                decimal rate = getTaxRate(parkId, utilType, taxType);
                obj.Add(new Object[] { String.Format(taxType + " {0}x{1}", usage.ToString("G29"), rate.ToString("G29")), (usage * rate) });
                //total += usage * rate;
            }
            return obj;
        }

        public List<Object[]> calculateSurcharges(int utilRateId, String rateType, int days, decimal quantity) {
            List<Object[]> obj = new List<Object[]>();
            ArrayList surcharges = getSurcharge(utilRateId, rateType);

            //decimal total = 0.0M;
            foreach (Object[] surcharge in surcharges) {
                decimal rate = (decimal)surcharge[2];
                int usage = (int)surcharge[1];
                String description = surcharge[0].ToString();

                switch (usage) {
                    case 1:
                        obj.Add(new Object[] { description, rate });
                        //total+= rate;
                        break;
                    case 2:
                        obj.Add(new Object[] { String.Format(description + " {0}x{1}", quantity.ToString("G29"), rate.ToString("G29")), quantity * rate });
                        //total+= quantity * rate;
                        break;
                    case 3:
                        obj.Add(new Object[] { String.Format(description + " {0}x{1}", quantity.ToString("G29"), rate.ToString("G29")), days * rate });
                        //total+= days * rate;
                        break;
                }
            }
            return obj;
        }

        private decimal getBaselineAllowance(int utilRateId, char season, char serviceType, char zone) {
            String condition = "UtilityRateID=@value0 AND Season=@value1 AND ServiceType=@value2 AND ClimateZone=@value3";
            Object[] values = { utilRateId, season, serviceType, zone };
            return (decimal)DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, values)[0];
        }

        private Object[] getTierPercentage(int tierSetId) {
            return DatabaseControl.getSingleRecord(new String[] { "TierSetName", "Tier1", "Tier2", "Tier3", "Tier4", "Tier5" }, DatabaseControl.tierTable, "TierSetID=@value0",
                new Object[] { tierSetId });
        }

        private decimal getBasicRate(int utilRateId, String status, char service) {
            String condition = "UtilityRateID=@value0 AND Status=@value1 AND Service=@value2";
            Object[] values = { utilRateId, status, service };
            return (decimal)DatabaseControl.getSingleRecord(new String[] { "Rate" }, DatabaseControl.utilBasicRatesTable, condition, values)[0];
        }

        private String getBasicDescription(int utilRateId, char serviceType, String rateType) {
            String condition = "UtilityRateID=@value0 AND ServiceType=@value1 AND RateType=@value2";
            Object[] values = { utilRateId, serviceType, rateType };
            return DatabaseControl.getSingleRecord(new String[] { "Description" }, DatabaseControl.utilBasicRatesTable, condition, values)[0].ToString();
        }

        private ArrayList getSurcharge(int utilRateId, String rateType) {
            String condition = "UtilityRateID=@value0 AND RateType=@value1";
            Object[] values = { utilRateId, rateType };
            return DatabaseControl.getMultipleRecord(new String[] { "Description", "Usage", "Rate" }, DatabaseControl.utilSurchargeTable, condition, values);
        }

        private Object[] getTierRate(int utilRateId, char chargeType, String rateType, char season) {
            String condition = "UtilityRateID=@value0 AND ChargeType=@value1 AND RateType=@value2 AND Season=@value3";
            Object[] values = { utilRateId, chargeType, rateType, season };
            return DatabaseControl.getSingleRecord(new String[] { "Rate1", "Rate2", "Rate3", "Rate4", "Rate5" },
                DatabaseControl.utilTierRatesTable, condition, values);
        }

        private decimal getTaxRate(int parkId, char utilType, String tax) {
            return Convert.ToDecimal(DatabaseControl.getSingleRecord(new String[] { tax }, DatabaseControl.taxTable,
                "ParkID=@value0 AND UtilityType=@value1 ORDER BY TaxRateID DESC", new Object[] { parkId, utilType })[0]);
        }
    }
}

