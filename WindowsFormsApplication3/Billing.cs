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
    class Billing {
        public class Bill {
            int parkId, parkSpaceId;
            Object[] meterRead;

            private static int minTier = 1;
            private static int maxTier = 5;

            public Bill(int parkId, int parkSpaceId) {
                this.parkId = parkId;
                this.parkSpaceId = parkSpaceId;
                String spaceId = DatabaseControl.getSingleRecord(new String[] {"SpaceID"}, DatabaseControl.spaceTable, "ParkID=@value0 AND ParkSpaceID=@value1",
                    new Object[] { parkId, parkSpaceId })[0].ToString();

                ArrayList reads = DatabaseControl.getMultipleRecord(DatabaseControl.meterReadsColumns, DatabaseControl.meterReadsTable,
                    "ParkID=@value0 AND SpaceID=@value1 ORDER BY MeterReadID DESC", new Object[] { parkId, spaceId });
                meterRead = (Object[])reads[0];
            }

            public Bill(int parkId, int parkSpaceId, Object[] currRead) {
                this.parkId = parkId;
                this.parkSpaceId = parkSpaceId;
                meterRead = currRead;
            }

            public decimal calculateGasTotalCost() {
                int utilRateId, tierSetId, numDays;
                char season, zone, rateType, serviceType;
                decimal usage, baseline;

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
                }
                else {
                    season = ' ';
                }
                zone = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "GasZone" }, DatabaseControl.parkTable,
                    "ParkID=@value0", new Object[] { parkId })[0]);
                rateType = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "GasStatus" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0]);
                serviceType = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "GasType" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0]);
                if (rateType == 'M') { rateType = 'S'; }

                usage = Convert.ToDecimal(meterRead[1]);
                numDays = (int)(((DateTime)meterRead[2]).Date - ((DateTime)meterRead[2]).Date).TotalDays;
                baseline = getBaselineAllowance(utilRateId, season, serviceType, zone);

                FileStream fs = new FileStream("Gas.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                Console.SetOut(sw);

                Console.WriteLine("Gas  \r\nUsage:{0}  Days:{1}  Baseline:{2}", usage, numDays, (baseline * numDays).ToString("G29"));
                Console.WriteLine("Status: {0}   Zone: {1}   Season: {2}  Service: {3}", rateType, zone, season, serviceType);
                Console.WriteLine("PrevDate: {0}   CurrDate: {1}   PrevRead: {2}   CurrRead: {3}\r\n", ((DateTime)meterRead[2]).Date.ToShortDateString(), ((DateTime)meterRead[2]).Date.ToShortDateString(),
                    meterRead[1], meterRead[1]);

                decimal tax = calculateTaxCost('G', new String[] { "LocalTax", "StateTax" }, usage);

                sw.Close();
                Process.Start("Gas.txt");
                return 0.0M;
            }

            public decimal calculateEleTotalCost() {
                int utilRateId, tierSetId, numDays, usage, numMed; char season, zone, serviceType; String rateType; decimal baseline, medAllowance;

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
                }
                else {
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

                FileStream fs = new FileStream("Electric.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                Console.SetOut(sw);

                Console.WriteLine("Electric  \r\nUsage:{0}  Days:{1}  Baseline:{2}  MedAllowance:{3}", usage, numDays,
                    (baseline * numDays + numMed * medAllowance).ToString("G29"), numMed);
                Console.WriteLine("Status: {0}   Zone: {1}   Season: {2}  Service: {3}", rateType, zone, season, serviceType);
                Console.WriteLine("PrevDate: {0}   CurrDate: {1}   PrevRead: {2}   CurrRead: {3}\r\n", ((DateTime)meterRead[2]).Date.ToShortDateString(), ((DateTime)meterRead[2]).Date.ToShortDateString(),
                    meterRead[4], meterRead[7]);

                decimal delivery = calculateTierCost(utilRateId, 'D', rateType, season, tierSetId, usage, baseline * numDays + numMed * medAllowance);
                Console.WriteLine("\r\nDelivery Total:              {0}\r\n", delivery.ToString("C", CultureInfo.CurrentCulture));

                decimal generation = calculateTierCost(utilRateId, 'G', rateType, season, tierSetId, usage, baseline * numDays + numMed * medAllowance);
                Console.WriteLine("\r\nGeneration Total:            {0}\r\n", generation.ToString("C", CultureInfo.CurrentCulture));

                decimal surcharge = calculateSurcharges(utilRateId, "All", numDays, usage) + calculateSurcharges(utilRateId, rateType, numDays, usage);
                Console.WriteLine("\r\nSurcharge Total:            {0}\r\n", surcharge.ToString("C", CultureInfo.CurrentCulture));

                decimal tax = calculateTaxCost('E', new String[] {"StateTax"}, usage);
                Console.WriteLine("\r\nTotal:{0}", (delivery + generation + surcharge + tax).ToString("C", CultureInfo.CurrentCulture));

                sw.Close();
                Process.Start("Electric.txt");
                return 0.0M;
            }

            public decimal calculateWatTotalCost() {
                return 0.0M;
            }

            public decimal calculateBasicCost(int utilRateId, char serviceType, String rateType, int numDays) {
                decimal rate = getBasicRate(utilRateId, serviceType, rateType);
                Console.WriteLine(String.Format("Basic Charge: {0}x{1}={2}", numDays, rate.ToString("G29"), (numDays * rate).ToString("C", CultureInfo.CurrentCulture)));
                return numDays * rate;
            }

            public decimal calculateTierCost(int utilRateId, char chargeType, String rateType, char season, int tierSetId, decimal usage, decimal baseline) {
                decimal totalCost = 0.0M;
                for (int i = minTier; i <= maxTier; i++) {
                    Object[] tierPercentages = getTierPercentage(tierSetId);
                    decimal tierLimit = baseline * (decimal)tierPercentages[i];
                    Object[] rate = getTierRate(utilRateId, chargeType, rateType, season);
                    if (usage > tierLimit && (decimal)tierPercentages[i] != -1.0M) {
                        totalCost += ((decimal)rate[i-1] * tierLimit);
                        usage -= tierLimit;
                        Console.WriteLine(String.Format("Tier{0}: {1}x{2}={3}", i, tierLimit.ToString("G29"), ((decimal)rate[i-1]).ToString("G29"), (tierLimit * (decimal)rate[i-1]).ToString("C", CultureInfo.CurrentCulture)));
                    }
                    else {
                        totalCost += ((decimal)rate[i-1] * usage);
                        Console.WriteLine(String.Format("Tier{0}: {1}x{2}={3}", i, usage.ToString("G29"), ((decimal)rate[i-1]).ToString("G29"), (usage * (decimal)rate[i-1]).ToString("C", CultureInfo.CurrentCulture)));
                        break;
                    }
                }
                return totalCost;
            }

            public decimal calculateTaxCost(char utilType, String[] tax, decimal usage) {
                decimal total = 0.0M;
                foreach (String taxType in tax) {
                    decimal rate = getTaxRate(parkId, utilType, taxType);
                    Console.WriteLine(String.Format(taxType + ": {0}x{1}={2}", usage.ToString("G29"), rate.ToString("G29"), (usage * rate).ToString("C", CultureInfo.CurrentCulture)));
                    total += usage * rate;
                }
                return total;
            }

            public decimal calculateSurcharges(int utilRateId, String rateType, int days, decimal quantity) {
                ArrayList surcharges = getSurcharge(utilRateId, rateType);

                decimal total = 0.0M;
                foreach (Object[] surcharge in surcharges) {
                    decimal rate = (decimal)surcharge[2];
                    int usage = (int)surcharge[1];
                    String description = surcharge[0].ToString();

                    switch (usage) {
                        case 1:
                            Console.WriteLine(String.Format(description + ": {0}", rate.ToString("G29")));
                            total+= rate;
                            break;
                        case 2:
                            Console.WriteLine(String.Format(description + ": {0}x{1}={2}", quantity.ToString("G29"), rate.ToString("G29"), (quantity * rate).ToString("C", CultureInfo.CurrentCulture)));
                            total+= quantity * rate;
                            break;
                        case 3:
                            Console.WriteLine(String.Format(description + ": {0}x{1}={2}", days.ToString("G29"), rate.ToString("G29"), (days * rate).ToString("C", CultureInfo.CurrentCulture)));
                            total+= days * rate;
                            break;
                    }
                }
                return total;
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

            private decimal getBasicRate(int utilRateId, char serviceType, String rateType) {
                String condition = "UtilityRateID=@value0 AND ServiceType=@value1 AND RateType=@value2";
                Object[] values = { utilRateId, serviceType, rateType };
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
                return Convert.ToDecimal(DatabaseControl.getSingleRecord(new String[] { tax }, DatabaseControl.taxTable, "ParkID=@value0 AND UtilityType=@value1 ORDER BY TaxRateID DESC",
                    new Object[] { parkId, utilType })[0]);
            }
        }
    }
}
