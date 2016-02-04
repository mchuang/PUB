using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace PUB
{
    static class DatabaseControl
    {
        public static String ownerTable = "Owner";
        public static String[] ownerColumns = {"CompanyName", "Contact", "Email", "PhoneOffice", "PhoneHome", "PhoneCellular",
                                               "Address1", "Address2", "City", "State", "Zip",
                                               "BillingAddress1", "BillingAddress2", "BillingCity", "BillingState", "BillingZip"};
        
        public static String parkTable = "Park";
        public static String[] parkColumns = {"ParkNumber", "ParkName", "Address", "City", "State", "ZipCode", "Phone", "Fax",
                                              "OwnerID", "ElectricityCompanyID", "EleZone", "GasCompanyID", "GasZone", "WaterCompanyID"};
        public static String optionsTable = "ChargeItem";
        public static String parkOptionsTable = "ParkChargeItem";
        public static String[] parkOptionsColumns = { "ParkID", "ChargeItemID", "ChargeItemValue" };

        public static String taxTable = "TaxRates";
        public static String[] taxColumns = new String[] { "ParkID", "UtilityType", "InspectionFee", "CountyTax", "LocalTax", "StateTax", "RegTax" };
 
        public static String spaceTable = "ParkSpaceTenant";
        public static String[] spaceColumns = {"ParkID", "SpaceID", "Tenant", "MoveInDate", "MoveOutDate", "GasStatus", "GasType", "EleStatus", "EleType", "WatStatus", "MedStatus",
                                               "DontBillForGas", "DontBillForEle", "DontBillForWat",
                                               "Address1", "Address2", "City", "State", "Zip",
                                               "BillingAddress1", "BillingAddress2", "BillingCity", "BillingState", "BillingZip",
                                              "Email", "PhoneOffice", "PhoneHome", "PhoneCellular" };
        
        public static String utilCompanyTable = "UtilityCompany";
        public static String[] utilCompanyColumns = {"CompanyName", "IsElectricity", "IsGas", "IsWater"};
        public static String utilRateTable = "UtilityRate";
        public static String[] utilRateColumns = { "UtilityCompanyID", "UtilityServiceType", "EffectiveDate", "Method", "Unit", "HasWinter", "WinterStartDate", "WinterEndDate", "TierSetID" };
        public static String utilBasicRatesTable = "BasicRates";
        public static String[] utilBasicRatesColumns = { "UtilityRateID", "Status", "Service", "Rate" };
        public static String utilSurchargeTable = "Surcharge";
        public static String[] utilSurchargeColumns = { "UtilityRateID", "Description", "RateType", "Usage", "Rate" };
        public static String utilTierRatesTable = "TierRates";
        public static String[] utilTierRatesColumns = { "UtilityRateID", "Season", "ChargeType", "RateType", "Rate1", "Rate2", "Rate3", "Rate4", "Rate5" };

        public static String meterReadsTable = "MeterReads";
        public static String[] meterReadsColumns = { "ParkID", "SpaceID", "MeterReadDate", "GasReadValue", "EleReadValue", "WatReadValue", "GasReadValueOld", "EleReadValueOld", "WatReadValueOld",
                                                     "GasPrevMonthUsage", "ElePrevMonthUsage", "WatPrevMonthUsage", "GasPrevYearUsage", "ElePrevYearUsage", "WatPrevYearUsage",
                                                     "GasMeterLoc", "EleMeterLoc", "WatMeterLoc", "SpecialAccess", "MeterReadEmployeeID", "MeterReadTime" };

        public static String baselineAllowanceTable = "BaselineAllowance";
        public static String[] baselineAllowanceColumns = { "UtilityRateID", "ServiceType", "Season", "ClimateZone", "BaselineAllowanceRate" };

        public static String tierTable = "Tier";

        public static String insertSql = "INSERT INTO {0} ({1}) VALUES ({2});";
        public static String selectSql = "SELECT {0} FROM {1};";
        public static String selectWhereSql = "SELECT {0} FROM {1} WHERE {2};";
        public static String updateSql = "UPDATE {0} SET {1} WHERE {2}";
        public static String lastIdentity = "Select Scope_Identity();";
        
        //Database and login credentials
        private static String credentials = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
            ConfigurationManager.AppSettings.Get("Server"), ConfigurationManager.AppSettings.Get("Database"),
            ConfigurationManager.AppSettings.Get("User"), ConfigurationManager.AppSettings.Get("Password"));

        //Returns a new connection
        public static SqlConnection Connect() {
            return new SqlConnection(credentials);
        }

        //Sets the parameters of a referenced SqlCommand with the given array of values
        //Assumes the parameters follow format of @value# where # is some integer
        public static void setSqlCommandParameters(ref SqlCommand cmd, Object[] values)
        {
            int i = 0;
            foreach (Object val in values)
            {
                if (val == null) { cmd.Parameters.Add(new SqlParameter("value" + i, DBNull.Value)); }
                else { cmd.Parameters.Add(new SqlParameter("value" + i, val)); }
                i++;
            }
        }

        public static int executeInsertQuery(String table, String[] columns, Object[] values)
        {
            String pair = "@value";
            String[] valuesSet = new String[values.Length];
            for(int i = 0; i < values.Length; i++)
            {
                valuesSet[i] = pair + i;
            }
            String query = String.Format(insertSql, table, String.Join(",", columns), String.Join(",",valuesSet)) + lastIdentity;

            return executeQueryWithParameters(query, values);
        }

        public static void executeUpdateQuery(String table, String[] columns, Object[] updates, String condition)
        {
            String pair = "{0}=@value";
            String[] set = new String[updates.Length];
            for(int i = 0; i < columns.Length; i++)
            {
                set[i] = String.Format(pair+i, columns[i]);
            }
            String query = String.Format(updateSql, table, String.Join(",",set), condition);

            executeNonQueryWithParameters(query, updates);
        }

        public static void executeUpdateQuery(String table, String[] columns, Object[] updates, String condition, Object[] conditionValues) {
            String pair = "{0}=@value";
            String[] set = new String[updates.Length];
            for (int i = 0; i < columns.Length; i++) {
                set[i] = String.Format(pair + i, columns[i]);
            }
            String query = String.Format(updateSql, table, String.Join(",", set), condition);
            Object[] values = new Object[updates.Length + conditionValues.Length];
            updates.CopyTo(values, 0);
            conditionValues.CopyTo(values, updates.Length); 

            executeNonQueryWithParameters(query, values);
        }

        public static int executeQueryWithParameters(String query, Object[] values)
        {
            SqlConnection conn = Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            setSqlCommandParameters(ref cmd, values);
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return id;
        }

        public static void executeNonQueryWithParameters(String query, Object[] values)
        {
            SqlConnection conn = Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            setSqlCommandParameters(ref cmd, values);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void populateComboBox(ref ComboBox list, String table, String text, String value)
        {
            String itemCombo = value + "," + text;
            String query = String.Format(DatabaseControl.selectSql, itemCombo, table);

            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader.GetValue(0);
                string name = reader.GetValue(1).ToString();
                list.Items.Add(new CommonTools.Item(id, name)); 
            }
            reader.Close();
            conn.Close();
        }

        public static void populateComboBox(ref ComboBox list, String table, String text, String value, String condition, Object[] values)
        {
            String itemCombo = value + "," + text;
            String query = String.Format(DatabaseControl.selectWhereSql, itemCombo, table, condition);

            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            setSqlCommandParameters(ref cmd, values);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader.GetValue(0);
                string name = reader.GetValue(1).ToString();
                list.Items.Add(new CommonTools.Item(id, name)); 
            }
            reader.Close();
            conn.Close();
        }

        public static void populateComboBox(ref ComboBox list, String table, String value, String text1, String text2, String condition, Object[] values)
        {
            String itemCombo = String.Join(",", new Object[] { value, text1, text2});
            String query = String.Format(DatabaseControl.selectWhereSql, itemCombo, table, condition);

            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            setSqlCommandParameters(ref cmd, values);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Items.Add(new CommonTools.Item((int)reader.GetValue(0), reader.GetValue(1).ToString() + ":" + reader.GetValue(2).ToString()));
            }
            reader.Close();
            conn.Close();
        }

        public static void populateDataTable(ref System.Data.DataTable dataTable, String field, String table)
        {
            String query = String.Format(DatabaseControl.selectSql, field, table);
            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            conn.Close();
        }

        public static void populateDataTable(ref System.Data.DataTable dataTable, String field, String table, String condition, Object[] values = null)
        {
            String query = String.Format(DatabaseControl.selectWhereSql, field, table, condition);
            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            if (values != null) { setSqlCommandParameters(ref cmd, values); }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            conn.Close();
        }

        public static Object[] getSingleRecord(String[] field, String table, String condition, Object[] values=null)
        {
            String query = String.Format(DatabaseControl.selectWhereSql, String.Join(",",field), table, condition);
            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            if (values != null) { setSqlCommandParameters(ref cmd, values); }
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Object[] results = new Object[reader.FieldCount];
            try { reader.GetValues(results); reader.Close(); conn.Close(); return results; }
            catch { reader.Close(); conn.Close(); return null; }
        }

        public static ArrayList getMultipleRecord(String[] field, String table, String condition, Object[] values = null)
        {
            ArrayList records = new ArrayList();
            String query = String.Format(DatabaseControl.selectWhereSql, String.Join(",", field), table, condition);

            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            if (values != null) { setSqlCommandParameters(ref cmd, values); }
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Object[] record = new Object[reader.FieldCount];
                reader.GetValues(record);
                records.Add(record);
            }
            reader.Close();
            conn.Close();
            return records;
        }
    }
}
