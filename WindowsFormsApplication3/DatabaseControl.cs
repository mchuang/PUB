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
        public static String userTable = "[User]";
        public static String ownerTable = "Owner";
        public static String[] ownerColumns = {"CompanyName", "Contact", "Email", "PhoneOffice", "PhoneHome", "PhoneCellular",
                                               "Address1", "Address2", "City", "State", "Zip",
                                               "BillingAddress1", "BillingAddress2", "BillingCity", "BillingState", "BillingZip"};
        
        public static String parkTable = "Park";
        public static String[] parkColumns = {"ParkNumber", "ParkName", "Address", "City", "State", "ZipCode", "Phone", "Fax",
                                              "OwnerID", "ElectricityCompanyID", "EleZone", "GasCompanyID", "GasZone", "WaterCompanyID", "CsvID", "TopMessage", "BotMessage", "Clerk" };
        public static String optionsTable = "ChargeItem";
        public static String parkOptionsTable = "ParkChargeItem";
        public static String[] parkOptionsColumns = { "ParkID", "ChargeItemID", "ChargeItemValue" };
        public static String spaceBillTable = "ParkSpaceBill";
        public static String[] spaceBillColumns = { "ParkSpaceID", "DueDate", "GasPreviousRead", "GasCurrentRead", "GasUsage", "GasBill", "ElePreviousRead", "EleCurrentRead", "EleUsage", "EleBill", "WatPreviousRead", "WatCurrentRead", "WatUsage", "WatBill" };
        public static String spaceChargeTable = "ParkSpaceCharge";
        public static String spaceTempChargeTable = "ParkSpaceTempCharge";
        public static String[] spaceChargeColumns = { "ParkSpaceID", "DueDate", "Description", "ChargeItemValue", "ChargeItemID" };
        public static String[] spaceTempChargeColumns = { "ParkSpaceID", "DueDate", "Description", "ChargeItemValue" };
        public static String tempChargeTable = "ParkTemporaryCharges";
        public static String[] tempChargeColumns = { "ParkID", "Description", "Charge", "DateAssigned" };

        public static String taxTable = "TaxRates";
        public static String[] taxColumns = new String[] { "ParkID", "UtilityType", "InspectionFee", "CountyTax", "LocalTax", "StateTax", "RegTax" };
 
        public static String spaceTable = "ParkSpaceTenant";
        public static String[] spaceColumns = { "ParkID", "SpaceID", "Tenant", "MoveInDate", "MoveOutDate", "GasStatus", "GasType", "EleStatus", "EleType", "WatStatus", "GasMedical", "EleMedical", 
                                               "DontBillForGas", "DontBillForEle", "DontBillForWat",
                                               "Address1", "Address2", "City", "State", "Zip",
                                               "BillingAddress1", "BillingAddress2", "BillingCity", "BillingState", "BillingZip",
                                               "Email", "PhoneOffice", "PhoneHome", "PhoneCellular", "OrderID", "Concession" };
        
        public static String utilCompanyTable = "UtilityCompany";
        public static String[] utilCompanyColumns = {"CompanyName", "IsElectricity", "IsGas", "IsWater"};
        public static String utilRateTable = "UtilityRate";
        public static String[] utilRateColumns = { "UtilityCompanyID", "UtilityServiceType", "EffectiveDate", "Method", "Unit", "TierSetID", "Dirty" };
        public static String utilBasicRatesTable = "BasicRates";
        public static String[] utilBasicRatesColumns = { "UtilityRateID", "Status", "Service", "Rate" };
        public static String utilSurchargeTable = "Surcharge";
        public static String[] utilSurchargeColumns = { "UtilityRateID", "Description", "RateType", "ChargeType", "Usage", "Rate" };
        public static String utilTierRatesTable = "TierRates";
        public static String[] utilTierRatesColumns = { "UtilityRateID", "ChargeType", "RateType", "Rate1", "Rate2", "Rate3", "Rate4", "Rate5" };
        public static String baselineAllowanceTable = "BaselineAllowance";
        public static String[] baselineAllowanceColumns = { "UtilityRateID", "ServiceType", "ClimateZone", "BaselineAllowanceRate" };

        public static String meterReadsTable = "MeterReads";
        public static String[] meterReadsColumns = { "ParkID", "OrderID", "StartDate","MeterReadDate", "GasReadValue", "EleReadValue", "WatReadValue", "MeterReadEmployeeID", "MeterReadTime", "DueDate", "ThermX" };
        public static String meterInfoTable = "MeterLoc";
        public static String periodTable = "ParkPeriod";
        public static String[] periodColumns = { "ParkID", "StartDate", "MeterReadDate", "DueDate" };

        public static String tierTable = "Tier";
        public static String messageTable = "ParkMessage";
        public static String[] messageColumns = { "ParkID", "DueDate", "TopMessage", "BotMessage" };
        public static String insertSql = "INSERT INTO {0} ({1}) VALUES ({2});";
        public static String selectSql = "SELECT {0} FROM {1};";
        public static String selectWhereSql = "SELECT {0} FROM {1} WHERE {2};";
        public static String deleteSql = "DELETE FROM {0} WHERE {1}";
        public static String updateSql = "UPDATE {0} SET {1} WHERE {2}";
        public static String joinSql = " ({0} JOIN {1} ON {0}.{2}={1}.{3}) ";
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
            for (int i = 0; i < values.Length; i++)
            {
                valuesSet[i] = pair + i;
            }
            String query = String.Format(insertSql, table, String.Join(",", columns), String.Join(",", valuesSet)) + lastIdentity;

            return executeQueryWithParameters(query, values);
        }
        public static void executeInsertQueryNoID(String table, String[] columns, Object[] values)
        {
            String pair = "@value";
            String[] valuesSet = new String[values.Length];
            for(int i = 0; i < values.Length; i++)
            {
                valuesSet[i] = pair + i;
            }
            String query = String.Format(insertSql, table, String.Join(",", columns), String.Join(",",valuesSet)) + lastIdentity;

            executeNonQueryWithParameters(query, values);
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

        public static void populateComboBox(ref ComboBox list, String table, String text1, String text2, String value, String condition, Object[] values)
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

        public static Dictionary<String, Object> getSingleRecordDict(String[] field, String table, String condition, Object[] values = null) {
            String query = String.Format(DatabaseControl.selectWhereSql, String.Join(",", field), table, condition);
            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            if (values != null) { setSqlCommandParameters(ref cmd, values); }
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Object[] results = new Object[reader.FieldCount];
            Dictionary<String, Object> dict = new Dictionary<String, Object>();
            try { 
                reader.GetValues(results); 
                reader.Close(); 
                conn.Close();
                for (int i = 0; i < field.Length; i++ ) {
                    dict[field[i]] = results[i];
                }
                return dict; 
            } catch { 
                reader.Close(); 
                conn.Close();
                return new Dictionary<String, Object>(); 
            }
        }

        public static List<Object[]> getMultipleRecord(String[] field, String table, String condition, Object[] values = null)
        {
            List<Object[]> records = new List<Object[]>();
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

        public static ArrayList getMultipleRecordDict(String[] field, String table, String condition, Object[] values = null) {
            ArrayList records = new ArrayList();
            String query = String.Format(DatabaseControl.selectWhereSql, String.Join(",", field), table, condition);

            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            if (values != null) { setSqlCommandParameters(ref cmd, values); }
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Dictionary<String, Object> dict = new Dictionary<String, Object>();
                Object[] record = new Object[reader.FieldCount];
                reader.GetValues(record);
                for (int i = 0; i < field.Length; i++) {
                    dict[field[i]] = record[i];
                }
                records.Add(dict);
            }
            reader.Close();
            conn.Close();
            return records;
        }

        public static void deleteRecords(String table, String condition, Object[] values = null) {
            String query = String.Format(DatabaseControl.deleteSql, table, condition);
            executeNonQueryWithParameters(query, values);
        }

        public static bool userLogin(String username, String password)
        {
            String query = String.Format(DatabaseControl.selectWhereSql, "*", DatabaseControl.userTable, "LoginName=@value0 AND Password=@value1 AND Active ='Y'");
            SqlConnection conn = DatabaseControl.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            setSqlCommandParameters(ref cmd, new Object[] { username, password });
            Object result = cmd.ExecuteScalar();
            if (result == null) { return false; }
            else { return true; }
        }

        //USING SQLCONNECTION
        public static int executeInsertQuery(SqlConnection conn, String table, String[] columns, Object[] values) {
            String pair = "@value";
            String[] valuesSet = new String[values.Length];
            for (int i = 0; i < values.Length; i++) {
                valuesSet[i] = pair + i;
            }
            String query = String.Format(insertSql, table, String.Join(",", columns), String.Join(",", valuesSet)) + lastIdentity;

            return executeQueryWithParameters(query, values);
        }

        public static void executeUpdateQuery(SqlConnection conn, String table, String[] columns, Object[] updates, String condition) {
            String pair = "{0}=@value";
            String[] set = new String[updates.Length];
            for (int i = 0; i < columns.Length; i++) {
                set[i] = String.Format(pair + i, columns[i]);
            }
            String query = String.Format(updateSql, table, String.Join(",", set), condition);

            executeNonQueryWithParameters(conn, query, updates);
        }

        public static void executeUpdateQuery(SqlConnection conn, String table, String[] columns, Object[] updates, String condition, Object[] conditionValues) {
            String pair = "{0}=@value";
            String[] set = new String[updates.Length];
            for (int i = 0; i < columns.Length; i++) {
                set[i] = String.Format(pair + i, columns[i]);
            }
            String query = String.Format(updateSql, table, String.Join(",", set), condition);
            Object[] values = new Object[updates.Length + conditionValues.Length];
            updates.CopyTo(values, 0);
            conditionValues.CopyTo(values, updates.Length);

            executeNonQueryWithParameters(conn, query, values);
        }

        public static int executeQueryWithParameters(SqlConnection conn, String query, Object[] values) {
            SqlCommand cmd = new SqlCommand(query, conn);
            setSqlCommandParameters(ref cmd, values);
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            return id;
        }

        public static void executeNonQueryWithParameters(SqlConnection conn, String query, Object[] values) {
            SqlCommand cmd = new SqlCommand(query, conn);
            setSqlCommandParameters(ref cmd, values);
            cmd.ExecuteNonQuery();
        }

        public static Object[] getSingleRecord(SqlConnection conn, String[] field, String table, String condition, Object[] values = null) {
            String query = String.Format(DatabaseControl.selectWhereSql, String.Join(",", field), table, condition);
            SqlCommand cmd = new SqlCommand(query, conn);
            if (values != null) { setSqlCommandParameters(ref cmd, values); }
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Object[] results = new Object[reader.FieldCount];
            try { reader.GetValues(results); reader.Close(); return results; } catch { reader.Close(); return null; }
        }

        public static Dictionary<String, Object> getSingleRecordDict(SqlConnection conn, String[] field, String table, String condition, Object[] values = null) {
            String query = String.Format(DatabaseControl.selectWhereSql, String.Join(",", field), table, condition);
            SqlCommand cmd = new SqlCommand(query, conn);
            if (values != null) { setSqlCommandParameters(ref cmd, values); }
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Object[] results = new Object[reader.FieldCount];
            Dictionary<String, Object> dict = new Dictionary<String, Object>();
            try {
                reader.GetValues(results);
                reader.Close();
                for (int i = 0; i < field.Length; i++) {
                    dict[field[i]] = results[i];
                }
                return dict;
            } catch {
                reader.Close();
                return null;
            }
        }

        public static ArrayList getMultipleRecord(SqlConnection conn, String[] field, String table, String condition, Object[] values = null) {
            ArrayList records = new ArrayList();
            String query = String.Format(DatabaseControl.selectWhereSql, String.Join(",", field), table, condition);

            SqlCommand cmd = new SqlCommand(query, conn);
            if (values != null) { setSqlCommandParameters(ref cmd, values); }
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Object[] record = new Object[reader.FieldCount];
                reader.GetValues(record);
                records.Add(record);
            }
            reader.Close();
            return records;
        }

        public static ArrayList getMultipleRecordDict(SqlConnection conn, String[] field, String table, String condition, Object[] values = null) {
            ArrayList records = new ArrayList();
            String query = String.Format(DatabaseControl.selectWhereSql, String.Join(",", field), table, condition);

            SqlCommand cmd = new SqlCommand(query, conn);
            if (values != null) { setSqlCommandParameters(ref cmd, values); }
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Dictionary<String, Object> dict = new Dictionary<String, Object>();
                Object[] record = new Object[reader.FieldCount];
                reader.GetValues(record);
                for (int i = 0; i < field.Length; i++) {
                    dict[field[i]] = record[i];
                }
                records.Add(dict);
            }
            reader.Close();
            return records;
        }
        public static int getParkSpaceID(string ParkNumber, int SpaceID)
        {
            int ParkID = getParkID(ParkNumber);
            String query = "Select top 1 ParkSpaceID from ParkSpaceTenant where ParkID =@value0 and SpaceID =@value1 order by ParkSpaceID desc";
            object [] values = {ParkID.ToString(), SpaceID};
            try
            {
                return executeQueryWithParameters(query, values);;
            }
            catch 
            {
                return -1;
            }
            
        }
        public static int getParkSpaceID(int ParkID, int SpaceID)
        {
            String query = "Select top 1 ParkSpaceID from ParkSpaceTenant where ParkID =@value0 and SpaceID =@value1 order by ParkSpaceID desc";
            object[] values = { ParkID, SpaceID };
            try
            {
                return executeQueryWithParameters(query, values); ;
            }
            catch
            {
                return -1;
            }

        }
        public static int getParkSpaceID(int ParkID, string SpaceID)
        {
            String query = "Select top 1 ParkSpaceID from ParkSpaceTenant where ParkID =@value0 and SpaceID =@value1 order by ParkSpaceID desc";
            object[] values = { ParkID, SpaceID };
            try
            {
                return executeQueryWithParameters(query, values); ;
            }
            catch
            {
                return -1;
            }

        }
        public static int getParkID(string ParkNumber)
        {
            String query = "Select ParkID from Park where ParkNumber =@value0 ";
            object[] values = { ParkNumber };
            try
            {
                return executeQueryWithParameters(query, values); ;
            }
            catch
            {
                return -1;
            }

        }
        public static int getParkNumberByParkSpaceID(int ParkSpaceID)
        {
            String query = "SELECT ParkNumber FROM Park INNER JOIN ParkSpaceTenant ON Park.ParkID = ParkSpaceTenant.ParkID where parkSpaceID =@value0 ";
            object[] values = { ParkSpaceID };
            try
            {
                return executeQueryWithParameters(query, values); ;
            }
            catch
            {
                return -1;
            }

        }
        public static int getParkIDByParkSpaceID(int ParkSpaceID)
        {
            String query = "SELECT Park.ParkID FROM Park INNER JOIN ParkSpaceTenant ON Park.ParkID = ParkSpaceTenant.ParkID where parkSpaceID =@value0 ";
            object[] values = { ParkSpaceID };
            try
            {
                return executeQueryWithParameters(query, values); ;
            }
            catch
            {
                return -1;
            }

        }
        public static int getUtilCompanyID(string utilCompanyName)
        {
            String query = "Select top 1 UtilityCompanyID from UtilityCompany where CompanyNmae =@value0 order by UtilityCompanyID desc";
            object[] values = { utilCompanyName };
            try
            {
                return executeQueryWithParameters(query, values); ;
            }
            catch
            {
                return -1;
            }

        }
        public static int getUtilRateID(int utilCompanyID, char type, DateTime effectiveDate)
        {
            String query = "Select top 1 UtilityRateID from UtilityRate where utilityCompanyID =@value0  and UtilitySderviceType = @value1 and EffectiveDate = @value2 order by UtilityRateID desc";
            object[] values = { utilCompanyID, type, effectiveDate };
            try
            {
                return executeQueryWithParameters(query, values); ;
            }
            catch
            {
                return -1;
            }
        }
        public static string joinTables(String tableA, String tableB, String column) {
            return String.Format(joinSql, tableA, tableB, column, column);
        }
        public static string joinTables(String tableA, String tableB, String columnA, String columnB) {
            return String.Format(joinSql, tableA, tableB, columnA, columnB);
        }
    }
}
