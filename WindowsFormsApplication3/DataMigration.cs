using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Reflection;
using System.Collections;

namespace PUB
{
    
    public partial class DataMigration : Form
    {
        int parkID;
        int parkIDSaved;
        int failed;
        int inserted;
        bool parkIDValidated = false;
        DateTime utilStartDate = new DateTime(1965, 05, 23);
        String[] ownerColumns = { "CompanyName", "Contact", "PhoneOffice","Address1","City","State","Zip" };
        String[] parkColumns = { "ParkNumber", "ParkName","Address","City","State","ZipCode","Phone","Fax","OwnerID","GasCompanyID","ElectricityCompanyID" };
        String[] spaceColumns = { "ParkID", "SpaceID", "Tenant" };
        public const string isEleDB = "NutiEle";
        public const string isGasDB = "NutiGas";
        public const string isWatDB = "NutiWat";
        public const int typeIsOwner = 0;
        public const int typeIsPark = 1;
        public const int typeIsSpaceAndTenant = 2;
        public const int typeIsBilling = 3;
        public int messageColumn = -1;
        public int eleMedAlloanceColumn = -1;
        public int DWRBCColumn = -1;
        public int FERADiscountColumn = -1; 
        public const char statusL = 'L';  // Low Income
        public const char statusS = 'S';  // Senior Citizen
        public const char statusF = 'F';  // FERA
        public const char statusM = 'M';  // Medical
        public class importType
        {
            public string Name { get; set; }
            public string ID { get; set; }
        }
        public bool CheckIfOwnerExist(string owner)
        { 
            object [] ownerValue = { owner};
            object [] result = DatabaseControl.getSingleRecord(ownerColumns,DatabaseControl.ownerTable,"CompanyName = @value0",ownerValue);
            if (result == null)
            { return false; }
            else 
            { return true; }
             
        }
        public int GetOwnerID(string owner)
        {
            object[] ownerValue = { owner };
            String[] ownerColumns = { "OwnerID", "CompanyName" };
            object[] result = DatabaseControl.getSingleRecord(ownerColumns, DatabaseControl.ownerTable, "CompanyName = @value0", ownerValue);
            if (result == null)
            { return -1; }
            else
            { return Convert.ToInt32(result[0]); }

        }
        public int GetParkID(string parkNumber)
        {
            object[] parkValue = { parkNumber };
            String[] parkColumns = { "ParkID", "ParkNumber" };
            object[] result = DatabaseControl.getSingleRecord(parkColumns, DatabaseControl.parkTable, "ParkNumber = @value0", parkValue);
            if (result == null)
            { return -1; }
            else
            {   return Convert.ToInt32(result[0]);}

        }
        public int GetParkIDForTenant(string parkIDNum)
        {
            // For Tenant file Import, we want to call the database only once per tenenat file (bad assumption?)
            if (parkIDValidated) { return parkIDSaved; }
            object[] parkValue = { parkIDNum };
            String[] parkColumns = { "ParkID", "ParkNumber" };
            object[] result = DatabaseControl.getSingleRecord(parkColumns, DatabaseControl.parkTable, "ParkNumber = @value0", parkValue);
            if (result == null)
            { return -1; }
            else
            {
                parkIDSaved = Convert.ToInt32(result[0]);
                parkIDValidated = true;
                return parkIDSaved;
            }

        }
        public bool CheckIfParkExist(string park)
        {
            object[] parkValue = { park};
            object[] result = DatabaseControl.getSingleRecord(parkColumns, DatabaseControl.parkTable, "ParkNumber = @value0", parkValue);
            if (result == null)
            { return false; }
            else
            { return true; }

        }

        public bool CheckIfSpaceAndTenantExist(int parkID, string spaceID, string Tenant)
        {
            object[] spaceValue = { parkID, spaceID, Tenant };
            object[] result = DatabaseControl.getSingleRecord(spaceColumns, DatabaseControl.spaceTable, "ParkID = @value0 and SpaceID = @value1 and Tenant =@value2", spaceValue);
            if (result == null)
            { return false; }
            else
            { return true; }

        }
        public void ImportOwner(ref string[] fields)
        {
            if (CheckIfOwnerExist(fields[0]))  
            {
                //MessageBox.Show("Owner name " + fields[0] + " exists in the database");
                failed++; 
                return;
            }
            //string companyName = fields[0];
            //string contact = fields[1];
            // CSV format "CompanyName", "Contact", "PhoneOffice","Address1","City","State","Zip"

            Object[] ownerValues = {fields[0],fields[1],fields[2],fields[3],fields[4],fields[5],fields[6]};
            try
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.ownerTable, ownerColumns, ownerValues);
                inserted++;
            }
            catch { MessageBox.Show("Owner Import failed!"); }
                        
        }
        public void ImportPark(ref string[] fields)
        {
           
            if (CheckIfParkExist(fields[0]))
            {
                //MessageBox.Show("Park Number " + fields[0] + " exists in the database");
                failed++;
                return;
            }
            int ownerID = GetOwnerID(fields[9]);
            
            // CSV Format "ParkNumber", "ParkName","Address","City","State","ZipCode","Phone","Fax","unkown","Owner","Gas","Elec"
            Object[] parkValues = { fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6],
                                  fields[7],ownerID,fields[10],fields[11]};
            try
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.parkTable, parkColumns, parkValues);
                inserted++;
            }
            catch { MessageBox.Show("Park Import failed!"); }
        }
        
        public void ImportSpaceAndTenant(ref string[] fields) 
        {
            int x = GetParkIDForTenant (fields[0]);
            if (x == -1) {MessageBox.Show("Invalid Park Number in CSV file!"); return;}

            string spaceID = fields[1];
            string tenant = fields[2];
            
            if (CheckIfSpaceAndTenantExist(x, spaceID, tenant))
            {
                //MessageBox.Show("Space/Tenant " + fields[1] +" " + fields[2] + " exists in the database");
                failed++;
                return;
            }
            
            Object[] spaceValues = { x, spaceID, tenant };
            try
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.spaceTable, spaceColumns, spaceValues);
                inserted++;
            }
            catch 
            { 
                MessageBox.Show("Space/Tenant Import failed!" + tenant);
                failed++;
            }
        }
        public void ImportBilling(ref string[] fields) { }

        public DataMigration()
        {
            InitializeComponent();
            buildTypeComboBox();
        }
        public void buildTypeComboBox()
        {
            var dataSource = new List<importType>();
            dataSource.Add(new importType() { Name = "Owner", ID = typeIsOwner.ToString() });
            dataSource.Add(new importType() { Name = "Park", ID = typeIsPark.ToString() });
            dataSource.Add(new importType() { Name = "Space and Tenant", ID = typeIsSpaceAndTenant.ToString() });
            dataSource.Add(new importType() { Name = "Billing", ID = typeIsBilling.ToString() });
            //Setup data binding
            this.typeComboBox.DataSource = dataSource;
            this.typeComboBox.DisplayMember = "Name";
            this.typeComboBox.ValueMember = "ID";
            typeComboBox.SelectedIndex = 0;
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable + " ORDER BY ParkNumber", "ParkNumber", "ParkID");
        }

        private void inputFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inputFile.Text = openFileDialog1.FileName;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            int i = typeComboBox.SelectedIndex;
            using (TextFieldParser csvParser = new TextFieldParser(inputFile.Text))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row if the first row has the column names
               
                if (headerComboBox.Text == "Yes")
                {
                    csvParser.ReadLine();
                }
                inserted = 0;
                failed = 0;
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    if (fields.Length == 1) break; // real record should have more than one column
                    int count = 0;
                    parkIDValidated = false;
                    foreach (string field in fields)
                    {
                        if (field == "") { fields[count] = null; }
                        count = count + 1;
                    }
                    switch (i)
                    {
                        case typeIsOwner:
                            ImportOwner(ref fields);
                            break;
                        case typeIsPark:
                            ImportPark(ref fields);
                            break;
                        case typeIsSpaceAndTenant:
                            ImportSpaceAndTenant(ref fields);
                            break;
                        case typeIsBilling:
                            ImportBilling(ref fields);
                            break;
                        default:
                            break;
                    }
                   }
                string s = "File imported with " + inserted.ToString() + " records! ";
                if (failed > 0)
                {
                    s= s + " and " + failed.ToString() + " records failed!";
                }
                MessageBox.Show(s);
                inputFile.Text = "";
                typeComboBox.Text = "";
                headerComboBox.Text = "";
                //parkLabel.Visible = false;
                //parkList.Visible = false;
            }
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.parkID = ((CommonTools.Item)parkList.SelectedItem).Value;
        }

        private void typeComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //parkLabel.Visible = false;
            //parkList.Visible = false;
            //if ((typeComboBox.SelectedIndex == typeIsSpaceAndTenant) |
            //    (typeComboBox.SelectedIndex == typeIsBilling ))
            //{
            //    parkLabel.Visible = true;
            //    parkList.Visible = true;
            //}
        }
        
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //This procedure imports only tenant related records
            DateTime starttime = DateTime.Now;
            string path = "";

            // user spaceifies directory path to override the defaults
            if (dirText.Text == "")
            { path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "data"); }
            else
            { path = dirText.Text; }

            int counter;
            if (importOneMonth.Checked == true)
            { counter = 1; }
            else
            { counter = 15; }

            for (int i = 0; i < counter; i = i + 1)
            {
                // we set up the path to include subfolders 0 - 13 that contains Park DB files and NUTIELE.DB 
                // but if we only import one month data, we just use the folder sepcified on gui
                string pathi;
                if (importOneMonth.Checked == true)
                {
                    pathi = path;
                }
                else
                {
                    pathi = Path.Combine(path, i.ToString());
                }
                
                // user specifies one park to extract 
                if (parkList.Text != "")
                {
                    // Extract data for one park only
                    string fileName = parkList.Text + ".db";
                    if (File.Exists(Path.Combine(pathi, fileName)))
                    {
                        ImportTenantandBill(parkList.Text, pathi);
                    }
                    else
                    {
                        MessageBox.Show("Park file " + fileName + " does not exist!");
                        return;
                    }
                }
                else
                {
                    //to loop through the park list in database and extract one by one
                    List<Object[]> parks = new List<Object[]>();
                    parks = DatabaseControl.getMultipleRecord(new String[] { "ParkNumber" }, DatabaseControl.parkTable, "ParkNumber IS NOT NULL");
                    foreach (Object[] park in parks)
                    {
                        if (Convert.ToInt32(park[0].ToString()) != 156)
                        {
                            string parkNumber = park[0].ToString();
                            string fileName = park[0].ToString() + ".db";
                            if (File.Exists(Path.Combine(pathi, fileName)))
                            {
                                ImportTenantandBill(parkNumber, pathi);
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Park DB Extraction is completed! Time spent from " + starttime.ToString() + " to " + DateTime.Now.ToString());
        }
        
        private int insertUtilCompany(ParadoxRecord rec, string serviceType)
        {
            // Check if the utility company exist in database
            // if not, insert a new one
            char type = serviceType[4]; //'E' or 'G' or 'W'
            bool ele = false;
            bool gas = false;
            bool wat = false;
            switch (type)
            {
                case 'E':
                    ele = true;
                    break;
                case 'G':
                    gas = true;
                    break;
                case 'W':
                    wat = true;
                    break;
                default:
                    break;
            }

            // utilCompanyColumns = {"CompanyName", "IsElectricity", "IsGas", "IsWater"};
            Object[] values = {  rec.DataValues[1],      //Utility Company Name
                                 ele,gas,wat  };
            try
            {
                return DatabaseControl.executeInsertQuery(DatabaseControl.utilCompanyTable, DatabaseControl.utilCompanyColumns, values);
            }
            catch
            {
                return -1;
            }

        }
        private int insertUtilRate(string[] rec, char serviceType, int utilCompanyID, string path, int tierSetID)
        {
            //For CSV file to use
            utilStartDate = getStartDate(path);
            int result = 0;
            Object[] values = {  utilCompanyID,
                                 serviceType,
                                 utilStartDate,             //Effective Date
                                 rec[4],                    //Method
                                 rec[5],                    //Unit;
                                 tierSetID,                 //Tier Set ID
                                 0                          //dirty bit
                              };
            try
            {
                result = DatabaseControl.executeInsertQuery(DatabaseControl.utilRateTable, DatabaseControl.utilRateColumns, values);
            }
            catch
            {
                MessageBox.Show("System Error 98 - Insert Utility Rate failed");
                result = -1;
            }
            return result;
        }
        
        private int insertUtilRate(ParadoxRecord rec, string serviceType, int utilCompanyID, string path)
        {
            //For input file of Paradox Tables
            utilStartDate = getStartDate(path);
            char type = serviceType[4]; //The fifth letter:'E' or 'G' or 'W'
            int result = 0;
            Object[] values = {  utilCompanyID,
                                 type,
                                 utilStartDate,             //Effective Date
                                 rec.DataValues[3],         //Method
                                 rec.DataValues[4],         //Unit;
                                 1,                         //Tier Set ID
                                 0                          //dirty bit
                              };
            try
            {
                result = DatabaseControl.executeInsertQuery(DatabaseControl.utilRateTable, DatabaseControl.utilRateColumns, values);
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        private int insertSurcharge(ParadoxRecord rec, int utilityRateID)
        {
            //utilSurchargeColumns = { "UtilityRateID", "Description", "RateType", "ChargeType", "Usage", "Rate" };
            Object[] values = {  utilityRateID,    
                                 "DWR BC",      //Description
                                 "All",         //Tenant Type, Regular, CARE, Medical, ...
                                 'D',
                                 2,             // 1- Flat Rate 2- Usage 3-Daily
                                 rec.DataValues[DWRBCColumn]
                              };
            /* SCE unique
             * if (DWRBCColumn > 0)
            {
                try
                {
                    return DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
                }
                catch
                {
                    return -1;
                }
            }
            */
           
            if (FERADiscountColumn > 0)
            {
                    try
                    {
                        values[0] = utilityRateID;
                        values[1] = "FERA Discount";
                        values[2] = "All";
                        values[3] = "D";
                        values[4] = 2;
                        values[5] = rec.DataValues[FERADiscountColumn];
                        return DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
                    }
                    catch
                    {
                        return -1;
                    }
            }
            try
            {
                values[0] = utilityRateID;
                values[1] = "State Tax";
                values[2] = "All";
                values[3] = "D";
                values[4] = 2;
                values[5] = rec.DataValues[73];
                return DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
            }
            catch
            {
                return -1;
            }
        }
        private DateTime getStartDate(string path  )
        {
            string fileLocation = Path.Combine(path, "156.DB");
            string localParkNumber = "156"; //156 for Yvonne data, 303 for Sandy
            if (File.Exists(Path.Combine(path, "156.DB"))) { localParkNumber = "156"; }
            if (File.Exists(Path.Combine(path, "303.DB"))) { localParkNumber = "303"; }
            // The utilstartDate 
            DateTime localStartDate = new DateTime(1965, 05, 23);
            var table = new ParadoxTable(path, localParkNumber);  //156 for Yvonne data, 303 for Sandy
            int count = 0;
            
            foreach (var rec in table.Enumerate())
            {
                count = count +1;
                if (count > 1) { break; }
                else
                {
                    return (DateTime)rec.DataValues[18];  // 18 is the New Date in the Park table which is the new meter read date
                }                                           // this is the start date we use to calculate the utility bill
            }
            return localStartDate;
        }
        private void insertTierRateforPGE(ParadoxRecord rec, int utilRateID)
        {
            Object[] value = {  utilRateID,
                                 "G",                         // Generation charge
                                 "Regular",                   // Type of user
                                 rec.DataValues[23],          // Regulau Tier Rate 1
                                 rec.DataValues[45],          // Regulau Tier Rate 2
                                 rec.DataValues[67],          // Regulau Tier Rate 3
                                 rec.DataValues[14],          // Regulau Tier Rate 4
                                 rec.DataValues[76]           // Regulau Tier Rate 5
                              };
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

            value[2] = "FERA";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

            value[2] = "CARE";
            value[3] = rec.DataValues[34];
            value[4] = rec.DataValues[57];
            value[5] = rec.DataValues[69];
            value[6] = rec.DataValues[15];
            value[7] = rec.DataValues[77];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

            value[2] = "CARE/Medical";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);
            
            value[2] = "Medical";
            value[3] = rec.DataValues[33];
            value[4] = rec.DataValues[56];
            value[5] = rec.DataValues[71];
            value[6] = rec.DataValues[16];
            value[7] = rec.DataValues[78];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);
         
        }
        private void insertTierRate(object[] tierValue, char tierType, string userType, int utilRateID)
        {
            Object[] value = {   utilRateID,
                                 tierType,              // Generation or Delivery charge
                                 userType,              // Type of user, Regular, CARE, Medical, ...
                                 tierValue[0],          // Regulau Tier Rate 1
                                 tierValue[1],          // Regulau Tier Rate 2
                                 tierValue[2],          // Regulau Tier Rate 3
                                 tierValue[3],          // Regulau Tier Rate 4
                                 tierValue[4],          // Regulau Tier Rate 5
                              };
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);
        }
        private void insertTierRateforSCE(ParadoxRecord rec, int utilRateID)
        {
            Object[] value  = {  utilRateID,
                                 "D",                         // Generation charge
                                 "Regular",                   // Type of user
                                 rec.DataValues[16],          // Regulau Tier Rate 1
                                 rec.DataValues[36],          // Regulau Tier Rate 2
                                 rec.DataValues[47],          // Regulau Tier Rate 3
                                 rec.DataValues[72],          // Regulau Tier Rate 4
                                 rec.DataValues[76]           // Regulau Tier Rate 5
                              };
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);
            
            value[2] = "Medical";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

            value[2] = "CARE";
            value[3] = rec.DataValues[24];
            value[4] = rec.DataValues[46];
            value[5] = rec.DataValues[68];
            value[6] = rec.DataValues[70];
            value[7] = rec.DataValues[77];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

            value[2] = "CARE/Medical";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

            value[1] = "G";
            value[2] = "Regular";
            value[3] = rec.DataValues[23];
            value[4] = rec.DataValues[45];
            value[5] = rec.DataValues[67];
            value[6] = rec.DataValues[14];
            value[7] = rec.DataValues[25];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

            value[2] = "CARE";
            value[3] = rec.DataValues[34];
            value[4] = rec.DataValues[57];
            value[5] = rec.DataValues[69];
            value[6] = rec.DataValues[15];
            value[7] = rec.DataValues[29];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

            value[2] = "Medical";
            value[3] = rec.DataValues[31];
            value[4] = rec.DataValues[54];
            value[5] = rec.DataValues[43];
            value[6] = rec.DataValues[65];
            value[7] = rec.DataValues[32];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

            value[2] = "CARE/Medical";
            value[3] = rec.DataValues[27];
            value[4] = rec.DataValues[50];
            value[5] = rec.DataValues[39];
            value[6] = rec.DataValues[61];
            value[7] = rec.DataValues[28];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, value);

        }
        
        private void insertBaselineAllowance(ParadoxRecord rec, int utilRateID)
        {
            DateTime summerStartDate = new DateTime(2015, 05, 01);
            DateTime winterStartDate = new DateTime(2015, 11, 01);
            int x = 113; //winter allowance rate column starts here
            int y = 82;  //winter medical baseline allownace column
            if ((utilStartDate > summerStartDate) & (utilStartDate < winterStartDate)) // 
            {
                x = 93; //summer allowance rate column starts here
                y = 81; //winter medical baseline allownace column
            }
            for (int i = 0; i < 10; i = i + 1)
                {   Object[] value = 
                                    {  utilRateID,
                                    '1',                           // All Services allowance
                                    i,                             // Zone
                                    rec.DataValues[x+i]            // Allowance
                                    };
                    DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, value);
                    value[1] = '2';                                 // All Electricity Services allowance
                    value[3] = rec.DataValues[x-10+i];
                    DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, value);
                }
            Object[] valueM = {  utilRateID,
                                 'M',                           // Medical Baseline Allowance
                                 'M',                           // Zone
                                 rec.DataValues[y]             // Allowance
                                 
                              };
            DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, valueM);
        }
        private void insertBaselineAllowance(char serviceType, char zone, int utilRateID, double allowance)
        {
            Object[] value =  {     utilRateID,
                                    serviceType,         // All Services allowance
                                    zone,                // Zone
                                    allowance            // Allowance
                                    };
            DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, value);
               
        }
        private void insertBasicRateforPGE(ParadoxRecord rec, int utilRateID)
        {
            Object[] value = {  utilRateID,
                                 "Regular",                 
                                 0,                          //minimum rate
                                 rec.DataValues[20]          //Regulau Rate
                              };
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "FERA";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
            
            value[1] = "CARE";
            value[3] = rec.DataValues[21];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "CARE/Medical";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "Medical";
            value[3] = rec.DataValues[22];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
            
            value[1] = "Regular";
            value[2] = 1;                                   // Basic rate
            value[3] = rec.DataValues[5];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "FERA";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "CARE";
            value[3] = rec.DataValues[6];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "CARE/Medical";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "Medical";
            value[3] = rec.DataValues[7];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

        }
        private void insertBasicRateforSCE(ParadoxRecord rec, int utilRateID)
        {
            Object[] value = {  utilRateID,
                                 "Regular",                 
                                 0,                          //minimum rate
                                 rec.DataValues[20]          //Regulau Rate
                              };
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "Medical";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "CARE";
            value[3] = rec.DataValues[21];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "CARE/Medical";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);

            value[1] = "Regular";
            value[2] = 1;                                   // Basic rate
            value[3] = rec.DataValues[5];
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
            
            value[1] = "Medical";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
            
            value[1] = "CARE";
            value[3] = rec.DataValues[6]; 
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
            
            value[1] = "CARE/Medical";
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
            
        }
        private int ImportTenantandBill(string ParkNumber, string dbPath)
        {
            int localParkID = GetParkID(ParkNumber);
            if (localParkID == -1)
            { 
                MessageBox.Show("Park record missing in Database - " + ParkNumber.ToString());
                return -1;
            }
            var table = new ParadoxTable(dbPath, ParkNumber);
            // find out whicch column index is used for electricity multiple medical allowances
            messageColumn = -1;
            eleMedAlloanceColumn = -1;
            for (int i = 0; i < table.FieldCount; i= i+1 )
            {
                if (table.FieldNames[i] == "Top mess1")
                {
                    messageColumn = i; // it is supposed to be 118
                    break;
                }
                
                if (table.FieldNames[i] == "ELE MULT ALLOW")
                {
                    eleMedAlloanceColumn = i;
                    break;
                }
            };
            
            // now we process each tenant space record in the park
            int count = 0;
            foreach (var rec in table.Enumerate())
            {
                int parkSpaceID = 0;
                
                // check if the parkSpacetenant already exist in database
                try
                {
                    if (count == 0)
                    { 
                        count = count + 1;
                        insertMessage(rec,localParkID, messageColumn);
                        insertPeriodRecord(localParkID, rec);
                        insertTaxRate(localParkID);
                    }
                    string spaceName = " ";
                    if (rec.DataValues[1] is DBNull)
                    {
                        spaceName = rec.DataValues[0].ToString();
                    }
                    else
                    {
                        spaceName = rec.DataValues[1].ToString();
                    }
                    parkSpaceID = DatabaseControl.getParkSpaceID(localParkID, spaceName);
                }
                catch
                {
                    parkSpaceID = -1;
                }
               
                if (parkSpaceID < 1)
                {
                    // parkspace does not exist, need to insert tenant record and meter location record
                    parkSpaceID = insertTenantRecord(localParkID, rec);
                    insertMeterLocationRecord(localParkID, rec);
                    
                }
                else
                {
                    int a1, a2;
                    // parkspace exist already, need to check if it is still the same tenant
                    object[] result = DatabaseControl.getSingleRecord(new string[] { "Tenant" }, "ParkSpaceTenant", "ParkSpaceID=@value0", new object[] { parkSpaceID });
                    if (result[0].ToString() == rec.DataValues[5].ToString())
                    {
                        // tenant name is the same as before
                        a1 = updateTenantRecord(localParkID, parkSpaceID, rec);
                    }

                    else
                    {
                        // new tenant --> need to update the old tenant move-out date and create tenant record for the new tenant 
                        a2 = updateTenantRecordWithMoveOutDate(localParkID, parkSpaceID, rec);
                        parkSpaceID = insertTenantRecord(localParkID, rec);
                    }
                }
                int b = insertBillRecord(parkSpaceID, rec);
                int c = insertMeterReadRecord(parkSpaceID, rec);
                int d = insertParkChargeRecord(parkSpaceID, rec, localParkID);
            }
            return 0;
        }
        private void insertMessage(ParadoxRecord rec, int localParkID, int column)
        {
            string topMessage =  rec.DataValues[column].ToString() + rec.DataValues[column + 1].ToString();
            string botMessage =  rec.DataValues[column + 2].ToString() + rec.DataValues[column + 3].ToString() +
                                 rec.DataValues[column + 4].ToString() + rec.DataValues[column + 5].ToString() +
                                 rec.DataValues[column + 6].ToString();
             
            object[] values = { localParkID, rec.DataValues[2], botMessage, topMessage};
            try
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.messageTable, DatabaseControl.messageColumns, values);
            }
            catch
            { }
        }
        private int insertParkChargeRecord(int parkSpaceID, ParadoxRecord rec, int localParkID)
        {
            object[] chargeitems =  {
                                       "Balance Forward",       //Balance Forward
                                       "Late Charge",           //Late Charge
                                       rec.DataValues[28],      //Charge item 1
                                       rec.DataValues[31],      //Charge item 2
                                       rec.DataValues[34],      //Charge item 3
                                       rec.DataValues[37],      //Charge item 4
                                       rec.DataValues[40],      //Charge item 5
                                       rec.DataValues[43],      //Charge item 6
                                       rec.DataValues[46],      //Charge item 7
                                       rec.DataValues[49],      //Charge item 8
                                       rec.DataValues[127],     //Charge item 9
                                       rec.DataValues[130]      //Charge item 10
                                    };
            object[] chargevalues = {
                                       rec.DataValues[25],      //Balance Forward
                                       rec.DataValues[26],      //Late Charge
                                       rec.DataValues[29],      //Charge item 1 value
                                       rec.DataValues[32],      //Charge item 2 value
                                       rec.DataValues[35],      //Charge item 3 value
                                       rec.DataValues[38],      //Charge item 4 value
                                       rec.DataValues[41],      //Charge item 5 value
                                       rec.DataValues[44],      //Charge item 6 value
                                       rec.DataValues[47],      //Charge item 7 value
                                       rec.DataValues[50],      //Charge item 8 value
                                       rec.DataValues[128],     //Charge item 9 value
                                       rec.DataValues[131]      //Charge item 10 value
                                     };
            for (int a = 0; a < 12; a = a + 1)
                       
            {
                if (chargevalues[a] != DBNull.Value) 
                {
                    try
                    {
                        int x = 0;
                        // get the id of chargeitem[a] from the chargeeitem table and check if the parkchargeitem table has the record
                        x = getCharegeItemID(chargeitems[a].ToString());
                        string c = "parkID = @value0 and chargeItemID = @value1";
                        object[] v = { localParkID, x };
                        String[] parkOptionsColumns = { "ParkID", "ChargeItemID" };
                        object[] r = DatabaseControl.getSingleRecord(parkOptionsColumns, DatabaseControl.parkOptionsTable, c, v);
                        if (r == null) 
                        {
                            DatabaseControl.executeInsertQuery(DatabaseControl.parkOptionsTable, parkOptionsColumns, v); 
                        }
                
                        object[] values = { parkSpaceID, rec.DataValues[2], chargeitems[a], chargevalues[a], x };
                        if (a < 9 )
                        { DatabaseControl.executeInsertQuery(DatabaseControl.spaceChargeTable, DatabaseControl.spaceChargeColumns, values); }
                        else // recode 9, 10, 11 are temporary charges
                        {
                            object[] tempValues = { parkSpaceID, rec.DataValues[2], chargeitems[a], chargevalues[a] };
                            DatabaseControl.executeInsertQuery(DatabaseControl.spaceTempChargeTable, DatabaseControl.spaceTempChargeColumns, tempValues); }
                    }
                    catch (Exception e)
                    {
                        String s = e.Message;
                        //MessageBox.Show("Import failed  for charge item: " +chargeitems[a].ToString() + chargevalues[a].ToString() + " for " + parkSpaceID.ToString());
                    }
                }
            }
            return 1;
        }
        
        private string convertStatus(string input)
        {
            string output="";
            for (int i = 0; i < input.Length; i = i + 1)
            {
                char[] chars = { input[i] };
                char x = input[i];
                switch (x)
                {
                    case statusL:
                        if (output != "") {output = output + "/" + "CARE";} else { output = "CARE"; }
                        break;
                    case statusS:
                        if (output != "") { output = output + "/" + "Senior"; } else { output = "Senior"; }
                        break;
                    case statusF:
                        if (output != "") { output = output + "/" + "FERA"; } else { output = "FERA"; }
                        break;
                    case statusM:
                        if (output != "") { output = output + "/" + "Medical"; } else { output = "Medical"; }
                        break;
                    default:
                        string s = new string(chars);
                        if (output != "") { output = output + "/" + s; } else {output = s ;}
                        break;
                }
                
            }
            if (output == "Medical/CARE") { output = "CARE/Medical"; }
            return output;
        }
        private int updateTenantRecordWithMoveOutDate(int localParkID, int parkSpaceID, ParadoxRecord rec)
        {
            String[] tenantColumns = {"MoveOutDate"};
            Object[] tenantValues  = {rec.DataValues[18]};   //the move out date for old tenant is the move in date for the new tenant
            
            try
            {
                String cond = "ParkSpaceID = "+ parkSpaceID.ToString();
                DatabaseControl.executeUpdateQuery(DatabaseControl.spaceTable, tenantColumns, tenantValues, cond);

            }
            catch
            {
                //MessageBox.Show("Tenant Import failed!" + localParkID.ToString() + "/" + rec.DataValues[0].ToString());
            }

            return 1;
        }
        private int updateTenantRecord(int localParkID, int parkSpaceID, ParadoxRecord rec)
        {
            //need to convert tenant status
            string gasStatus = convertStatus(rec.DataValues[11].ToString());
            string eleStatus = convertStatus(rec.DataValues[12].ToString());
            string watStatus = convertStatus(rec.DataValues[13].ToString());
            bool DBForGas = false;
            bool DBForEle = false;
            bool DBForWat = false;
            if (rec.DataValues[14].ToString() == "Y") { DBForGas = true; }
            if (rec.DataValues[15].ToString() == "Y") { DBForEle = true; }
            if (rec.DataValues[16].ToString() == "Y") { DBForWat = true; }
            // Except move-in date, we update all other columns
            String[] tenantColumns = {"ParkID", 
                                      "SpaceID", 
                                      "Tenant",  
                                      "GasStatus",  
                                      "EleStatus", 
                                      "WatStatus", 
                                      "GasMedical",
                                      "EleMedical",
                                      "GasType",
                                      "EleType",
                                      "DontBillForGas", 
                                      "DontBillForEle", 
                                      "DontBillForWat",
                                      "Address1", 
                                      "City", 
                                      "State", 
                                      "Zip",
                                      "OrderID",
                                      //"Concession",
                                      "ConcessionType"
                                      };
            int allowanceForEle = determineMedicalAllowanceForEle(rec);
            int allowanceForGas = determineMedicalAllowanceForGas(rec);
            string spaceName = " ";
            if (rec.DataValues[1] is DBNull)
            {
                spaceName = rec.DataValues[0].ToString();
            }
            else
            {
                spaceName = rec.DataValues[1].ToString();
            }
            Object[] tenantValues = {  localParkID,            //ParkID
                                       spaceName,              //spaceID
                                       rec.DataValues[5],      //Tenant Name
                                       gasStatus,              //GasStatus
                                       eleStatus,              //EleStatus
                                       watStatus,              //WatStatus
                                       allowanceForGas,
                                       allowanceForEle,
                                       1,                      //GasType - All Service
                                       1,                      //EleType - All Service
                                       DBForGas,               //DontBillForGas
                                       DBForEle,               //DontBillForEle
                                       DBForWat,               //DontBillForWat
                                       rec.DataValues[7],      //Address1
                                       rec.DataValues[8],      //City
                                       rec.DataValues[9],      //State
                                       rec.DataValues[10],     //Zip
                                       rec.DataValues[0],      //Tenany order
                                       rec.DataValues[4]
                                       };
            try
            {
                String cond = "ParkSpaceID = "+ parkSpaceID.ToString();
                DatabaseControl.executeUpdateQuery(DatabaseControl.spaceTable, tenantColumns, tenantValues, cond);
            }
            catch
            {
                //MessageBox.Show("Tenant Import failed!" + localParkID.ToString() + "/" + rec.DataValues[0].ToString());
            }
            return 1;
        }
        private int determineMedicalAllowanceForEle(ParadoxRecord rec)
        {
            int result = 0;
            if (eleMedAlloanceColumn == -1)   // if there is ele Med Allowance column exist in DB
            {
                result = checkStatusColumn(rec.DataValues[12].ToString());
            }
            else
            {
                if (rec.DataValues[eleMedAlloanceColumn] != DBNull.Value)
                {
                    result = (Int16)rec.DataValues[eleMedAlloanceColumn];
                }
                else
                {
                    result = checkStatusColumn(rec.DataValues[11].ToString());
                }
            }
            return result;
        }
        private int determineMedicalAllowanceForGas(ParadoxRecord rec)
        {
            int result = 0;
            result = checkStatusColumn(rec.DataValues[11].ToString());
            return result;
        }
        private int checkStatusColumn(string input)
        {   int medicalAlloance = 0;
            for (int i = 0; i < input.Length; i = i + 1)
            {
                char[] chars = { input[i] };
                char x = input[i];
                if (x == 'M')
                {
                    medicalAlloance++;
                    break;
                }
            }
            return medicalAlloance;
        }
        private int insertTenantRecord(int localParkID, ParadoxRecord rec)
        {
            //need to convert tenant status
            string gasStatus = convertStatus(rec.DataValues[11].ToString());
            string eleStatus = convertStatus(rec.DataValues[12].ToString());
            string watStatus = convertStatus(rec.DataValues[13].ToString());
            bool DBForGas = false;
            bool DBForEle = false;
            bool DBForWat = false;
            if (rec.DataValues[14].ToString() == "Y") { DBForGas = true;}
            if (rec.DataValues[15].ToString() == "Y") { DBForEle = true; }
            if (rec.DataValues[16].ToString() == "Y") { DBForWat = true; }
            String[] tenantColumns = { "ParkID", 
                                       "SpaceID", 
                                       "Tenant", 
                                       "MoveInDate", 
                                       "GasStatus",  
                                       "EleStatus", 
                                       "WatStatus", 
                                       "GasMedical",
                                       "EleMedical",
                                       "GasType",
                                       "EleType",
                                       "DontBillForGas", 
                                       "DontBillForEle", 
                                       "DontBillForWat",
                                       "Address1", 
                                       "City", 
                                       "State", 
                                       "Zip",
                                       "OrderID",
                                       //"Concession",
                                       "ConcessionType"
                                    };
            int allowanceForEle = determineMedicalAllowanceForEle(rec);
            int allowanceForGas = determineMedicalAllowanceForGas(rec);
            char concessionType;
            if (rec.DataValues[4].ToString().Length == 1)
            {
                concessionType = Convert.ToChar(rec.DataValues[4]);
            }
            else
            {
                concessionType = ' ';
            }
            string spaceName = " ";
            if (rec.DataValues[1] is DBNull)
            {
                spaceName = rec.DataValues[0].ToString();
            }
            else
            {
                spaceName = rec.DataValues[1].ToString();
            }
            Object[] tenantValues = {  localParkID,            //ParkID
                                       spaceName,              //spaceID
                                       rec.DataValues[5],      //Tenant Name
                                       rec.DataValues[18],     //Move-in Date
                                       gasStatus,              //GasStatus
                                       eleStatus,              //EleStatus
                                       watStatus,              //WatStatus
                                       allowanceForGas,
                                       allowanceForEle,
                                       1,                      //Gas Type - All Service
                                       1,                      //Ele Type = All Service
                                       DBForGas,               //DontBillForGas
                                       DBForEle,               //DontBillForEle
                                       DBForWat,               //DontBillForWat
                                       rec.DataValues[7],      //Address1
                                       rec.DataValues[8],      //City
                                       rec.DataValues[9],      //State
                                       rec.DataValues[10],     //Zip
                                       rec.DataValues[0],      //space order ID
                                       concessionType 
                                       };

            try
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.spaceTable, tenantColumns, tenantValues);
                return DatabaseControl.getParkSpaceID(localParkID, spaceName);
            }
            catch
            {
                MessageBox.Show("Park/Tenant Import failed!" + localParkID.ToString() + "/" + rec.DataValues[0].ToString());
                return -1;
            }

           
        }
        private int insertMeterLocationRecord(int localParkID, ParadoxRecord rec)
        {
           String[] locColumns = {      "ParkID", 
                                        "OrderID", 
                                        "GasMeterLoc",
                                        "EleMeterLoc",
                                        "WatMeterLoc",
                                        "SpecialAccess"
                                 };
            Object[] locValues = {      localParkID,            //ParkID
                                        rec.DataValues[0],      //spaceID
                                        rec.DataValues[134],    //Gas Location
                                        rec.DataValues[133],    //Ele Location
                                        rec.DataValues[135],    //Wat Location
                                        rec.DataValues[136]     //Wat Location
                                 };

            try
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.meterInfoTable, locColumns, locValues);

            }
            catch
            {
                //MessageBox.Show("Meter Location Import failed for Park/Space: " + localParkID.ToString() + "/" + rec.DataValues[0].ToString());
            }

            return 1;
        }
        private int insertTaxRate(int localParkID)
        {
            String[] locColumns = {     "ParkID", 
                                        "UtilityType"
                                 };
            Object[] locValues = {      localParkID,            //ParkID
                                        "E"
                                 };

           
            DatabaseControl.executeInsertQuery("TaxRates", locColumns, locValues);
            locValues[1] = "G";
            DatabaseControl.executeInsertQuery("TaxRates", locColumns, locValues);
            locValues[1] = "W";
            DatabaseControl.executeInsertQuery("TaxRates", locColumns, locValues);
            return 1;
        }
        private int insertBillRecord(int parkSpaceID, ParadoxRecord rec)
        {
            Object[] billValues = { parkSpaceID, 
                                       rec.DataValues[2],       //DueDate
                                       rec.DataValues[19],      //GasPreviousRead
                                       rec.DataValues[20],      //GasCurrentRead
                                       rec.DataValues[56],      //GasUsage
                                       rec.DataValues[73],      //GasBill
                                       rec.DataValues[21],      //ElePreviousRead
                                       rec.DataValues[22],      //EleCurrentRead
                                       rec.DataValues[77],      //EleUsage
                                       rec.DataValues[96],      //EleBill
                                       rec.DataValues[23],      //WatPreviousRead
                                       rec.DataValues[24],      //WatCurrentRead
                                       rec.DataValues[99],      //WatUsage
                                       rec.DataValues[116]      //WatBill
                                       };
            for (int i = 2; i < 14; i++)
            {
                if (billValues[i] == DBNull.Value)
                { billValues[i] = 0; }
            }
            try
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.spaceBillTable, DatabaseControl.spaceBillColumns, billValues);
            }
            catch
            {
                string ParkNumber = (DatabaseControl.getParkNumberByParkSpaceID(parkSpaceID)).ToString();
                //MessageBox.Show("Tenant Bill Import failed for Park/Tenant: " + ParkNumber + "/" + parkSpaceID);

            }

            return 1;
        }
        private int insertMeterReadRecord(int parkSpaceID, ParadoxRecord rec)
        {
            int parkID = DatabaseControl.getParkIDByParkSpaceID(parkSpaceID);
            
            Double gasread ;
            Double eleread ;
            Double watread ;
            if (rec.DataValues[20] is DBNull) {gasread = 0;} else {gasread = Convert.ToDouble(rec.DataValues[20]);}
            if (rec.DataValues[22] is DBNull) {eleread = 0;} else {eleread = Convert.ToDouble(rec.DataValues[22]);}
            if (rec.DataValues[24] is DBNull) { watread = 0; } else { watread = Convert.ToDouble(rec.DataValues[24]); }
            Object[] meterValues = {   parkID, 
                                       rec.DataValues[0],       //spaceID
                                       rec.DataValues[18],      //StartDate
                                       rec.DataValues[17],      //EndDate
                                       gasread,                 //GasCurrentRead
                                       eleread,                 //EleCurrentRead
                                       watread,                 //WatCurrentRead
                                       rec.DataValues[137],     //Employee ID
                                       rec.DataValues[138],     //read time
                                       rec.DataValues[2],       //dueDate
                                       rec.DataValues[55],      //Gas Thermo factor
                                       };

            try
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.meterReadsTable, DatabaseControl.meterReadsColumns, meterValues);
            }
            catch
            {
                //MessageBox.Show("Insert Meter Read record failed!" + parkSpaceID);
            }
            return 1;
        }
        private int insertPeriodRecord(int parkID, ParadoxRecord rec)
        {
           Object[] values = {   parkID, 
                                 rec.DataValues[18],      //StartDate
                                 rec.DataValues[17],      //EndDate
                                 rec.DataValues[2],       //dueDate
                             };

            try
            {
                DatabaseControl.executeInsertQuery("ParkPeriod", DatabaseControl.periodColumns, values);
            }
            catch
            {
                //MessageBox.Show("Insert Meter Read record failed!" + parkSpaceID);
            }
            return 1;
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                dirText.Text = folderBrowserDialog1.SelectedPath;
                }
        }

        
        

        

        private void utilityButton_Click(object sender, EventArgs e)
        {
            //This method improts utility company and rate records
            DateTime starttime = DateTime.Now;
            string path = "";

            // user spaceifies directory path to override the defaults
            if (dirText.Text == "")
            { path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "data"); }
            else
            { path = dirText.Text; }

            int counter;
            if (importOneMonth.Checked == true)
            { counter = 1; }
            else
            { counter = 14; }

            for (int i = 0; i < counter; i = i + 1)
            {
                // we set up the path to include subfolders 0 - 13 that contains Park DB files and NUTIELE.DB 
                // but if we only import one month data, we just use the folder sepcified on gui
                string pathi;
                if (importOneMonth.Checked == true)
                { pathi = path; }
                else
                { pathi = Path.Combine(path, i.ToString()); }
                // Import the Utility Rate Info
                switch (UtilcomboBox1.Text)
                {
                    case "ELECTRICITY":
                        ImportEleRate(pathi);
                        break;
                    case "GAS":
                        ImportGasRate(pathi);
                        break;
                    case "WATER":
                        ImportWatRate(pathi);
                        break;
                    default:
                        break;
                }
                
            }
        }
        private void ImportEleRate(string path)
        {
            var table = new ParadoxTable(path, isEleDB);
            int utilRateID, utilCompanyID = 1;
            // find out whicch column index is used for DWR BC
            DWRBCColumn = -1;
            FERADiscountColumn = -1;
            for (int i = 0; i < table.FieldCount; i = i + 1)
            {
                if (table.FieldNames[i] == "Ele li sur sr citizen")
                {
                    DWRBCColumn = i;
                }
                else
                    if (table.FieldNames[i] == "'98 Discount")
                    {
                        FERADiscountColumn = i;
                    }

            };
            //string x = table.Enumerate().ElementAt(0).DataValues[0].ToString();
            foreach (var rec in table.Enumerate())
            {
                //  We will only work for SCE for now
                //utilCompanyID = insertUtilCompany(rec, isEleDB);

                //if (rec.DataValues[1].ToString() == "PG&E")
                //{
                utilCompanyID = 1102;  //PG&E 
                utilRateID = insertUtilRate(rec, isEleDB, utilCompanyID, path);
                if ((utilRateID > 0) & (DWRBCColumn > 0) | (FERADiscountColumn > 0))
                {
                    insertSurcharge(rec, utilRateID);
                }
                
                insertBaselineAllowance(rec, utilRateID);
                const int SCE = 1;
                const int PGE = 1102;
                switch (utilCompanyID)
                {
                    case SCE:
                        insertTierRateforSCE(rec, utilRateID);
                        insertBasicRateforSCE(rec, utilRateID);
                        break;
                    case PGE:
                        insertTierRateforPGE(rec, utilRateID);
                        insertBasicRateforPGE(rec, utilRateID);
                        break;
                    default:
                        break;
                }
                break;
                //}

            }

        }
        private void ImportGasRate(string path)
        {

            string fileLocation = Path.Combine(path, "GAS.csv");
            if (!(File.Exists(fileLocation)))
            {
                MessageBox.Show("GAS.CSV file is missing");
                return;
            }
            using (TextFieldParser csvParser = new TextFieldParser(fileLocation))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row if the first row has the column names
                if (headerComboBox.Text == "Yes")
                {
                    csvParser.ReadLine();
                }

                inserted = 0;
                failed = 0;
                //public static String utilCompanyTable = "UtilityCompany";
                //public static String[] utilCompanyColumns = {"CompanyName", "IsElectricity", "IsGas", "IsWater"};

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    if (fields.Length == 1) break; // real record should have more than one column
                    int count = 0;
                    foreach (string field in fields)
                    {
                        if (field == "") { fields[count] = "0.0"; }
                        count = count + 1;
                    }
                    //check if the Utility company already existed in the database
                    // if not, insert the new one
                    int result = checkIfUtilCompanyExist(fields[2], 'G');
                    int utilCompanyID = -1;
                    switch (result)
                    {
                        case 0:
                            updateUtilCompany(fields[2], 'G');
                            utilCompanyID = getUtilCompanyID(fields[2], 'G');
                            break;
                        case 1:
                            utilCompanyID = getUtilCompanyID(fields[2], 'G');
                            break;
                        case -1:
                            utilCompanyID = insertUtilCompany(fields[2], 'G');
                            inserted = inserted + 1;
                            break;
                        default:
                            // system error
                            return;
                    }

                    // insert utility rate
                    int gasTierSetID = 6;
                    int uRateID = insertUtilRate(fields, 'G', utilCompanyID, path, gasTierSetID);
                    object[] tierRateValue = { 0.0, 0.0, 0.0, 0.0, 0.0 };
                    tierRateValue[0] = fields[21];
                    tierRateValue[1] = fields[24];
                    tierRateValue[2] = fields[27];
                    insertTierRate(tierRateValue, 'G', "Regular",       uRateID);  // G for Generation
                    tierRateValue[0] = fields[22];
                    tierRateValue[1] = fields[25];
                    tierRateValue[2] = fields[28];
                    insertTierRate(tierRateValue, 'G', "CARE",          uRateID);
                    insertTierRate(tierRateValue, 'G', "CARE/Medical",  uRateID);
                    tierRateValue[0] = fields[23];
                    tierRateValue[1] = fields[26];
                    tierRateValue[2] = fields[29];
                    insertTierRate(tierRateValue, 'G', "Senior",        uRateID);
                    
                    DateTime summerStartDate = new DateTime(utilStartDate.Year, 05, 01);
                    DateTime winterStartDate = new DateTime(utilStartDate.Year, 11, 01);
                    int x = 38; 
                    
                    if ((utilStartDate > summerStartDate) & (utilStartDate < winterStartDate))  
                    { x = 38; }    // summer allowance rate column starts here
                    else
                    { x = 68; }    // winter allowance rate column starts here
                    Object[] value = 
                                    {uRateID,
                                    '1',                       // All Services allowance
                                    '1',                       // Zone
                                    fields[x]                  // Allowance
                                    };
                    for (int i = 0; i < 10; i = i + 1)
                    {
                        value[2] = i;
                        value[1] = '1';                             // All Services allowance
                        value[3] = fields[x + 10 + i];
                        DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, value);
                        value[1] = '2';                             // Cooking only Services allowance
                        value[3] = fields[x + 20 + i];
                        DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, value);
                        value[1] = '3';                             // Space Heating only Services allowance
                        value[3] = fields[x  + i];
                        DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, value);
                    }
                    //insert medical baseline allowance
                    if ((utilStartDate > summerStartDate) & (utilStartDate < winterStartDate))
                    { x = 36; }    // summer medical baseline allowance rate column 
                    else
                    { x = 37; }    // winter medical baseline allowance rate column
                    value[2] = 'M';
                    value[1] = 'M';                                 // All Services allowance
                    value[3] = fields[x];
                    DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, value);

                    //insert utility basic rates
                    value[1] = "Regular";                     // Tenant type
                    value[2] = 0;                             // minimum Services rate
                    value[3] = fields[18];                    // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "Regular";                     // Tenant type
                    value[2] = 1;                             // all Services rate
                    value[3] = fields[6];                     // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "Regular";                     // Tenant type
                    value[2] = 2;                             // cooking only Services rate
                    value[3] = fields[9];                     // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "Regular";                     // Tenant type
                    value[2] = 3;                             // space heating only Services rate
                    value[3] = fields[9];                     // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE";                        // Tenant type
                    value[2] = 0;                             // minimum Services rate
                    value[3] = fields[19];                    // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE/Medical";                // Tenant type
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE";                        // Tenant type
                    value[2] = 1;                             // all Services rate
                    value[3] = fields[7];                     // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE/Medical";                // Tenant type
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE";                        // Tenant type
                    value[2] = 2;                             // cooking only Services rate
                    value[3] = fields[10];                    // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE/Medical";                // Tenant type
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE";                        // Tenant type
                    value[2] = 3;                             // space heating only Services rate
                    value[3] = fields[10];                    // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE/Medical";                // Tenant type
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "Senior";                      // Tenant type
                    value[2] = 0;                             // minimum Services rate
                    value[3] = fields[20];                    // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "Senior";                      // Tenant type
                    value[2] = 1;                             // all Services rate
                    value[3] = fields[8];                     // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "Senior";                      // Tenant type
                    value[2] = 2;                             // cooking only Services rate
                    value[3] = fields[11];                    // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "Senior";                      // Tenant type
                    value[2] = 3;                             // space heating only Services rate
                    value[3] = fields[11];                    // basic rate
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    inserted = inserted + 1;

                    //insert Surcharge record
                    Object[] values = 
                    {
                        uRateID,    
                        "Inspection Fee",            //Description
                        "All",                       //Tenant Type, Regular, CARE, Medical, ...
                        'D',
                        1,                           // 1- Flat Rate 2- Usage 3-Daily
                        fields[15]
                    };
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
                    values[1] = "LIRA Surcharge";    //Description
                    values[5] =  fields[33];
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
                }
                string s = "File imported with " + inserted.ToString() + " records! ";
                if (failed > 0)
                {
                    s = s + " and " + failed.ToString() + " records failed!";
                }
                MessageBox.Show(s);

            }

        }
        private void ImportWatRate(string path)
        {
            string fileLocation = Path.Combine(path, "WATER.csv");
            if (!(File.Exists(fileLocation)))
            {
                MessageBox.Show("Water.CSV file is missing");
                return;
            }
            using (TextFieldParser csvParser = new TextFieldParser(fileLocation))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row if the first row has the column names
                if (headerComboBox.Text == "Yes")
                {
                    csvParser.ReadLine();
                }

                inserted = 0;
                failed = 0;
                //public static String utilCompanyTable = "UtilityCompany";
                //public static String[] utilCompanyColumns = {"CompanyName", "IsElectricity", "IsGas", "IsWater"};
        
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    if (fields.Length == 1) break; // real record should have more than one column
                    int count = 0;
                    foreach (string field in fields)
                    {
                        if (field == "") { fields[count] = "0.0"; }
                        count = count + 1;
                    }
                    //check if the Utility company already existed in the database
                    // if not, insert the new one
                    int result = checkIfUtilCompanyExist(fields[2], 'W');
                    int utilCompanyID =-1;
                    switch (result)
                    {
                        case 0:
                            updateUtilCompany(fields[2], 'W');
                            break;
                        case 1:
                            break;
                        case -1:
                            utilCompanyID = insertUtilCompany(fields[2], 'W');
                            inserted = inserted + 1;
                            break;
                        default:
                            break;
                    }
                    utilCompanyID = getUtilCompanyID(fields[2], 'W');
                    // insert utility rate
                    int watTierSetID = 4;
                    int uRateID = insertUtilRate(fields, 'W', utilCompanyID, path, watTierSetID);
                    object[] tierRateValue = {0.0,0.0,0.0,0.0,0.0};
                    tierRateValue[0] = fields[23];
                    tierRateValue[1] = fields[27];
                    tierRateValue[2] = fields[31];
                    insertTierRate(tierRateValue, 'G', "Regular", uRateID);
                    tierRateValue[0] = fields[24];
                    tierRateValue[1] = fields[28];
                    tierRateValue[2] = fields[32];
                    insertTierRate(tierRateValue, 'G', "CARE", uRateID);
                    insertTierRate(tierRateValue, 'G', "CARE/Medical", uRateID);
                    tierRateValue[0] = fields[25];
                    tierRateValue[1] = fields[29];
                    tierRateValue[2] = fields[33];
                    insertTierRate(tierRateValue, 'G', "Senior", uRateID);
                    tierRateValue[0] = fields[26];
                    tierRateValue[1] = fields[30];
                    tierRateValue[2] = fields[34];
                    insertTierRate(tierRateValue, 'S', "All", uRateID);  //Sewer charges

                    insertBaselineAllowance('1', 'S', uRateID, double.Parse(fields[45]));
                    insertBaselineAllowance('1', 'W', uRateID, double.Parse(fields[47]));
                    insertBaselineAllowance('2', 'S', uRateID, double.Parse(fields[49]));
                    insertBaselineAllowance('2', 'W', uRateID, double.Parse(fields[51]));
                    insertBaselineAllowance('3', 'S', uRateID, double.Parse(fields[46]));
                    insertBaselineAllowance('3', 'W', uRateID, double.Parse(fields[48]));
                    insertBaselineAllowance('4', 'S', uRateID, double.Parse(fields[50]));
                    insertBaselineAllowance('4', 'W', uRateID, double.Parse(fields[52]));
                    Object[] value = {  uRateID,
                                        "Regular",                 
                                        0,                  //minimum rate
                                        fields[20]          //Regulau Rate
                                     };
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[2] = 'W';                           //regular basic rate
                    value[3] = fields[6];
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE"; 
                    value[2] = 0;                           //minimum basic rate
                    value[3] = fields[21];
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE";
                    value[2] = 'W';                           //regular basic rate
                    value[3] = fields[7];
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE/Medical";
                    value[2] = 0;                           //minimum basic rate
                    value[3] = fields[21];
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "CARE/Medical";
                    value[2] = 'W';                           //regular basic rate
                    value[3] = fields[7];
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "Senior";
                    value[2] = 0;                           //minimum basic rate
                    value[3] = fields[22];
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "Senior";
                    value[2] = 'W';                           //regular basic rate
                    value[3] = fields[8];
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    value[1] = "All";
                    value[2] = '3';                           //Sewer basic rate
                    value[3] = fields[9];
                    DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, value);
                    inserted = inserted + 1;
                }
                string s = "File imported with " + inserted.ToString() + " records! ";
                if (failed > 0)
                {
                    s = s + " and " + failed.ToString() + " records failed!";
                }
                MessageBox.Show(s);

            }
        }
        public int checkIfUtilCompanyExist(string utilCompany, char utilityType)
            // return -1 if the utility company does not exist
            // return 1 if the utility company does exist
            // return 0 if the utility company does exist, but the type (isElectricity, isGas, isWater) is not turned on 
            // return 99 system error
        {
            // utilitype is either E, G, or W that is the type of the utility company
            object[] value = { utilCompany };
            object[] result = DatabaseControl.getSingleRecord(DatabaseControl.utilCompanyColumns, DatabaseControl.utilCompanyTable, "CompanyName = @value0", value);
            // utilCompanyColumns = {"CompanyName", "IsElectricity", "IsGas", "IsWater"};
            if (result == null)
                { return -1; }          //Util Company does not exist
            else
            {
                int ix = 0;
                switch (utilityType)
                {
                    case 'E':
                        ix = 1;
                        break;
                    case 'G':
                        ix = 2;
                        break;
                    case 'W':
                        ix = 3;
                        break;
                    default:
                        MessageBox.Show("System Error 99, notify the system administrator!");
                        return 99;
                }

                if ((bool)result[ix])
                { return 1; }           // Util Company exist and it has the Util Company type as the input
                else { return 0; }      // Util Company exist, but it does not have the coorect Util type turned on yet
            }

        }
        private int insertUtilCompany(string utilCompany, char utilType)
        {
            object[] value = { utilCompany, false, false, false };
            switch (utilType)
            {
                case 'E':
                    value[1] = true;
                    break;
                case 'G':
                    value[2] = true;
                    break;
                case 'W':
                    value[3] = true;
                    break;
                default:
                    break;
            }
            
            try
            {
                return DatabaseControl.executeInsertQuery(DatabaseControl.utilCompanyTable, DatabaseControl.utilCompanyColumns, value);
                
            }
            catch 
            { 
                MessageBox.Show("Utility Company Import failed!" + utilCompany);
                return -1;
            }
        }
        private void updateUtilCompany(string utilCompany, char utilType)
        {
            object[] value = { false, false, false };
            string[] utilCompanyColumns = { "IsElectricity", "IsGas", "IsWater"};
            switch (utilType)
            {
                case 'E':
                    value[0] = true;
                    break;
                case 'G':
                    value[1] = true;
                    break;
                case 'W':
                    value[2] = true;
                    break;
                default:
                    break;
            }

            try
            {
                DatabaseControl.executeUpdateQuery(DatabaseControl.utilCompanyTable, utilCompanyColumns, value, "CompanyName =" + utilCompany);
            }
            catch { MessageBox.Show("Utility Company update failed!" + utilCompany); }
        }
        private int getUtilCompanyID(string utilCompany, char utilType)
        {
            object[] value = { utilCompany, 1 };
            string[] utilCompanyColumns = { "UtilityCompanyID"};
            string cond = "";
            switch (utilType)
            {
                case 'E':
                    cond = "IsElectricity";
                    break;
                case 'G':
                    cond = "IsGas";
                    break;
                case 'W':
                    cond = "IsWater";
                    break;
                default:
                    break;
            }
            int ID = -1;
            try
            {
                object[] result = DatabaseControl.getSingleRecord(utilCompanyColumns,"UtilityCompany","CompanyName = @value0 and " + cond + " = @value1", value);
                if (result == null) { ID = -1; }
                else {ID = (int) result[0];}
            }
            catch { MessageBox.Show("Could not retrieve Utility Company ID"); ID = -1; }
            return ID;
        }
        private int getCharegeItemID(string chargeItem)
        {
            object[] value = { chargeItem };
            string[] Columns = { "ChargeItemID" };
            
            int ID = -1;
            try
            {
                object[] result = DatabaseControl.getSingleRecord(Columns, "ChargeItem", "ChargeItemDescription = @value0 " , value);
                if (result == null) 
                {
                    Columns[0] = "ChargeItemDescription";
                    ID = DatabaseControl.executeInsertQuery("ChargeItem", Columns, value);
                }
                else 
                { 
                    ID = (int)result[0]; 
                }
            }
            catch (Exception e) { MessageBox.Show("could not get/create the chargeItem ID" + e.Message); ID = -1; }
            return ID;
        }
    }
}
