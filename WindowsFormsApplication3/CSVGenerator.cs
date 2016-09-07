using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUB {
    class CSVGenerator {
        public static void totalListFormat(int parkNumber, String parkName, ArrayList info, String[] parkCharges) {
            StringBuilder csv = new StringBuilder();
            foreach (Dictionary<String, Object> dict in info) {
                List<Object> row = new List<Object>();
                row.Add(dict["OrderID"]);
                row.Add(dict["Tenant"].ToString().Replace(',',' '));
                String gasSpecial = "";
                String eleSpecial = "";
                decimal total = 0.0M;

                if (!(string.Equals("Regular", dict["GasStatus"].ToString(), StringComparison.OrdinalIgnoreCase) || dict["GasStatus"].ToString() == "")) gasSpecial += "#";
                if (!(string.Equals("Regular", dict["EleStatus"].ToString(), StringComparison.OrdinalIgnoreCase) || dict["EleStatus"].ToString() == "")) eleSpecial += "#";
                if ((int)dict["GasMedical"] > 0) gasSpecial += "@";
                if ((int)dict["EleMedical"] > 0) eleSpecial += "@";

                row.Add(gasSpecial);
                row.Add(((decimal)dict["GasBill"]).ToString("N2"));
                row.Add(eleSpecial);
                row.Add(((decimal)dict["EleBill"]).ToString("N2"));
                row.Add(((decimal)dict["WatBill"]).ToString("N2"));
                foreach (String item in parkCharges) {
                    row.Add(((decimal)dict[item]).ToString("N2"));
                    total += (decimal)dict[item];
                }
                row.Add(((decimal)dict["Other"]).ToString("N2"));
                row.Add(((decimal)dict["TempCharge"]).ToString("N2"));
                total += (decimal)dict["GasBill"] + (decimal)dict["EleBill"] + (decimal)dict["WatBill"];
                total += (decimal)dict["Other"] + (decimal)dict["TempCharge"];
                row.Add(total.ToString("N2"));

                csv.AppendLine(String.Join(",", row));
            }

            Directory.CreateDirectory("Export");
            File.WriteAllText(String.Format("Export\\{0}.csv", parkNumber), csv.ToString());
        }

        public static void santiagoFormat(String santiago, ArrayList info) {
            List<String> csv = new List<String>();
            Dictionary <String, int> santiagoCode = new Dictionary<string,int>();
            switch (santiago) {
                case "NAP": santiagoCode = SantiagoCodes.nap; break;
                case "MIL": santiagoCode = SantiagoCodes.mil; break;
                case "SCM": santiagoCode = SantiagoCodes.scm; break;
                case "OMH": santiagoCode = SantiagoCodes.omh; break;
                case "GAM": santiagoCode = SantiagoCodes.gam; break;
                case "SES": santiagoCode = SantiagoCodes.ses; break;
                case "PPM": santiagoCode = SantiagoCodes.ppm; break;
                case "SPE": santiagoCode = SantiagoCodes.spe; break;
                case "DBE": santiagoCode = SantiagoCodes.dbe; break;
                case "SDV": santiagoCode = SantiagoCodes.sdv; break;
                case "SAV": santiagoCode = SantiagoCodes.sav; break;
                case "SBS": santiagoCode = SantiagoCodes.sbs; break;
                case "SEP": santiagoCode = SantiagoCodes.sep; break;
                case "WOS": santiagoCode = SantiagoCodes.wos; break;
                case "COP": santiagoCode = SantiagoCodes.cop; break;
                case "SIV": santiagoCode = SantiagoCodes.siv; break;
                case "CON": santiagoCode = SantiagoCodes.con; break;
                case "SMH": santiagoCode = SantiagoCodes.smh; break;
                case "RMM": santiagoCode = SantiagoCodes.rmm; break;
                case "SHE": santiagoCode = SantiagoCodes.she; break;
                case "NSM": santiagoCode = SantiagoCodes.nsm; break;
                case "VPE": santiagoCode = SantiagoCodes.vpe; break;
                case "PLH": santiagoCode = SantiagoCodes.plh; break;
                case "SEM": santiagoCode = SantiagoCodes.sem; break;
                case "SCE": santiagoCode = SantiagoCodes.sce; break;
                case "SSV": santiagoCode = SantiagoCodes.ssv; break;

                case "SSC": santiagoCode = SantiagoCodes.ssc; break;
                case "SEG": santiagoCode = SantiagoCodes.seg; break;
                case "SEL": santiagoCode = SantiagoCodes.sel; break;
                case "SMV": santiagoCode = SantiagoCodes.smv; break;
                case "SCS": santiagoCode = SantiagoCodes.scs; break;
                case "SSE": santiagoCode = SantiagoCodes.sse; break;
            }


            foreach (Dictionary<String, Object> dict in info) {
                if (!santiagoCode.Keys.Contains(dict["ChargeItemSantiago"].ToString()) || (decimal)dict["ChargeItemValue"] == 0M) { continue; }
                List<Object> row = new List<Object>();
                row.Add(dict["SpaceID"].ToString());
                row.Add(((DateTime)dict["DueDate"]).ToString("d"));
                row.Add(santiagoCode[dict["ChargeItemSantiago"].ToString()]);
                row.Add(dict["ChargeItemSantiago"]);
                row.Add(dict["Description"].ToString());
                row.Add("");
                row.Add(((decimal)dict["ChargeItemValue"]).ToString("N2"));
                row.Add("");
                row.Add(((decimal)dict["ChargeItemValue"]).ToString("N2"));

                csv.Add(String.Join(",", row));
            }

            csv.Sort(delegate(String obj1, String obj2) {
                String[] compare1 = obj1.Split(',');
                String[] compare2 = obj2.Split(',');
                if (compare1[0] == compare2[0]) return Convert.ToInt32(compare1[2]).CompareTo(Convert.ToInt32(compare2[2]));
                else return compare1[0].CompareTo(compare2[0]);
            });

            StringBuilder data = new StringBuilder();
            foreach (String item in csv) data.AppendLine(item);

            Directory.CreateDirectory("Export");
            File.WriteAllText(String.Format("Export\\{0}.csv", santiago), data.ToString());
        }
    }
}
