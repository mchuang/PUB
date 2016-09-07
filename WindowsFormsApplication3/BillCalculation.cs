using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PUB {
    public class BillCalculation {
        int parkId, parkSpaceId, parkNumber;
        public Dictionary<String, Object> tenantInfo;
        public Dictionary<String, int> meterRead;
        public DateTime start, end, dueDate;
        public decimal thermX;
        Object[] companyInfo;

        static String[] taxList = new String[] { "LocalTax", "CountyTax", "StateTax", "RegTax" };
        static String[] utilInfoList = new String[] { "UtilityRateID", "TierSetID", "EffectiveDate", "Method" };

        private static int minTier = 1;
        private static int maxTier = 5;

        public BillCalculation(int parkId, int parkSpaceId, DateTime dueDate) {
            this.parkId = parkId;
            this.parkSpaceId = parkSpaceId;
            this.parkNumber = (int)DatabaseControl.getSingleRecord(new String[] { "ParkNumber" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0];
            String[] tenantFields = new String[] { "OrderID", "SpaceID", "Tenant", "GasStatus", "GasType", "EleStatus", "EleType", 
                    "WatStatus", "GasMedical", "EleMedical", "DontBillForGas", "DontBillForEle", "DontBillForWat", 
                    "Address1", "Address2", "City", "State", "Zip", "MoveInDate", "MoveOutDate" };
            tenantInfo = DatabaseControl.getSingleRecordDict(tenantFields, DatabaseControl.spaceTable,
                "ParkID=@value0 AND ParkSpaceID=@value1", new Object[] { parkId, parkSpaceId });

            this.dueDate = dueDate;
            Object[] readDates = DatabaseControl.getSingleRecord(new String[] { "StartDate", "MeterReadDate" }, DatabaseControl.meterReadsTable,
                "ParkID=@value0 AND OrderID=@value1 AND DueDate=@value2", new Object[] { parkId, tenantInfo["OrderID"], dueDate });

            if (readDates != null) {
                if ((DateTime)tenantInfo["MoveInDate"] > (DateTime)readDates[0] && (DateTime)tenantInfo["MoveInDate"] < (DateTime)readDates[1]) this.start = (DateTime)tenantInfo["MoveInDate"];
                else this.start = (DateTime)readDates[0];

                if (tenantInfo["MoveOutDate"] != DBNull.Value && (DateTime)tenantInfo["MoveOutDate"] > (DateTime)readDates[0] && (DateTime)tenantInfo["MoveOutDate"] < (DateTime)readDates[1]) this.end = (DateTime)tenantInfo["MoveOutDate"];
                else this.end = (DateTime)readDates[1];
            }
            String[] fields = new String[] { "GasReadValue", "EleReadValue", "WatReadValue", "ThermX" };
            String condition = "ParkID=@value0 AND OrderID=@value1 AND DueDate<=@value2 ORDER BY DueDate DESC";
            ArrayList reads = DatabaseControl.getMultipleRecordDict(fields, DatabaseControl.meterReadsTable, condition, new Object[] { parkId, tenantInfo["OrderID"], dueDate });

            populateMeterRead(reads);

            companyInfo = DatabaseControl.getSingleRecord(new String[] { "GasCompanyID", "ElectricityCompanyID", "WaterCompanyID" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId });
        }

        public void populateMeterRead(ArrayList reads) {
            meterRead = new Dictionary<String, int>();
            if (reads.Count > 0) {
                Dictionary<String, Object> month0 = (Dictionary<String, Object>)reads[0];
                if (month0["ThermX"] != DBNull.Value) { thermX = (decimal)month0["ThermX"]; } else { thermX = 1; }
                meterRead["GasReadValue"] = (int)month0["GasReadValue"];
                meterRead["EleReadValue"] = (int)month0["EleReadValue"];
                meterRead["WatReadValue"] = (int)month0["WatReadValue"];
            } else {
                meterRead["GasReadValue"] = meterRead["EleReadValue"] = meterRead["WatReadValue"] = -1;
            }

            if (reads.Count > 1) {
                Dictionary<String, Object> month1 = (Dictionary<String, Object>)reads[1];
                meterRead["PrevGasReadValue"] = (int)month1["GasReadValue"];
                meterRead["PrevEleReadValue"] = (int)month1["EleReadValue"];
                meterRead["PrevWatReadValue"] = (int)month1["WatReadValue"];
            } else {
                meterRead["PrevGasReadValue"] = meterRead["PrevEleReadValue"] = meterRead["PrevWatReadValue"] = -1;
            }

            if (reads.Count > 13) {
                try {
                    Dictionary<String, Object> month0 = (Dictionary<String, Object>)reads[12];
                    Dictionary<String, Object> month1 = (Dictionary<String, Object>)reads[13];
                    meterRead["PrevYearGasUsage"] = Convert.ToInt32(((int)month0["EleReadValue"] - (int)month1["EleReadValue"]) * (decimal)month1["ThermX"]);
                    meterRead["PrevYearEleUsage"] = (int)month0["GasReadValue"] - (int)month1["GasReadValue"];
                    meterRead["PrevYearWatUsage"] = (int)month0["WatReadValue"] - (int)month1["WatReadValue"];
                } catch {
                    meterRead["PrevYearGasUsage"] = meterRead["PrevYearEleUsage"] = meterRead["PrevYearWatUsage"] = -1;
                }
            } else {
                meterRead["PrevYearGasUsage"] = meterRead["PrevYearEleUsage"] = meterRead["PrevYearWatUsage"] = -1;
            }
        }

        public Object[] generateBill() {
            using (SqlConnection connection = DatabaseControl.Connect()) {
                connection.Open();
                Object[] tenant = getTenantAddr();
                Object[] park = null;// getParkAddr();
                Object[] ele, gas, wat;
                ele = calculateEleTotalCost(connection);
                if (companyInfo[0] == DBNull.Value) { gas = calculateGasTotalCost(connection); } else {
                    switch ((int)companyInfo[0]) {
                        case 21:
                            gas = calculateGasTotalCostSCE(connection); break;
                        case 1102:
                            gas = calculateGasTotalCostPGE(connection); break;
                        default:
                            gas = calculateGasTotalCost(connection); break;
                    }
                }
                wat = calculateWatTotalCost(connection);
                Object[] usage = getReadData((decimal)gas[2], (decimal)ele[2], (decimal)wat[2]);
                connection.Close();
                return new Object[] { parkNumber, tenantInfo["OrderID"], tenant, park, usage, gas, ele, wat, null, start, end, null };
            }
        }

        public Object[] generateUtilReport() {
            using (SqlConnection connection = DatabaseControl.Connect()) {
                connection.Open();
                Dictionary<string, decimal> ele, gas, wat;
                ele = calculateUtilEle(connection);
                gas = calculateUtilGas(connection);
                wat = calculateUtilWat(connection);
                connection.Close();
                return new Object[] { gas, ele, wat };
            }
        }

        public Object[] getTenantAddr() {
            return new Object[] { tenantInfo["Tenant"], tenantInfo["Address1"], tenantInfo["Address2"], tenantInfo["City"], tenantInfo["State"], tenantInfo["Zip"] };
        }

        public Object[] getParkAddr() {
            String[] fields = { "ParkName", "Address", "City", "State", "ZipCode" };
            String condition = "ParkID=@value0";
            return DatabaseControl.getSingleRecord(fields, DatabaseControl.parkTable, condition, new Object[] { this.parkId });
        }

        public Object[] getReadData(decimal gasTotal, decimal eleTotal, decimal watTotal) {
            int gasUsage = Convert.ToInt32((meterRead["GasReadValue"] - meterRead["PrevGasReadValue"]) * thermX);
            int eleUsage = meterRead["EleReadValue"] - meterRead["PrevEleReadValue"];
            int watUsage = meterRead["WatReadValue"] - meterRead["PrevWatReadValue"];
            Object[] usages = new Object[] { (int)(end-start).TotalDays, gasUsage, meterRead["PrevYearGasUsage"],
                                                 (int)(end-start).TotalDays, eleUsage, meterRead["PrevYearEleUsage"],
                                                 (int)(end-start).TotalDays, watUsage, meterRead["PrevYearWatUsage"] };
            Object[] gas = new Object[] { "GAS", start.ToString("MM/dd"), end.ToString("MM/dd"), meterRead["PrevGasReadValue"], 
                    meterRead["GasReadValue"], thermX, gasUsage, gasTotal.ToString() };
            Object[] ele = new Object[] { "ELE", start.ToString("MM/dd"), end.ToString("MM/dd"), meterRead["PrevEleReadValue"], 
                    meterRead["EleReadValue"], null, eleUsage, eleTotal.ToString() };
            Object[] wat = new Object[] { "WAT", start.ToString("MM/dd"), end.ToString("MM/dd"), meterRead["PrevWatReadValue"], 
                    meterRead["WatReadValue"], null, watUsage, watTotal.ToString() };
            return new Object[] { usages, gas, ele, wat };
        }

        //USING SQLCONNECTION
        public Object[] calculateGasTotalCost(SqlConnection conn) {
            int utilRateId, tierSetId, totalDays, numMed;
            char zone, serviceType;
            String rateType;
            decimal usage, baseline;

            if ((bool)tenantInfo["DontBillForGas"]) return new Object[] { null, new List<Object[]>(), 0M };

            Object company = companyInfo[0];
            if (company == DBNull.Value) return new Object[] { null, new List<Object[]>(), 0M };
            int gasCompanyId = (int)company;
            String utilRateCondition = "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND Dirty=0 ORDER BY EffectiveDate DESC";
            ArrayList utilRateCompany = DatabaseControl.getMultipleRecord(conn, utilInfoList, DatabaseControl.utilRateTable,
                utilRateCondition, new Object[] { gasCompanyId, 'G' });
            Object[] utilCompany = (Object[])utilRateCompany[0];
            tierSetId = (int)utilCompany[1];

            zone = Convert.ToChar(DatabaseControl.getSingleRecord(conn, new String[] { "GasZone" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0]);
            rateType = tenantInfo["GasStatus"].ToString();
            serviceType = Convert.ToChar(tenantInfo["GasType"]);
            numMed = (int)tenantInfo["GasMedical"];
            if (rateType == "") { rateType = "Regular"; }

            usage = Decimal.Round((meterRead["GasReadValue"] - meterRead["PrevGasReadValue"]) * thermX);
            totalDays = (int)(end - start).TotalDays;

            List<Object[]> details = new List<Object[]>();

            baseline = 0M;
            DateTime startDate, endDate;
            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            foreach (Object[] item in utilRateCompany) {
                int numDays = 0;
                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                    break;
                } else if (effDate <= endDate) {
                    numDays = (int)(endDate - effDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                }
                endDate = effDate;
            }
            baseline = Decimal.Round(baseline);

            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            decimal subtotal = 0M;
            foreach (Object[] item in utilRateCompany) {
                decimal generationTotal, surchargeTotal;
                generationTotal = surchargeTotal = 0M;

                int numDays = 0;
                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                } else if (effDate <= endDate) {
                    numDays = (int)(endDate - effDate).TotalDays;
                }
                endDate = effDate;

                if (numDays <= 0) break;
                Object[] custCharge = calculateBasicCost(getBasicRate(conn, (int)item[0], rateType, serviceType), numDays * numDays / totalDays);

                List<Object[]> generation = calculateTierCost(getTierRate(conn, (int)item[0], 'G', rateType), getTierPercentage(conn, tierSetId), usage, baseline, (decimal)numDays / totalDays);
                foreach (Object[] row in generation) if (row.Length != 1) generationTotal += (decimal)row[1];

                List<Object[]> surcharge = calculateSurcharges(getSurcharge(conn, (int)item[0], "All"), numDays, usage * numDays / totalDays, generationTotal);
                surcharge.AddRange(calculateSurcharges(getSurcharge(conn, (int)item[0], rateType), numDays, usage * numDays / totalDays, generationTotal));
                foreach (Object[] row in surcharge) surchargeTotal += (decimal)row[1];

                Object[] minimum = calculateMinimumCost(getBasicRate(conn, (int)item[0], rateType, '0'), numDays);
                if ((decimal)minimum[1] > generationTotal) {
                    generation.Clear();
                    generation.Insert(0, minimum);
                    generationTotal = (decimal)minimum[1];
                }

                decimal taxTotal = 0M;
                List<Object[]> tax = calculateTaxCost(conn, 'G', taxList, generationTotal + (decimal)custCharge[1]);
                foreach (Object[] row in tax) taxTotal += (decimal)row[1];

                details.Add(custCharge);
                details.AddRange(generation);
                details.AddRange(surcharge);
                details.AddRange(tax);
                details.Add(new Object[] { "" });

                subtotal += (decimal)custCharge[1] + generationTotal + surchargeTotal + taxTotal;
            }

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[1];

            details.Add(new Object[] { "Subtotal", subtotal });
            Object[] pdfGas = new Object[] { baseline, details, subtotal };

            return pdfGas;
        }

        public Dictionary<string, decimal> calculateUtilGas(SqlConnection conn) {
            int utilRateId, tierSetId, totalDays, numMed;
            char zone, serviceType;
            String rateType;
            decimal usage, baseline;

            Dictionary<string, decimal> details = new Dictionary<string, decimal>();
            details["CustCharge"] = 0M;
            details["Base"] = 0M;
            details["OverBase"] = 0M;
            details["Surcharge"] = 0M;
            details["Tax"] = 0M;
            details["Total"] = 0M;

            if ((bool)tenantInfo["DontBillForGas"]) return details;

            Object company = companyInfo[0];
            if (company == DBNull.Value) return details;
            int gasCompanyId = (int)company;
            String utilRateCondition = "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND Dirty=0 ORDER BY EffectiveDate DESC";
            ArrayList utilRateCompany = DatabaseControl.getMultipleRecord(conn, utilInfoList, DatabaseControl.utilRateTable,
                utilRateCondition, new Object[] { gasCompanyId, 'G' });
            Object[] utilCompany = (Object[])utilRateCompany[0];
            tierSetId = (int)utilCompany[1];

            zone = Convert.ToChar(DatabaseControl.getSingleRecord(conn, new String[] { "GasZone" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0]);
            rateType = tenantInfo["GasStatus"].ToString();
            serviceType = Convert.ToChar(tenantInfo["GasType"]);
            numMed = (int)tenantInfo["GasMedical"];
            if (rateType == "") { rateType = "Regular"; }

            usage = Decimal.Round((meterRead["GasReadValue"] - meterRead["PrevGasReadValue"]) * thermX);
            totalDays = (int)(end - start).TotalDays;

            baseline = 0M;
            DateTime startDate, endDate;
            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            foreach (Object[] item in utilRateCompany) {
                int numDays = 0;
                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                    utilCompany = item;
                    break;
                } else if (effDate <= endDate) {
                    numDays = (int)(endDate - effDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                }
                endDate = effDate;
            }
            baseline = Decimal.Round(baseline);

            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            decimal subtotal = 0M;
            foreach (Object[] item in utilRateCompany) {
                decimal generationTotal, surchargeTotal;
                generationTotal = surchargeTotal = 0M;

                int numDays = 0;
                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                } else if (effDate <= endDate) {
                    numDays = (int)(endDate - effDate).TotalDays;
                }
                endDate = effDate;

                if (numDays <= 0) break;
                Object[] custCharge = calculateBasicCost(getBasicRate(conn, (int)item[0], rateType, serviceType), numDays * numDays / totalDays);

                List<Object[]> generation = calculateTierCost(getTierRate(conn, (int)item[0], 'G', rateType), getTierPercentage(conn, tierSetId), usage, baseline, (decimal)numDays / totalDays);
                foreach (Object[] row in generation) if (row.Length != 1) generationTotal += (decimal)row[1];

                List<Object[]> surcharge = calculateSurcharges(getSurcharge(conn, (int)item[0], "All"), numDays, usage * numDays / totalDays, generationTotal);
                surcharge.AddRange(calculateSurcharges(getSurcharge(conn, (int)item[0], rateType), numDays, usage * numDays / totalDays, generationTotal));
                foreach (Object[] row in surcharge) surchargeTotal += (decimal)row[1];

                Object[] minimum = calculateMinimumCost(getBasicRate(conn, (int)item[0], rateType, '0'), numDays);
                if ((decimal)minimum[1] > generationTotal) {
                    generation.Clear();
                    generation.Insert(0, minimum);
                    generationTotal = (decimal)minimum[1];
                }

                decimal taxTotal = 0M;
                List<Object[]> tax = calculateTaxCost(conn, 'G', taxList, generationTotal + (decimal)custCharge[1]);
                foreach (Object[] row in tax) taxTotal += (decimal)row[1];

                details["CustCharge"] += (decimal)custCharge[1];
                if (generation.Count > 0) details["Base"] += (decimal)generation[0][1]; else details["Base"] = 0M;
                if (generation.Count > 1) details["OverBase"] += (decimal)generation[1][1]; else details["OverBase"] = 0M;
                details["Surcharge"] += surchargeTotal;
                details["Tax"] += taxTotal;
                details["Total"] += (decimal)custCharge[1] + generationTotal + surchargeTotal + taxTotal;
            }
            return details;
        }

        public Object[] calculateGasTotalCostPGE(SqlConnection conn) {
            int utilRateId, tierSetId, totalDays, numMed;
            char zone, serviceType;
            String rateType;
            decimal usage, baseline;

            if ((bool)tenantInfo["DontBillForGas"]) return new Object[] { null, new List<Object[]>(), 0M };

            Object company = companyInfo[0];
            if (company == DBNull.Value) return new Object[] { null, new List<Object[]>(), 0M };
            int gasCompanyId = (int)company;
            String utilRateCondition = "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND Dirty=0 ORDER BY EffectiveDate DESC";
            ArrayList utilRateCompany = DatabaseControl.getMultipleRecord(conn, utilInfoList, DatabaseControl.utilRateTable,
                utilRateCondition, new Object[] { gasCompanyId, 'G' });
            Object[] utilCompany = (Object[])utilRateCompany[0];
            tierSetId = (int)utilCompany[1];

            zone = Convert.ToChar(DatabaseControl.getSingleRecord(conn, new String[] { "GasZone" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0]);
            rateType = tenantInfo["GasStatus"].ToString();
            serviceType = Convert.ToChar(tenantInfo["GasType"]);
            numMed = (int)tenantInfo["GasMedical"];
            if (rateType == "") { rateType = "Regular"; }

            usage = Decimal.Round((meterRead["GasReadValue"] - meterRead["PrevGasReadValue"]) * thermX);
            totalDays = (int)(end - start).TotalDays;

            List<Object[]> details = new List<Object[]>();

            baseline = 0M;
            DateTime startDate, endDate;
            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            foreach (Object[] item in utilRateCompany) {
                int numDays = 0;
                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                    break;
                } else if (effDate <= endDate) {
                    numDays = (int)(endDate - effDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                } else {
                    continue;
                }
                endDate = effDate;
            }
            baseline = Decimal.Round(baseline);

            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            decimal subtotal = 0M;
            foreach (Object[] item in utilRateCompany) {
                decimal generationTotal, surchargeTotal;
                generationTotal = surchargeTotal = 0M;

                int numDays = 0;
                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                    if (numDays <= 0) break;
                    details.Add(new Object[] { String.Format("{0}-{1}", startDate.AddDays(-1).ToString("d"), endDate.AddDays(-1).ToString("d")) });
                } else if (effDate <= endDate) {
                    numDays = (int)(endDate - effDate).TotalDays;
                    details.Add(new Object[] { String.Format("{0}-{1}", effDate.ToString("d"), endDate.AddDays(-1).ToString("d")) });
                } else {
                    continue;
                }
                endDate = effDate;

                if (numDays <= 0) break;
                Object[] custCharge = calculateBasicCost(getBasicRate(conn, (int)item[0], rateType, serviceType), numDays * numDays / totalDays);

                List<Object[]> generation = calculateTierCost(getTierRate(conn, (int)item[0], 'G', rateType), getTierPercentage(conn, tierSetId), usage, baseline, (decimal)numDays / totalDays);
                foreach (Object[] row in generation) if (row.Length != 1) generationTotal += (decimal)row[1];

                List<Object[]> surcharge = calculateSurcharges(getSurcharge(conn, (int)item[0], "All"), numDays, usage * numDays / totalDays, generationTotal);
                surcharge.AddRange(calculateSurcharges(getSurcharge(conn, (int)item[0], rateType), numDays, usage * numDays / totalDays, generationTotal));
                foreach (Object[] row in surcharge) surchargeTotal += (decimal)row[1];

                Object[] minimum = calculateMinimumCost(getBasicRate(conn, (int)item[0], rateType, '0'), numDays);
                if ((decimal)minimum[1] > generationTotal) {
                    generation.Clear();
                    generation.Insert(0, minimum);
                    generationTotal = (decimal)minimum[1];
                }

                decimal taxTotal = 0M;
                List<Object[]> tax = calculateTaxCost(conn, 'G', taxList, generationTotal + (decimal)custCharge[1]);
                foreach (Object[] row in tax) taxTotal += (decimal)row[1];

                details.Add(custCharge);
                details.AddRange(generation);
                details.AddRange(surcharge);
                details.AddRange(tax);
                details.Add(new Object[] { "" });

                subtotal += (decimal)custCharge[1] + generationTotal + surchargeTotal + taxTotal;
            }

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[1];

            details.Add(new Object[] { "Subtotal", subtotal });
            Object[] pdfGas = new Object[] { baseline, details, subtotal };

            return pdfGas;
        }

        public Object[] calculateGasTotalCostSCE(SqlConnection conn) {
            int utilRateId, tierSetId, usage, numMed; char serviceType, zone;
            String rateType; decimal baseline;
            Object company = companyInfo[0];
            if (company == DBNull.Value) return new Object[] { null, new List<Object[]>(), 0M };
            int gasCompanyId = (int)company;
            if ((bool)tenantInfo["DontBillForGas"]) {
                return new Object[] { null, new List<Object[]>(), 0M };
            }
            String utilRateCondition = "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND Dirty=0 ORDER BY EffectiveDate DESC";
            ArrayList utilRateCompany = DatabaseControl.getMultipleRecord(conn, utilInfoList, DatabaseControl.utilRateTable,
                utilRateCondition, new Object[] { gasCompanyId, 'G' });
            Object[] utilCompany = (Object[])utilRateCompany[0];

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[1];

            zone = Convert.ToChar(DatabaseControl.getSingleRecord(conn, new String[] { "GasZone" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0]);
            rateType = tenantInfo["GasStatus"].ToString();
            serviceType = Convert.ToChar(tenantInfo["GasType"]);
            numMed = (int)tenantInfo["GasMedical"];
            if (rateType == "") { rateType = "Regular"; }

            usage = meterRead["GasReadValue"] - meterRead["PrevGasReadValue"];
            int totalDays = (int)(end - start).TotalDays;

            List<Object[]> details = new List<Object[]>();

            List<Object[]> utilRateBlendList = new List<Object[]>();
            baseline = 0M;
            DateTime startDate, endDate;
            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            foreach (Object[] item in utilRateCompany) {
                int numDays = 0;
                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                    utilRateBlendList.Add(item);
                    break;
                } else if (effDate <= endDate) {
                    numDays = (int)(endDate - effDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                    utilRateBlendList.Add(item);
                }
                endDate = effDate;
            }
            baseline = Decimal.Round(baseline);

            decimal subtotal = 0M;

            decimal generationTotal, surchargeTotal;
            generationTotal = surchargeTotal = 0M;

            Object[] custCharge = calculateBasicCost(getBasicRate(conn, utilRateId, rateType, serviceType), totalDays);

            List<Object[]> generation = calculateBlendedTierCost(conn, utilRateBlendList, 'G', rateType, tierSetId, (decimal)usage, baseline);
            foreach (Object[] row in generation) if (row.Length != 1) generationTotal += (decimal)row[1];

            List<Object[]> surcharge = calculateSurcharges(getSurcharge(conn, utilRateId, "All"), totalDays, usage, (decimal)custCharge[1] + generationTotal);
            surcharge.AddRange(calculateSurcharges(getSurcharge(conn, utilRateId, rateType), totalDays, usage, (decimal)custCharge[1] + generationTotal));

            List<Object[]> filterSurcharge = new List<Object[]>();
            foreach (Object[] row in surcharge) {
                if (Convert.ToChar(row[2]) == 'G') {
                    generation.Add(row);
                    generationTotal += (decimal)row[1];
                } else {
                    filterSurcharge.Add(row);
                    surchargeTotal += (decimal)row[1];
                }
            }

            Object[] minimum = calculateMinimumCost(getBasicRate(conn, utilRateId, rateType, '0'), totalDays);
            if ((decimal)minimum[1] > (decimal)custCharge[1] + generationTotal) {
                generation.Clear();
                generation.Insert(0, minimum);
                generationTotal = (decimal)minimum[1];
            }

            details.Add(new Object[] { "" });
            details.Add(custCharge);
            details.AddRange(generation);
            details.Add(new Object[] { "" });
            details.AddRange(filterSurcharge);
            details.Add(new Object[] { "" });

            subtotal += (decimal)custCharge[1] + generationTotal + surchargeTotal;

            decimal taxTotal = 0M;
            List<Object[]> tax = calculateTaxCost(conn, 'E', taxList, (decimal)custCharge[1] + generationTotal);
            foreach (Object[] item in tax) taxTotal += (decimal)item[1];

            details.Add(new Object[] { "Subtotal", subtotal });
            details.AddRange(tax);

            Object[] pdfGas = new Object[] { baseline, details, subtotal + taxTotal };
            return pdfGas;
        }

        public Object[] calculateEleTotalCost(SqlConnection conn) {
            int utilRateId, tierSetId, usage, numMed; char serviceType, zone;
            String rateType; decimal baseline;
            Object company = companyInfo[1];
            if (company == DBNull.Value) return new Object[] { null, new List<Object[]>(), 0M };
            int eleCompanyId = (int)company;
            if ((bool)tenantInfo["DontBillForEle"]) return new Object[] { null, new List<Object[]>(), 0M };
            String utilRateCondition = "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND Dirty=0 ORDER BY EffectiveDate DESC";
            ArrayList utilRateCompany = DatabaseControl.getMultipleRecord(conn, utilInfoList, DatabaseControl.utilRateTable,
                utilRateCondition, new Object[] { eleCompanyId, 'E' });
            Object[] utilCompany = (Object[])utilRateCompany[0];

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[1];

            zone = Convert.ToChar(DatabaseControl.getSingleRecord(conn, new String[] { "EleZone" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0]);
            rateType = tenantInfo["EleStatus"].ToString();
            serviceType = Convert.ToChar(tenantInfo["EleType"]);
            numMed = (int)tenantInfo["EleMedical"];
            if (rateType == "") { rateType = "Regular"; }

            usage = meterRead["EleReadValue"] - meterRead["PrevEleReadValue"];
            int totalDays = (int)(end - start).TotalDays;
            
            List<Object[]> details = new List<Object[]>();

            List<Object[]> utilRateBlendList = new List<Object[]>();
            baseline = 0M;
            DateTime startDate, endDate;
            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            foreach (Object[] item in utilRateCompany) {
                int numDays = 0;
                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                    utilRateBlendList.Add(item);
                    break;
                } else if (effDate <= endDate) {
                    numDays = (int)(endDate - effDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                    utilRateBlendList.Add(item);
                }
                endDate = effDate;
            }
            utilCompany = utilRateBlendList[0];
            if (utilCompany[3].ToString() == "Monthly") baseline = getBaselineAllowance(conn, (int)utilCompany[0], serviceType, zone, numMed, 1);
            baseline = Decimal.Round(baseline);

            decimal subtotal = 0M;

            decimal generationTotal, surchargeTotal, deliveryTotal;
            generationTotal = surchargeTotal = deliveryTotal = 0M;

            Object[] custCharge = calculateBasicCost(getBasicRate(conn, utilRateId, rateType, serviceType), totalDays);

            List<Object[]> generation = calculateBlendedTierCost(conn, utilRateBlendList, 'G', rateType, tierSetId, (decimal)usage, baseline);
            foreach (Object[] row in generation) if (row.Length != 1) generationTotal += (decimal)row[1];

            List<Object[]> delivery = calculateBlendedTierCost(conn, utilRateBlendList, 'D', rateType, tierSetId, (decimal)usage, baseline);
            foreach (Object[] row in delivery) if (row.Length != 1) deliveryTotal += (decimal)row[1];

            List<Object[]> surcharge = calculateSurcharges(getSurcharge(conn, utilRateId, "All"), totalDays, usage, generationTotal);
            surcharge.AddRange(calculateSurcharges(getSurcharge(conn, utilRateId, rateType), totalDays, usage, generationTotal));

            List<Object[]> filterSurcharge = new List<Object[]>();
            foreach (Object[] row in surcharge) {
                if (Convert.ToChar(row[2]) == 'D') {
                    delivery.Add(row);
                    deliveryTotal += (decimal)row[1];
                } else if (Convert.ToChar(row[2]) == 'G') {
                    generation.Add(row);
                    generationTotal += (decimal)row[1];
                } else {
                    filterSurcharge.Add(row);
                    surchargeTotal += (decimal)row[1];
                }
            }

            Object[] minimum;
            switch ((int)company) {
                case 1:
                    minimum = new Object[] { "Min Charge" , Decimal.Round(getBasicRate(conn, utilRateId, rateType, '0'), 2)};
                    break;
                default:
                    minimum = calculateMinimumCost(getBasicRate(conn, utilRateId, rateType, '0'), totalDays);
                    break;
            }
            if ((decimal)minimum[1] >= (decimal)custCharge[1] + generationTotal + deliveryTotal) {
                custCharge = new Object[] { "", 0M };
                delivery.Clear();
                deliveryTotal = 0M;
                generation.Clear();
                generation.Insert(0, minimum);
                generationTotal = (decimal)minimum[1];
            }

            details.Add(new Object[] { "" });
            details.Add(custCharge);
            details.AddRange(delivery);
            details.Add(new Object[] { "Delivery Total", (decimal)custCharge[1] + deliveryTotal });
            details.Add(new Object[] { "" });
            details.AddRange(generation);
            details.Add(new Object[] { "Generation Total", generationTotal });
            details.Add(new Object[] { "" });
            details.AddRange(filterSurcharge);
            details.Add(new Object[] { "" });

            subtotal += (decimal)custCharge[1] + generationTotal + surchargeTotal + deliveryTotal;

            decimal taxTotal = 0M;
            if ((decimal)minimum[1] <= (decimal)custCharge[1] + generationTotal + deliveryTotal) {
                List<Object[]> tax = calculateTaxCost(conn, 'E', taxList, (decimal)custCharge[1] + generationTotal + deliveryTotal);
                foreach (Object[] item in tax) taxTotal += (decimal)item[1];
                details.Add(new Object[] { "Subtotal", subtotal });
                details.AddRange(tax);
            }
            Object[] pdfEle = new Object[] { baseline, details, subtotal + taxTotal };
            return pdfEle;
        }

        public Dictionary<string, decimal> calculateUtilEle(SqlConnection conn) {
            int utilRateId, tierSetId, usage, numMed; char serviceType, zone;
            String rateType; decimal baseline;

            Dictionary<string, decimal> details = new Dictionary<string, decimal>();
            details["CustCharge"] = 0M;
            details["Tier1"] = 0M;
            details["Tier2"] = 0M;
            details["Tier3-5"] = 0M;
            details["Surcharge"] = 0M;
            details["Tax"] = 0M;
            details["Total"] = 0M;

            Object company = companyInfo[1];
            if (company == DBNull.Value) return details;
            int eleCompanyId = (int)company;
            if ((bool)tenantInfo["DontBillForEle"]) return details;
            String utilRateCondition = "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND Dirty=0 ORDER BY EffectiveDate DESC";
            ArrayList utilRateCompany = DatabaseControl.getMultipleRecord(conn, utilInfoList, DatabaseControl.utilRateTable,
                utilRateCondition, new Object[] { eleCompanyId, 'E' });
            Object[] utilCompany = (Object[])utilRateCompany[0];

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[1];

            zone = Convert.ToChar(DatabaseControl.getSingleRecord(conn, new String[] { "EleZone" }, DatabaseControl.parkTable,
                "ParkID=@value0", new Object[] { parkId })[0]);
            rateType = tenantInfo["EleStatus"].ToString();
            serviceType = Convert.ToChar(tenantInfo["EleType"]);
            numMed = (int)tenantInfo["EleMedical"];
            if (rateType == "") { rateType = "Regular"; }

            usage = meterRead["EleReadValue"] - meterRead["PrevEleReadValue"];
            int totalDays = (int)(end - start).TotalDays;

            List<Object[]> utilRateBlendList = new List<Object[]>();
            baseline = 0M;
            DateTime startDate, endDate;
            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            foreach (Object[] item in utilRateCompany) {
                int numDays = 0;
                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                    utilRateBlendList.Add(item);
                    break;
                } else if (effDate <= endDate) {
                    numDays = (int)(endDate - effDate).TotalDays;
                    baseline += getBaselineAllowance(conn, (int)item[0], serviceType, zone, numMed, numDays);
                    utilRateBlendList.Add(item);
                }
                endDate = effDate;
            }
            baseline = Decimal.Round(baseline);

            decimal subtotal = 0M;

            decimal generationTotal, surchargeTotal, deliveryTotal;
            generationTotal = surchargeTotal = deliveryTotal = 0M;

            Object[] custCharge = calculateBasicCost(getBasicRate(conn, utilRateId, rateType, serviceType), totalDays);

            List<Object[]> generation = calculateBlendedTierCost(conn, utilRateBlendList, 'G', rateType, tierSetId, (decimal)usage, baseline);
            foreach (Object[] row in generation) if (row.Length != 1) generationTotal += (decimal)row[1];

            List<Object[]> delivery = calculateBlendedTierCost(conn, utilRateBlendList, 'D', rateType, tierSetId, (decimal)usage, baseline);
            foreach (Object[] row in delivery) if (row.Length != 1) deliveryTotal += (decimal)row[1];

            List<Object[]> surcharge = calculateSurcharges(getSurcharge(conn, utilRateId, "All"), totalDays, usage, generationTotal);
            surcharge.AddRange(calculateSurcharges(getSurcharge(conn, utilRateId, rateType), totalDays, usage, generationTotal));

            List<Object[]> filterSurcharge = new List<Object[]>();
            foreach (Object[] row in surcharge) {
                if (Convert.ToChar(row[2]) == 'D') {
                    delivery.Add(row);
                    deliveryTotal += (decimal)row[1];
                } else if (Convert.ToChar(row[2]) == 'G') {
                    generation.Add(row);
                    generationTotal += (decimal)row[1];
                } else {
                    filterSurcharge.Add(row);
                    surchargeTotal += (decimal)row[1];
                }
            }

            Object[] minimum = calculateMinimumCost(getBasicRate(conn, utilRateId, rateType, '0'), totalDays);
            if ((decimal)minimum[1] > generationTotal) {
                generation.Clear();
                generation.Insert(0, minimum);
                generationTotal = (decimal)minimum[1];
            }

            subtotal += (decimal)custCharge[1] + generationTotal + surchargeTotal + deliveryTotal;

            decimal taxTotal = 0M;
            List<Object[]> tax = calculateTaxCost(conn, 'E', taxList, (decimal)custCharge[1] + generationTotal + deliveryTotal);
            foreach (Object[] item in tax) taxTotal += (decimal)item[1];

            details["CustCharge"] += (decimal)custCharge[1];
            for (int i = 0; i < generation.Count; i++ ) {
                if (i == 0) details["Tier1"] += (decimal)generation[i][1];
                else if (i == 1) details["Tier2"] += (decimal)generation[i][1];
                else details["Tier3-5"] += (decimal)generation[i][1];
            }
            details["Surcharge"] += surchargeTotal;
            details["Tax"] += taxTotal;
            details["Total"] += (decimal)custCharge[1] + generationTotal + surchargeTotal + taxTotal;

            return details;
        }
        
        public Object[] calculateWatTotalCost(SqlConnection conn) {
            int utilRateId, tierSetId, totalDays, usage, numMed; char serviceType; String rateType;

            Object company = companyInfo[2];
            if (company == DBNull.Value) return new Object[] { null, new List<Object[]>(), 0M };
            int watCompanyId = (int)company;
            if ((bool)tenantInfo["DontBillForWat"]) return new Object[] { null, new List<Object[]>(), 0M };
            String utilRateCondition = "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND Dirty=0 ORDER BY EffectiveDate DESC";
            ArrayList utilRateCompany = DatabaseControl.getMultipleRecord(conn, utilInfoList, DatabaseControl.utilRateTable,
                utilRateCondition, new Object[] { watCompanyId, 'W' });
            Object[] utilCompany = (Object[])utilRateCompany[0];

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[1];

            rateType = tenantInfo["WatStatus"].ToString();
            serviceType = 'W';
            numMed = 0;
            if (rateType == "") { rateType = "Regular"; }

            usage = meterRead["WatReadValue"] - meterRead["PrevWatReadValue"];
            totalDays = (int)(end - start).TotalDays;

            List<Object[]> details = new List<Object[]>();

            Object[] baseline = new Object[] { null, getBaselineAllowanceWater(conn, utilRateId, '1', 'W'), 
                getBaselineAllowanceWater(conn, utilRateId, '2', 'W'), -1.0M };
            DateTime startDate, endDate;

            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            decimal subtotal = 0M;
            foreach (Object[] item in utilRateCompany) {
                decimal generationTotal, surchargeTotal;
                generationTotal = surchargeTotal = 0M;
                int numDays = 0;

                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                } else if (effDate <= end) {
                    numDays = (int)(endDate - effDate).TotalDays;
                }
                endDate = effDate;

                if (numDays <= 0) break;


                Object[] custCharge;
                String method = DatabaseControl.getSingleRecord(new String[] { "Method" }, DatabaseControl.utilRateTable, "UtilityRateID=@value0", new Object[] { utilRateId })[0].ToString();
                switch (method.ToLowerInvariant()) {
                    case "monthly":
                        custCharge = new Object[] { "Cust Charge", Decimal.Round(getBasicRate(conn, (int)item[0], rateType, serviceType), 2) };
                        break;
                    case "daily":
                        custCharge = calculateBasicCost(getBasicRate(conn, (int)item[0], rateType, serviceType), numDays);
                        break;
                    default:
                        custCharge = calculateBasicCost(getBasicRate(conn, (int)item[0], rateType, serviceType), numDays);
                        break;
                }

                List<Object[]> generation = calculateTierCost(getTierRate(conn, (int)item[0], 'G', rateType), baseline, usage, 1M, (decimal)numDays / totalDays);
                foreach (Object[] row in generation) if (row.Length != 1) generationTotal += (decimal)row[1];

                List<Object[]> surcharge = calculateSurcharges(getSurcharge(conn, (int)item[0], "All"), numDays, usage, generationTotal);
                surcharge.AddRange(calculateSurcharges(getSurcharge(conn, (int)item[0], rateType), numDays, usage, generationTotal));
                foreach (Object[] row in surcharge) surchargeTotal += (decimal)row[1];

                Object[] minimum = calculateMinimumCost(getBasicRate(conn, (int)item[0], rateType, '0'), numDays);
                if ((decimal)minimum[1] > generationTotal) {
                    generation.Clear();
                    generation.Insert(0, minimum);
                    generationTotal = (decimal)minimum[1];
                }

                details.Add(custCharge);
                details.AddRange(generation);
                details.AddRange(surcharge);

                subtotal += (decimal)custCharge[1] + generationTotal + surchargeTotal;
            }

            decimal taxTotal = 0M;
            List<Object[]> tax = calculateTaxCost(conn, 'G', taxList, subtotal);
            foreach (Object[] item in tax) taxTotal += (decimal)item[1];

            details.Add(new Object[] { "Subtotal", subtotal });
            details.AddRange(tax);

            Object[] pdfWat = new Object[] { null, details, subtotal + taxTotal };
            return pdfWat;
        }

        public Dictionary<string, decimal> calculateUtilWat(SqlConnection conn) {
            int utilRateId, tierSetId, totalDays, usage, numMed; char serviceType; String rateType;

            Dictionary<string, decimal> details = new Dictionary<string, decimal>();
            details["CustCharge"] = 0M;
            details["Base"] = 0M;
            details["OverBase"] = 0M;
            details["Surcharge"] = 0M;
            details["Tax"] = 0M;
            details["Total"] = 0M;

            Object company = companyInfo[2];
            if (company == DBNull.Value) return details;
            int watCompanyId = (int)company;
            if ((bool)tenantInfo["DontBillForWat"]) return details;
            String utilRateCondition = "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND Dirty=0 ORDER BY EffectiveDate DESC";
            ArrayList utilRateCompany = DatabaseControl.getMultipleRecord(conn, utilInfoList, DatabaseControl.utilRateTable,
                utilRateCondition, new Object[] { watCompanyId, 'W' });
            Object[] utilCompany = (Object[])utilRateCompany[0];

            utilRateId = (int)utilCompany[0];
            tierSetId = (int)utilCompany[1];

            rateType = tenantInfo["WatStatus"].ToString();
            serviceType = 'W';
            numMed = 0;
            if (rateType == "") { rateType = "Regular"; }

            usage = meterRead["WatReadValue"] - meterRead["PrevWatReadValue"];
            totalDays = (int)(end - start).TotalDays;

            Object[] baseline = new Object[] { null, getBaselineAllowanceWater(conn, utilRateId, '1', 'S'), getBaselineAllowanceWater(conn, utilRateId, '2', 'S'), -1.0M };
            DateTime startDate, endDate;

            startDate = start.AddDays(1);
            endDate = end.AddDays(1);
            decimal subtotal = 0M;
            foreach (Object[] item in utilRateCompany) {
                decimal generationTotal, surchargeTotal;
                generationTotal = surchargeTotal = 0M;
                int numDays = 0;

                DateTime effDate = ((DateTime)item[2]).Date;
                if (effDate <= startDate) {
                    numDays = (int)(endDate - startDate).TotalDays;
                } else if (effDate <= end) {
                    numDays = (int)(endDate - effDate).TotalDays;
                }
                endDate = effDate;

                if (numDays <= 0) break;


                Object[] custCharge;
                String method = DatabaseControl.getSingleRecord(new String[] { "Method" }, DatabaseControl.utilRateTable, "UtilityRateID=@value0", new Object[] { utilRateId })[0].ToString();
                switch (method.ToLowerInvariant()) {
                    case "monthly":
                        custCharge = new Object[] { "Cust Charge", Decimal.Round(getBasicRate(conn, (int)item[0], rateType, serviceType), 2) };
                        break;
                    case "daily":
                        custCharge = calculateBasicCost(getBasicRate(conn, (int)item[0], rateType, serviceType), numDays);
                        break;
                    default:
                        custCharge = calculateBasicCost(getBasicRate(conn, (int)item[0], rateType, serviceType), numDays);
                        break;
                }

                List<Object[]> generation = calculateTierCost(getTierRate(conn, (int)item[0], 'G', rateType), baseline, usage, 1M, (decimal)numDays / totalDays);
                foreach (Object[] row in generation) if (row.Length != 1) generationTotal += (decimal)row[1];

                List<Object[]> surcharge = calculateSurcharges(getSurcharge(conn, (int)item[0], "All"), numDays, usage, generationTotal);
                surcharge.AddRange(calculateSurcharges(getSurcharge(conn, (int)item[0], rateType), numDays, usage, generationTotal));
                foreach (Object[] row in surcharge) surchargeTotal += (decimal)row[1];

                Object[] minimum = calculateMinimumCost(getBasicRate(conn, (int)item[0], rateType, '0'), numDays);
                if ((decimal)minimum[1] > generationTotal) {
                    generation.Clear();
                    generation.Insert(0, minimum);
                    generationTotal = (decimal)minimum[1];
                }

                details["CustCharge"] += (decimal)custCharge[1];
                if (generation.Count > 0) details["Base"] += (decimal)generation[0][1];
                if (generation.Count > 1) details["OverBase"] += (decimal)generation[1][1];
                details["Surcharge"] += surchargeTotal;
                details["Total"] += (decimal)custCharge[1] + generationTotal + surchargeTotal;
            }

            decimal taxTotal = 0M;
            List<Object[]> tax = calculateTaxCost(conn, 'G', taxList, subtotal);
            foreach (Object[] item in tax) taxTotal += (decimal)item[1];

            details["Tax"] += taxTotal;

            return details;
        }


        public Object[] calculateBasicCost(decimal rate, int numDays) {
            return new Object[] { String.Format("Cust Chrg: {0:G29}x{1:G29}", numDays, rate), Decimal.Round(numDays * rate, 2) };
        }

        public Object[] calculateMinimumCost(decimal rate, int numDays) {
            return new Object[] { String.Format("Min Use: {0:G29}x{1:G29}", numDays, rate), Decimal.Round(numDays * rate, 2) };
        }

        public List<Object[]> calculateSurcharges(ArrayList surcharges, int days, decimal quantity, decimal subtotal) {
            List<Object[]> obj = new List<Object[]>();

            foreach (Object[] surcharge in surcharges) {
                decimal rate = (decimal)surcharge[2];
                int usage = (int)surcharge[1];
                String description = surcharge[0].ToString();

                switch (usage) {
                    case 1:
                        obj.Add(new Object[] { description, Decimal.Round(rate, 2), surcharge[3] });
                        break;
                    case 2:
                        obj.Add(new Object[] { String.Format(description + " {0}x{1}", quantity.ToString("G29"), rate.ToString("G29")), Decimal.Round(quantity * rate, 2), surcharge[3] });
                        break;
                    case 3:
                        obj.Add(new Object[] { String.Format(description + " {0}x{1}", days.ToString("G29"), rate.ToString("G29")), Decimal.Round(days * rate, 2), surcharge[3] });
                        break;
                    case 4:
                        obj.Add(new Object[] { String.Format(description + " {0}x{1}", subtotal.ToString("G29"), rate.ToString("G29")), Decimal.Round(subtotal * rate, 2), surcharge[3] });
                        break;
                }
            }
            return obj;
        }

        public List<Object[]> calculateTierCost(Object[] rate, Object[] tierPercentages, decimal usage, decimal baseline, decimal ratio) {
            List<Object[]> obj = new List<Object[]>();

            for (int i = minTier; i <= maxTier; i++) {
                decimal tierLimit = baseline * (decimal)tierPercentages[i];
                if (usage > tierLimit && ((decimal)tierPercentages[i] != -1.00M && (decimal)tierPercentages[i] != 0M)) {
                    usage -= tierLimit;
                    obj.Add(new Object[] { String.Format("Tr{0}: {1:G29}x{2:G29}", i, Decimal.Round(tierLimit * ratio, 2), (decimal)rate[i - 1]), Decimal.Round(tierLimit * ratio * (decimal)rate[i - 1], 2) });
                } else {
                    obj.Add(new Object[] { String.Format("Tr{0}: {1:G29}x{2:G29}", i, Decimal.Round(usage * ratio, 2), (decimal)rate[i - 1]), Decimal.Round(usage * ratio * (decimal)rate[i - 1], 2) });
                    break;
                }
            }
            return obj;
        }

        public List<Object[]> calculateBlendedTierCost(SqlConnection conn, List<Object[]> utilRateId, char chargeType, String rateType, int tierSetId, decimal usage, decimal baseline) {
            List<Object[]> obj = new List<Object[]>();
            decimal[] rate = { 0M, 0M, 0M, 0M, 0M };
            int totalDays = ((int)(end - start).TotalDays);
            if (totalDays == 0) return obj;
            DateTime startDate = start.AddDays(1);
            DateTime endDate = end.AddDays(1);
            Object[] tierPercentages = getTierPercentage(conn, tierSetId);
            foreach (Object[] item in utilRateId) {
                Object[] periodRate = getTierRate(conn, (int)item[0], chargeType, rateType);
                DateTime effDate = (DateTime)item[2];
                if (effDate < startDate) effDate = startDate;
                int numDays = (int)(endDate - effDate).TotalDays;
                for (int i = minTier; i <= maxTier; i++) {
                    if (numDays == 0) continue;
                    rate[i - 1] += (decimal)periodRate[i - 1] * numDays / totalDays;
                }
                endDate = effDate;
            }

            for (int i = minTier; i <= maxTier; i++) {
                decimal tierLimit;
                tierLimit = Decimal.Round(baseline * (decimal)tierPercentages[i], 2);
                if (usage > tierLimit && (decimal)tierPercentages[i] != -1.00M) {
                    usage -= tierLimit;
                    obj.Add(new Object[] { String.Format("Tr{0}: {1:N2}x{2:N5}", i, tierLimit, rate[i - 1]), Decimal.Round(tierLimit * rate[i - 1], 2) });
                } else {
                    obj.Add(new Object[] { String.Format("Tr{0}: {1:N2}x{2:N5}", i, usage, rate[i - 1]), Decimal.Round(usage * rate[i - 1], 2) });
                    break;
                }
            }

            return obj;
        }

        public List<Object[]> calculateTaxCost(SqlConnection conn, char utilType, String[] tax, decimal subtotal) {
            List<Object[]> obj = new List<Object[]>();
            foreach (String taxType in tax) {
                decimal rate = getTaxRate(conn, parkId, utilType, taxType);
                obj.Add(new Object[] { String.Format(taxType + " {0}x{1}", subtotal.ToString("G29"), rate.ToString("G29")), Decimal.Round(subtotal * rate, 2) });
            }
            return obj;
        }

        private decimal getBaselineAllowance(SqlConnection conn, int utilRateId, char serviceType, char zone, int numMed, int numDays) {
            String condition = "UtilityRateID=@value0 AND ServiceType=@value1 AND ClimateZone=@value2";
            decimal total = 0.0M;
            DateTime startDate = start;
            DateTime endDate = end;

            Object[] values = { utilRateId, serviceType, zone };
            decimal rate = (decimal)DatabaseControl.getSingleRecord(conn, new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, values)[0];
            decimal medA;
            try { medA = (decimal)DatabaseControl.getSingleRecord(conn, new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, new Object[] { utilRateId, 'M', 'M' })[0]; } catch { medA = 0; }
            DateTime effDate = (DateTime)DatabaseControl.getSingleRecord(conn, new String[] { "EffectiveDate" }, DatabaseControl.utilRateTable, "UtilityRateID=@value0", new Object[] { utilRateId })[0];
            if (effDate < startDate) effDate = startDate;
            total += (numDays * (rate + medA * numMed));
            endDate = effDate;

            return total;
        }

        private decimal getBaselineAllowanceWater(SqlConnection conn, int utilRateId, char serviceType, char zone) {
            String condition = "UtilityRateID=@value0 AND ServiceType=@value1 AND ClimateZone=@value2";
            Object[] values = { utilRateId, serviceType, zone };
            return (decimal)DatabaseControl.getSingleRecord(conn, new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, values)[0];
        }

        private Object[] getTierPercentage(SqlConnection conn, int tierSetId) {
            return DatabaseControl.getSingleRecord(conn, new String[] { "TierSetName", "Tier1", "Tier2", "Tier3", "Tier4", "Tier5" }, DatabaseControl.tierTable, "TierSetID=@value0",
                new Object[] { tierSetId });
        }

        private decimal getBasicRate(SqlConnection conn, int utilRateId, String status, char service) {
            String condition = "UtilityRateID=@value0 AND Status=@value1 AND Service=@value2";
            Object[] values = { utilRateId, status, service };
            Object[] result = DatabaseControl.getSingleRecord(conn, new String[] { "Rate" }, DatabaseControl.utilBasicRatesTable, condition, values);
            if (result == null) return 0M;
            else return (decimal)result[0];
        }

        private String getBasicDescription(SqlConnection conn, int utilRateId, char serviceType, String rateType) {
            String condition = "UtilityRateID=@value0 AND ServiceType=@value1 AND RateType=@value2";
            Object[] values = { utilRateId, serviceType, rateType };
            return DatabaseControl.getSingleRecord(conn, new String[] { "Description" }, DatabaseControl.utilBasicRatesTable, condition, values)[0].ToString();
        }

        private ArrayList getSurcharge(SqlConnection conn, int utilRateId, String rateType) {
            String condition = "UtilityRateID=@value0 AND RateType=@value1";
            Object[] values = { utilRateId, rateType };
            return DatabaseControl.getMultipleRecord(conn, new String[] { "Description", "Usage", "Rate", "ChargeType" }, DatabaseControl.utilSurchargeTable, condition, values);
        }

        private Object[] getTierRate(SqlConnection conn, int utilRateId, char chargeType, String rateType) {
            String condition = "UtilityRateID=@value0 AND ChargeType=@value1 AND RateType=@value2";
            Object[] values = { utilRateId, chargeType, rateType };
            Object[] result = DatabaseControl.getSingleRecord(conn, new String[] { "Rate1", "Rate2", "Rate3", "Rate4", "Rate5" },
                DatabaseControl.utilTierRatesTable, condition, values);
            if (result == null) return new Object[] { 0M, 0M, 0M, 0M, 0M };
            else return result;
        }

        private decimal getTaxRate(SqlConnection conn, int parkId, char utilType, String tax) {
            return Convert.ToDecimal(DatabaseControl.getSingleRecord(conn, new String[] { tax }, DatabaseControl.taxTable,
                "ParkID=@value0 AND UtilityType=@value1 ORDER BY TaxRateID DESC", new Object[] { parkId, utilType })[0]);
        }
    }
}
