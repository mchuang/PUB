using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUB {
    class PdfControl {
        static XFont font = new XFont("Consolas", 11, XFontStyle.Regular);
        static double height = 11;

        public static void testFont() {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "testfonts";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            XFont font;
            String teststring = "The quick brown fox jumped over the lazy dog. 0123456789 abcdefghi ABCDEFGHI";
            font = new XFont("Lucida Sans", 10, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 0, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Lucida Sans", 12, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 20, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Lucida Sans", 14, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 40, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Lucida Sans TypeWriter", 10, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 60, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Lucida Sans TypeWriter", 12, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 80, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Lucida Sans TypeWriter", 14, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 100, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Terminal", 10, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 120, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Terminal", 12, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 140, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Terminal", 14, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 160, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("OCR A Extended", 10, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 180, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("OCR A Extended", 12, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 200, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("OCR A Extended", 14, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 220, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Consolas", 10, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 240, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Consolas", 12, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 260, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Consolas", 14, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 280, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Courier New", 10, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 300, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Courier New", 12, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 320, page.Width, height), XStringFormats.TopLeft);
            font = new XFont("Courier New", 14, XFontStyle.Regular);
            tf.DrawString(teststring, font, XBrushes.Black, new XRect(0, 340, page.Width, height), XStringFormats.TopLeft);
            document.Save("test.pdf");
        }

        public static void createBillPdf(int statementNum, Object[] bill) {
            int parkNumber = (int)bill[0];
            String spaceId = bill[1].ToString();
            Object[] tenantPdf = (Object[])bill[2];
            Object[] parkPdf = (Object[])bill[3];
            Object[] usagePdf = (Object[])bill[4];
            Object[] gasPdf = (Object[])bill[5];
            Object[] elePdf = (Object[])bill[6];
            Object[] watPdf = (Object[])bill[7];
            List<Object[]> summaryPdf = (List<Object[]>)bill[8];
            DateTime start = (DateTime)bill[9];
            DateTime end = (DateTime)bill[10];
            Object[] messagePdf = (Object[])bill[11];
            
            String billForm = "form.pdf";
            PdfDocument billDocument = PdfReader.Open(billForm, PdfDocumentOpenMode.Import);

            Directory.CreateDirectory("Records");
            PdfDocument document = new PdfDocument();
            document.Info.Title = String.Format("{0}", parkNumber);
            PdfPage page = billDocument.Pages[0];
            document.AddPage(page);
            XGraphics gfx = XGraphics.FromPdfPage(document.Pages[0]);

            int marginSpace = 0;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;

            double line = height;

            writeTenantPdf(gfx, xMin + 10, yMin + line * 11, xMin + 10 + xMax / 2, yMin + line * 20, tenantPdf);
            writeParkPdf(gfx, xMin + 30 + xMax / 2, yMin + line * 11, xMax, yMin + line * 20, parkPdf);
            writeUsagePdf(gfx, xMin + 10, yMin + line * 21, xMax, yMin + line * 40, usagePdf);
            writeGasPdf(gfx, xMin + 10, yMin + line * 28, xMin + (xMax - xMin) * 1 / 3 - 10, yMin + line * 60, gasPdf);
            writeElePdf(gfx, xMin + (xMax - xMin) / 3, yMin + line * 28, xMin + (xMax - xMin) * 2 / 3 - 10, yMin + line * 60, elePdf);
            writeWatPdf(gfx, xMin + (xMax - xMin) * 2/ 3, yMin + line * 28, xMin + (xMax - xMin) - 10, yMin + line * 60, watPdf);
            writeSummaryPdf(gfx, xMin + (xMax - xMin) * 2 / 3, yMin + line * 42, xMin + (xMax - xMin) - 10, yMin + line * 80, summaryPdf);
            writePayTotalPdf(gfx, xMin + (xMax - xMin) * 5 / 6, yMin + height * 8, xMax - 10, yMax, summaryPdf);
            writePayTotalPdf(gfx, xMin + (xMax - xMin) * 5 / 6, yMin + height * 62, xMax - 10, yMax, summaryPdf);
            writeStatementNum(gfx, xMin, yMin + height * 1, xMax, yMax, statementNum);
            writeTopMessagePdf(gfx, xMin + 10, yMin + height * 49, xMax / 2, yMax, messagePdf[0].ToString());
            writeBotMessagePdf(gfx, xMin + 10, yMin + height * 58, xMax / 2, yMax, messagePdf[1].ToString());

            Directory.CreateDirectory(String.Format("Records\\{0}", parkNumber));
            string path = String.Format("Records\\{0}\\Bill{1}({2}-{3}).pdf", parkNumber, spaceId, start.ToString("MMdd"), end.ToString("MMdd"));
            document.Save(path);
            //Process.Start(path);
        }

        public static void createBillPrint(int statementNum, Object[] bill) {
            int parkNumber = (int)bill[0];
            String spaceId = bill[1].ToString();
            Object[] tenantPdf = (Object[])bill[2];
            Object[] parkPdf = (Object[])bill[3];
            Object[] usagePdf = (Object[])bill[4];
            Object[] gasPdf = (Object[])bill[5];
            Object[] elePdf = (Object[])bill[6];
            Object[] watPdf = (Object[])bill[7];
            List<Object[]> summaryPdf = (List<Object[]>)bill[8];
            DateTime start = (DateTime)bill[9];
            DateTime end = (DateTime)bill[10];
            Object[] messagePdf = (Object[])bill[11];

            Directory.CreateDirectory("Records");
            PdfDocument document = new PdfDocument();
            document.Info.Title = String.Format("{0}", parkNumber);
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            int marginSpace = 0;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;

            double line = height;

            writeTenantPdf(gfx, xMin + 10, yMin + line * 10, xMin + 10 + xMax / 2, yMin + line * 20, tenantPdf);
            writeParkPdf(gfx, xMin + 30 + xMax / 2, yMin + line * 10, xMax, yMin + line * 20, parkPdf);
            writeUsagePdfPrint(gfx, xMin, yMin + line * 20, xMax, yMin + line * 40, usagePdf);
            writeGasPdf(gfx, xMin, yMin + line * 27, xMin + (xMax - xMin) * 1 / 3 - 10, yMin + line * 60, gasPdf);
            writeElePdf(gfx, xMin + (xMax - xMin) / 3, yMin + line * 27, xMin + (xMax - xMin) * 2 / 3 - 10, yMin + line * 60, elePdf);
            writeWatPdf(gfx, xMin + (xMax - xMin) * 2 / 3 + 10, yMin + line * 27, xMax - 10, yMin + line * 60, watPdf);
            writeSummaryPdf(gfx, xMin + (xMax - xMin) * 2 / 3 + 10, yMin + line * 42, xMax - 10, yMin + line * 80, summaryPdf);
            writePayTotalPdf(gfx, xMin + (xMax - xMin) * 5 / 6, yMin + height * 7, xMax - 10, yMax, summaryPdf);
            writePayTotalPdf(gfx, xMin + (xMax - xMin) * 5 / 6, yMin + height * 61, xMax - 10, yMax, summaryPdf);
            writeStatementNum(gfx, xMin, yMin + height * 3, xMax, yMax, statementNum);

            Directory.CreateDirectory(String.Format("Records\\{0}", parkNumber));
            string path = String.Format("Records\\{0}\\Print{1}({2}-{3}).pdf", parkNumber, spaceId, start.ToString("MMdd"), end.ToString("MMdd"));
            document.Save(path);
        }

        public static void createParkSummReport(int parkNumber, String parkName, DateTime dueDate, ArrayList info, List<Object[]> parkCharges, List<String> tempCharges) {
            Directory.CreateDirectory("ParkReports");
            PdfDocument document = new PdfDocument();
            document.Info.Title = String.Format("{0}", parkNumber);
            XTextFormatter tf = createNewPageParkSumm(ref document);
            XFont font = new XFont("Consolas", 8, XFontStyle.Regular);

            double height = 24;
            int marginSpace = 10;
            XUnit xMin = marginSpace;
            XUnit xMax = document.Pages[0].Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = document.Pages[0].Height - marginSpace;

            List<String> headers = new List<String>();
            headers.Add("Gas"); headers.Add("Ele"); headers.Add("Wat");
            foreach (Object[] item in parkCharges) if (item[0].ToString() != "Balance Forward") headers.Add(item[0].ToString());
            headers.AddRange(tempCharges);
            headers.Add("Balance Forward");
            headers.Add("Total");
            String headerRow = "";
            foreach (String item in headers) if (item.Length >= 10) headerRow += String.Format("{0, -10}", item.Substring(0, 9)); else headerRow += String.Format("{0, -10}", item);
            headerRow += String.Format("{0,-15}{1,-15}{2,-10}", "Payment", "Balance", "Dep.No");

            int i = 1;
            tf.DrawString("Total Listing Report" + String.Format("{0, 80}", "Page " + document.PageCount), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Park Number: {0}", parkNumber), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(parkName, font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("Special Utility Rates: #-CARE    @-Medical", font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString("Sp#", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("Tenant", font, XBrushes.Black, new XRect(xMin + 30, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(headerRow, font, XBrushes.Black, new XRect(xMin + 160, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            i++;

            decimal[] finalTotals = new decimal[4 + parkCharges.Count + tempCharges.Count];
            decimal[] pageTotals = new decimal[4 + parkCharges.Count + tempCharges.Count];
            for (int k = 0; k < pageTotals.Length; k++) pageTotals[k] = 0M;
            for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] = 0M;
            String pageTotalRow = "";
            foreach (Dictionary<String, Object> dict in info) {
                decimal total = 0.0M;
                tf.DrawString(dict["OrderID"].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, 20, height), XStringFormats.TopLeft);
                tf.DrawString(dict["Tenant"].ToString(), font, XBrushes.Black, new XRect(xMin + 30, yMin + height * i, 120, height), XStringFormats.TopLeft);
                String gasSpecial = "";
                String eleSpecial = "";
                String watSpecial = "";
                if ((int)dict["EleMedical"] > 0) eleSpecial += "@";
                if ((int)dict["GasMedical"] > 0) gasSpecial += "@";
                if (!(string.Equals("Regular", dict["GasStatus"].ToString(), StringComparison.OrdinalIgnoreCase) || dict["GasStatus"].ToString() == "")) gasSpecial += "#";
                if (!(string.Equals("Regular", dict["EleStatus"].ToString(), StringComparison.OrdinalIgnoreCase) || dict["EleStatus"].ToString() == "")) eleSpecial += "#";
                if (!(string.Equals("Regular", dict["WatStatus"].ToString(), StringComparison.OrdinalIgnoreCase) || dict["WatStatus"].ToString() == "")) watSpecial += "#";
                String row = String.Format("{3, -2}{0,-8:N2}{4, -2}{1,-8:N2}{5, -2}{2,-8:N2}", dict["GasBill"], dict["EleBill"], dict["WatBill"], gasSpecial, eleSpecial, watSpecial);
                pageTotals[0] += (decimal)dict["GasBill"];
                pageTotals[1] += (decimal)dict["EleBill"];
                pageTotals[2] += (decimal)dict["WatBill"];
                decimal balFor = 0M;
                for (int k = 0; k < parkCharges.Count; k++) {
                    Object[] item = (Object[])parkCharges[k];
                    if (item[0].ToString() == "Balance Forward") {
                        if (dict.Keys.Contains(item[0].ToString())) balFor = (decimal)dict[item[0].ToString()];
                        continue; 
                    } else if (dict.Keys.Contains(item[0].ToString())) {
                        row += String.Format("{0,-10:N2}", (decimal)dict[item[0].ToString()]);
                        pageTotals[k + 3] += (decimal)dict[item[0].ToString()];
                        total += (decimal)dict[item[0].ToString()];
                    } else {
                        row += String.Format("{0,-10}", "");
                    }
                }
                for (int k = 0; k < tempCharges.Count; k++) {
                    if (dict.Keys.Contains(tempCharges[k])) {
                        row += String.Format("{0,-10:N2}", (decimal)dict[tempCharges[k]]);
                        pageTotals[k + parkCharges.Count + 2] += (decimal)dict[tempCharges[k]];
                        total += (decimal)dict[tempCharges[k]];
                    } else {
                        row += String.Format("{0,-10}", "");
                    }
                }
                row += String.Format("{0,-10:N2}", balFor);
                pageTotals[tempCharges.Count + parkCharges.Count + 2] += balFor;
                total += balFor;

                total += (decimal)dict["GasBill"] + (decimal)dict["EleBill"] + (decimal)dict["WatBill"];
                pageTotals[pageTotals.Length - 1] += total;
                row += String.Format("{0,-10:N2}", total);
                row += String.Format("{0,-15}{1,-15}{2,-10}", "________|____", "________|____", "____");
                tf.DrawString(row, font, XBrushes.Black, new XRect(xMin + 160, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                i++;

                if (height * (i+8) >= yMax) {
                    for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] += pageTotals[k];
                    pageTotalRow = "";
                    for (int k = 0; k < pageTotals.Length; k++) {
                        pageTotalRow += String.Format("{0, -10:N2}", pageTotals[k]);
                        pageTotals[k] = 0M;
                    }
                    tf.DrawString("Page Total", font, XBrushes.Black, new XRect(xMin + 30, yMax - height * 5, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString(pageTotalRow, font, XBrushes.Black, new XRect(xMin + 160, yMax - height * 5, xMax, height), XStringFormats.TopLeft);

                    tf = createNewPageParkSumm(ref document);
                    i = 1;
                    tf.DrawString("Total Listing Report" + String.Format("{0, 80}", "Page " + document.PageCount), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Park Number: {0}", parkNumber), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(parkName, font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString("Special Utility Rates: #-CARE    @-Medical", font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString("Sp#", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString("Tenant", font, XBrushes.Black, new XRect(xMin + 30, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString(headerRow, font, XBrushes.Black, new XRect(xMin + 160, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    i++;
                }
            }
            for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] += (decimal)pageTotals[k];
            pageTotalRow = "";
            for (int k = 0; k < pageTotals.Length; k++) pageTotalRow += String.Format("{0, -10:N2}", pageTotals[k]);
            tf.DrawString("Page Total", font, XBrushes.Black, new XRect(xMin + 30, yMax - height * 5, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(pageTotalRow, font, XBrushes.Black, new XRect(xMin + 160, yMax - height * 5, xMax, height), XStringFormats.TopLeft);
            String parkTotal = "";
            foreach (decimal item in finalTotals) parkTotal += String.Format("{0,-10:N2}", item);
            tf.DrawString("Park Total", font, XBrushes.Black, new XRect(xMin + 30, yMax - height * 4, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(parkTotal, font, XBrushes.Black, new XRect(xMin + 160, yMax - height * 4, xMax, height), XStringFormats.TopLeft);

            tf = createNewPageParkSumm(ref document);
            i = 1;
            tf.DrawString("Total Listing Report", font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(parkName, font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMax - 80, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;

            for (int k = 0; k < finalTotals.Length; k++) {
                String summaryFormat = "{0, -20}{1, -30:N2}";
                tf.DrawString(String.Format(summaryFormat, headers[k], finalTotals[k]), font, XBrushes.Black, new XRect((xMax-xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); 
                i++;
            }

            tf = createNewPageParkSumm(ref document);
            String blank = "_______________";
            height = 24;
            i = 1;
            tf.DrawString("Monthly Audit Report", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(parkName, font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("{0}", "Financial Accountability"), font, XBrushes.Black, new XRect((xMax - xMin) / 8, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0}", "Deposit Information"), font, XBrushes.Black, new XRect((xMax - xMin) * 5 / 8, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("{0, -30}{1, 15}{2, 25}", "Current Charges:", "Variance:", "Amount Collected:"), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;

            for (int k = 0; k < finalTotals.Length; k++) {
                String summaryFormat = "{0,-15}{1, 15:N2}{2, 20}{2, 20}";
                tf.DrawString(String.Format(summaryFormat, headers[k], finalTotals[k], blank), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                i++;
            }
            tf.DrawString(String.Format("{0} {1}", "Prior Adjustments", "____________"), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("{0,-10}{1, 20}{1, 20}{1, 20}", "Total:", "==============="), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString("Miscellaneous Deposits", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("{0}     {1}     {2}", "  (Deposit Dates)", "(Deposit Amount)", "(Items)"), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            for (int k = 1; k <= 4; k++) {
                tf.DrawString(String.Format("{0}. {1,-20}{1,-20}{1,-20}", k, blank), font, XBrushes.Black, new XRect(xMin, yMin + height * (k + i), xMax, height), XStringFormats.TopLeft);
            }
            tf.DrawString(String.Format("{0} {1}", "    (Deposit Dates)", "(Deposit Amount)"), font, XBrushes.Black, new XRect((xMax-xMin) / 2, yMin + height * 4, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0} {1}", "    (Deposit Dates)", "(Deposit Amount)"), font, XBrushes.Black, new XRect((xMax - xMin) * 3 / 4, yMin + height * 4, xMax, height), XStringFormats.TopLeft);
            for (int k = 1; k <= 16; k++) {
                tf.DrawString(String.Format("{0, 2}  {1}  {1}", k, blank), font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * (k + 4), xMax, height), XStringFormats.TopLeft);
                tf.DrawString(String.Format("{0, 2}  {1}  {1}", k+16, blank), font, XBrushes.Black, new XRect((xMax - xMin) * 3 / 4, yMin + height * (k + 4), xMax, height), XStringFormats.TopLeft);
            }
            tf.DrawString(String.Format("      {0}      {1}     --->      {0}      {1}", "Sub Total", blank), font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * 21, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0}      {1}", "Total Deposits:", blank), font, XBrushes.Black, new XRect((xMax - xMin) * 3 / 4, yMin + height * 22, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("This report submitted without benefit of audit:", font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * 23, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0}{0}{0}", blank), font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * 24, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("Manager", font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * 25, xMax, height), XStringFormats.TopLeft);

            tf = createNewPageParkSumm(ref document);
            i = 1;

            tf.DrawString("Account Receivable Control Schedule", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(parkName, font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            String stringRow = String.Format("{0,12}{1,12}{2,12}{3,12}", "Space", "Gas", "Ele", "Wat");
            foreach (Object[] item in parkCharges) stringRow += String.Format("{0,12}", item[0]);
            stringRow += String.Format("{0,12}{1,12}{2, 12}", "Other", "TmpChrg", "Total");
            tf.DrawString(stringRow, font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            stringRow = ""; stringRow += String.Format("{0,12}", info.Count);
            foreach (decimal item in finalTotals) stringRow += String.Format("{0,12:N2}", item);
            tf.DrawString(stringRow, font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            stringRow = String.Format("{0,12}{0,12}{0,12}{0,12}{0,12}{0,12}{0, 12}", "__________");
            foreach (Object[] item in parkCharges) stringRow += String.Format("{0,12}", "__________");
            for (int k = 0; k < info.Count; k++) {
                tf.DrawString(stringRow, font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                i++;
                if (yMin + height * i > yMax) {
                    break;
                    tf = createNewPageParkSumm(ref document);
                    i = 0;
                }
            }

            tf = createNewPageParkSumm(ref document);
            i = 0;
            tf.DrawString("Comments", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString("For Adjustments", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            for (int k = 0; k < info.Count; k++) {
                tf.DrawString(String.Format("{0}   {0}{0}{0}{0}{0}{0}{0}{0}", blank), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                i++;
                if (yMin + height * i > yMax) {
                    break;
                    tf = createNewPageParkSumm(ref document);
                    i = 0;
                }
            }

            string path = String.Format("ParkReports\\TotalListingsReport{0}_{1}.pdf", parkNumber, dueDate.ToString("MMddyyyy"));
            try
            {
                document.Save(path);
                Process.Start(path);
            }
            catch
            {
                MessageBox.Show(path + " is in use");
                return;
            }
        }

        public static XTextFormatter createNewPageParkSumm(ref PdfDocument document) {
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromInch(14);
            page.Height = XUnit.FromInch(11);
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;
            return tf;
        }

        public static void createParkUtilReport(int parkId, String parkName, DateTime start, DateTime end, List<Object[]> gas, List<Object[]> ele, List<Object[]> wat) {
            Directory.CreateDirectory("ParkReports");
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            UtilityReport util = new UtilityReport(parkId, parkName, document, start, end);
            util.gasUtilReport(gas);
            util.eleUtilReport(ele);
            util.watUtilReport(wat);

            string path = String.Format("ParkReports\\UtilReport{0}.pdf", parkId);
            try {
                document.Save(path);
                Process.Start(path);
            } catch {
                MessageBox.Show(path + " is in use");
                return;
            }
        }

        public static void createParkExtraChargeReport(int parkNumber, String parkName, DateTime start, DateTime end, List<Object[]> spaces, List<String> statuses) {
            Directory.CreateDirectory("ParkReports");
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            double height = 16;
            int marginSpace = 20;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;

            String legend = "";
            for (int k = 0; k < statuses.Count; k++) {
                legend += String.Format("{0} = {1}\n", k, statuses[k]);
            }
            legend += String.Format("{0} = {1}\n", 'M', "Medical");

            String format = "{0,-8}{1,-40}{2,8:N2}{3,8:N2}";
            int i = 1;
            tf.DrawString("Summary of Extra Charges" + String.Format("{0, 80}", "Page " + document.PageCount), font, XBrushes.Black,
                new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(legend, font, XBrushes.Black,
                new XRect((xMin + xMax) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("Park Number: {0}", parkNumber), font, XBrushes.Black,
                new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Park Name: {0}", parkName), font, XBrushes.Black,
                new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i+=4;
            tf.DrawString(String.Format(format+"{4,24}", "Space", "Name", "BalFwd", "LtChg", "One Time Charges"), font, XBrushes.Black,
                new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0,8}{1,8}", "Gas", "Ele" ), font, XBrushes.Black,
                new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            decimal[] pageTotal = new decimal[] { 0M, 0M };
            decimal[] finalTotal = new decimal[] { 0M, 0M };
            foreach (Object[] space in spaces) {
                String oneTimeCharges = String.Format(format, space[0], space[1], space[2], space[3]);
                List<Object[]> temps = (List<Object[]>)space[4];
                foreach (Object[] temp in temps) {
                    oneTimeCharges += String.Format("{0,8:N2}",(decimal)temp[0]);
                }

                tf.DrawString(oneTimeCharges, font, XBrushes.Black,
                    new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);

                String status = "";
                String holder = "";
                if (statuses.IndexOf(space[5].ToString()) != -1) holder += statuses.IndexOf(space[5].ToString());
                if ((int)space[7] != 0) holder += 'M';
                status += String.Format("{0,8}", holder);
                holder = "";
                if (statuses.IndexOf(space[6].ToString()) != -1) holder += statuses.IndexOf(space[6].ToString());
                if ((int)space[7] != 0) holder += 'M';
                status += String.Format("{0,8}", holder);

                tf.DrawString(status, font, XBrushes.Black,
                    new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;

                pageTotal[0] += (decimal)space[2];
                pageTotal[1] += (decimal)space[3];
                finalTotal[0] += (decimal)space[2];
                finalTotal[1] += (decimal)space[3];

                if (height * (i + 3) >= yMax) {
                    tf.DrawString(String.Format(format, null, "Page Total", pageTotal[0], pageTotal[1]), font, XBrushes.Black, new XRect(xMin, yMax - height, xMax, height), XStringFormats.TopLeft);
                    pageTotal = new decimal[] { 0M, 0M };

                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx);
                    i = 1;
                    tf.DrawString("Summary of Extra Charges" + String.Format("{0, 80}", "Page " + document.PageCount), font, XBrushes.Black,
                        new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(legend, font, XBrushes.Black,
                        new XRect((xMin + xMax) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString(String.Format("Park Number: {0}", parkNumber), font, XBrushes.Black,
                        new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Park Name: {0}", parkName), font, XBrushes.Black,
                        new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i += 4;
                    tf.DrawString(String.Format(format + "{4,24}", "Space", "Name", "BalFwd", "LtChg", "One Time Charges"), font, XBrushes.Black,
                        new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString(String.Format("{0,8}{1,8}", "Gas", "Ele"), font, XBrushes.Black,
                        new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                }
            }
            tf.DrawString(String.Format(format, null, "Page Total", pageTotal[0], pageTotal[1]), font, XBrushes.Black, new XRect(xMin, yMax - height, xMax, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format(format, null, "Park Total", finalTotal[0], finalTotal[1]), font, XBrushes.Black, new XRect(xMin, yMax, xMax, height), XStringFormats.TopLeft);

            string path = String.Format("ParkReports\\ExtraChargesReport{0}.pdf", parkNumber);
            try {
                document.Save(path);
            } catch {
                MessageBox.Show(path + " is in use");
                return;
            }
        }

        public static void generateReadSheet(int parkNumber, String parkName, ArrayList spaces, DateTime prevDate) {
            Directory.CreateDirectory("ParkReports");
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            double height = 16;
            int marginSpace = 20;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;
            String format = "{0,8}{1,14}{2,14}{3,14}{4,14}{5,14}{6,14}";
            String blank = "__________";

            int i = 1;
            tf.DrawString("Utility Read Sheet",
                font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("Page " + document.PageCount,
                font, XBrushes.Black, new XRect(xMax - 100, yMin + height * i, 100, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("ParkNumber: {0}", parkNumber),
                font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("ParkName: {0}", parkName),
                font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format(format, null, "GAS", null, "ELE", null, "WAT", null),
                font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format(format, "Space", "PrevRead", "CurrRead", "PrevRead", "CurrRead","PrevRead", "CurrRead"),
                font, XBrushes.Black, new XRect(xMin, yMin + height*i, xMax, height), XStringFormats.TopLeft); i++;
            foreach (Dictionary<String, Object> space in spaces) {
                tf.DrawString(String.Format(format, space["OrderID"], space["GasReadValue"], blank, space["EleReadValue"], blank, space["WatReadValue"], blank),
                    font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                i++;
                if (height * (i + 3) >= yMax) {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx);
                    i = 1;
                    tf.DrawString("Utility Read Sheet",
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString("Page " + document.PageCount,
                        font, XBrushes.Black, new XRect(xMax - 100, yMin + height * i, 100, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("ParkNumber: {0}", parkNumber),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("ParkName: {0}", parkName),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format(format, null, "GAS", null, "ELE", null, "WAT", null),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format(format, "Space", "PrevRead", "CurrRead", "PrevRead", "CurrRead", "PrevRead", "CurrRead"),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                }
            }

            string path = String.Format("ParkReports\\ReadSheet{0}.pdf", parkNumber);
            try {
                document.Save(path);
            } catch {
                MessageBox.Show(path + " is in use");
                return;
            }
        }

        public static void generateReadSheet2(int parkNumber, String parkName, ArrayList spaces1, ArrayList spaces2, DateTime dueDate) {
            Directory.CreateDirectory("ParkReports");
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            double height = 16;
            int marginSpace = 20;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;
            String format = "{0,8}{1,14}{2,14}{3,14}{4,14}{5,14}{6,14}";
            String blank = "__________";

            int i = 1;
            tf.DrawString("Utility Read Sheet",
                font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("Page " + document.PageCount,
                font, XBrushes.Black, new XRect(xMax - 100, yMin + height * i, 100, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("ParkNumber: {0}", parkNumber),
                font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("ParkName: {0}", parkName),
                font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format(format, null, "GAS", null, "ELE", null, "WAT", null),
                font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format(format, "Space", "PrevRead", "CurrRead", "PrevRead", "CurrRead", "PrevRead", "CurrRead"),
                font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            for (int j = 0; j < spaces1.Count; j++ ) {
                Dictionary<string, object> space1 = (Dictionary<string, object>)spaces1[j];
                Dictionary<string, object> space2 = (Dictionary<string, object>)spaces2[j];
                tf.DrawString(String.Format(format, space1["OrderID"], space1["GasReadValue"], space2["GasReadValue"], 
                    space1["EleReadValue"], space2["EleReadValue"], space1["WatReadValue"], space2["WatReadValue"]),
                    font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                i++;
                if (height * (i + 3) >= yMax) {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx);
                    i = 1;
                    tf.DrawString("Utility Read Sheet",
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString("Page " + document.PageCount,
                        font, XBrushes.Black, new XRect(xMax - 100, yMin + height * i, 100, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("ParkNumber: {0}", parkNumber),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("ParkName: {0}", parkName),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format(format, null, "GAS", null, "ELE", null, "WAT", null),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format(format, "Space", "PrevRead", "CurrRead", "PrevRead", "CurrRead", "PrevRead", "CurrRead"),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                }
            }

            string path = String.Format("ParkReports\\ReadSheet{0}.pdf", parkNumber);
            try {
                document.Save(path);
            } catch {
                MessageBox.Show(path + " is in use");
                return;
            }
        }

        private class UtilityReport {
            int parkNumber;
            String parkName;
            DateTime start, end;
            PdfDocument document;
            XUnit xMin, xMax, yMin, yMax;
            static int marginSpace = 11;
            static XFont font = new XFont("Consolas", 11, XFontStyle.Regular);
            static double height = 14;
            public UtilityReport(int parkNumber, String parkName, PdfDocument document, DateTime start, DateTime end) {
                this.parkNumber = parkNumber;
                this.parkName = parkName;
                this.start = start;
                this.end = end;
                this.document = document;
                xMin = yMin = marginSpace;
                xMax = XUnit.FromInch(8.5) - marginSpace;
                yMax = XUnit.FromInch(11) - marginSpace;
            }

            private XTextFormatter newPage() {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XTextFormatter tf = new XTextFormatter(gfx);
                return tf;
            }

            public void gasUtilReport(List<Object[]> info) {
                String rowFormat = "{0,4}{1,8}{2,8}{3,6}{4,10:N2}{5,10:N2}{6,10:N2}{7,10:N2}{8,10:N2}{9,12:N2}";
                String[] headers = new String[] { "Sp#", "PrevRd", "CurrRd", "Units", "CustCh", "Base", "Over", "Surch", "Tax", "Total" };

                XTextFormatter tf = newPage();
                gasHeader(tf, "{0,4}{1,8}{2,8}{3,6}{4,10}{5,10}{6,10}{7,10}{8,10}{9,12}", headers);

                object[] pageTotal = new object[7];
                object[] parkTotal = new object[7];
                for (int i = 0; i < 7; i++) {
                    pageTotal[i] = 0M;
                    parkTotal[i] = 0M;
                }

                int j = 0;
                foreach (Object[] row in info) {
                    if (j == 40) {
                        tf.DrawString(String.Format("Page Total{0,16:G29}{1,10:N2}{2,10:N2}{3,10:N2}{4,10:N2}{5,10:N2}{6,12:N2}", pageTotal), font, XBrushes.Black, new XRect(xMin, yMax - height * 4, xMax, height), XStringFormats.TopLeft);
                        tf = newPage();
                        gasHeader(tf, "{0,4}{1,8}{2,8}{3,6}{4,10}{5,10}{6,10}{7,10}{8,10}{9,12}", headers);
                        for (int i = 0; i < 7; i++) pageTotal[i] = 0M;
                        j = 0;
                    }
                    if (row == null) continue;
                    string spaceId = row[0].ToString();
                    int prevRead = (int)row[1];
                    int currRead = (int)row[2];
                    Dictionary<string, decimal> costInfo = (Dictionary<string, decimal>)row[3];
                    Object[] result = new Object[] { spaceId, prevRead, currRead, currRead - prevRead, costInfo["CustCharge"], costInfo["Base"], costInfo["OverBase"], costInfo["Surcharge"], costInfo["Tax"], costInfo["Total"] };
                    for (int i = 0; i < 7; i++) {
                        pageTotal[i] = (decimal)pageTotal[i] + Convert.ToDecimal(result[i + 3]);
                        parkTotal[i] = (decimal)parkTotal[i] + Convert.ToDecimal(result[i + 3]);
                    }
                    tf.DrawString(String.Format(rowFormat, result), font, XBrushes.Black, new XRect(xMin, yMin+height*(10+j), xMax, height), XStringFormats.TopLeft);
                    j++;
                }
                tf.DrawString(String.Format("Page Total{0,16:G29}{1,10:N2}{2,10:N2}{3,10:N2}{4,10:N2}{5,10:N2}{6,12:N2}", pageTotal), font, XBrushes.Black, new XRect(xMin, yMax - height * 4, xMax, height), XStringFormats.TopLeft);
                tf.DrawString(String.Format("Park Total{0,16:G29}{1,10:N2}{2,10:N2}{3,10:N2}{4,10:N2}{5,10:N2}{6,12:N2}", parkTotal), font, XBrushes.Black, new XRect(xMin, yMax - height * 3, xMax, height), XStringFormats.TopLeft);
            }

            private void gasHeader(XTextFormatter tf, String format, String[] headers) {
                tf.DrawString(String.Format("{0}  #{1}",parkName,parkNumber), font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height, xMax, height), XStringFormats.TopLeft); 
                tf.DrawString("Gas Utility Charge Report", font, XBrushes.Black, new XRect(xMin, yMin + height, xMax, height), XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMin, yMin + height, xMax - xMin, height), XStringFormats.TopLeft);
                tf.DrawString("Current Read Date " + end.Date.ToString("d"), font, XBrushes.Black, new XRect(xMin, yMin + height * 2, xMax - xMin, height), XStringFormats.TopLeft);
                tf.DrawString("Previous Read Date " + start.Date.ToString("d"), font, XBrushes.Black, new XRect(xMin, yMin + height * 3, xMax - xMin, height), XStringFormats.TopLeft);
                tf.DrawString("Billing Days " + (int)(end - start).TotalDays, font, XBrushes.Black, new XRect(xMin, yMin + height * 4, xMax - xMin, height), XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(String.Format(format, headers), font, XBrushes.Black, new XRect(xMin, yMin + height * 8, xMax, height), XStringFormats.TopLeft);
            }

            public void eleUtilReport(List<Object[]> info) {
                String rowFormat = "{0,4}{1,8}{2,8}{3,6}{4,10:N2}{5,10:N2}{6,10:N2}{7,10:N2}{8,10:N2}{9,10:N2}{10,12:N2}";
                String[] headers = new String[] { "Sp#", "PrevRd", "CurrRd", "Units", "CustCh", "Tier1", "Tier2", "Tier3-5", "Surch", "Tax", "Total" };

                XTextFormatter tf = newPage();
                eleHeader(tf, "{0,4}{1,8}{2,8}{3,6}{4,10}{5,10}{6,10}{7,10}{8,10}{9,10}{10,12}", headers);

                object[] pageTotal = new object[8];
                object[] parkTotal = new object[8];
                for (int i = 0; i < 8; i++) {
                    pageTotal[i] = 0M;
                    parkTotal[i] = 0M;
                }

                int j = 0;
                foreach (Object[] row in info) {
                    if (j == 40) {
                        tf.DrawString(String.Format("Page Total{0,16:G29}{1,10:N2}{2,10:N2}{3,10:N2}{4,10:N2}{5,10:N2}{6,10:N2}{7,12:N2}", pageTotal), font, XBrushes.Black, new XRect(xMin, yMax - height * 4, xMax, height), XStringFormats.TopLeft);
                        tf = newPage();
                        eleHeader(tf, "{0,4}{1,8}{2,8}{3,6}{4,10}{5,10}{6,10}{7,10}{8,10}{9,10}{10,12}", headers);
                        for (int i = 0; i < 8; i++) pageTotal[i] = 0M;
                        j = 0;
                    }

                    string spaceId = row[0].ToString();
                    int prevRead = (int)row[1];
                    int currRead = (int)row[2];
                    Dictionary<string, decimal> costInfo = (Dictionary<string, decimal>)row[3];
                    Object[] result = new Object[] { spaceId, prevRead, currRead, currRead - prevRead, costInfo["CustCharge"], costInfo["Tier1"], costInfo["Tier2"], costInfo["Tier3-5"] ,costInfo["Surcharge"], costInfo["Tax"], costInfo["Total"] };
                    for (int i = 0; i < 8; i++) {
                        pageTotal[i] = (decimal)pageTotal[i] + Convert.ToDecimal(result[i + 3]);
                        parkTotal[i] = (decimal)parkTotal[i] + Convert.ToDecimal(result[i + 3]);
                    }
                    tf.DrawString(String.Format(rowFormat, result), font, XBrushes.Black, new XRect(xMin, yMin + height * (10 + j), xMax, height), XStringFormats.TopLeft);
                    j++;
                }
                tf.DrawString(String.Format("Page Total{0,16:G29}{1,10:N2}{2,10:N2}{3,10:N2}{4,10:N2}{5,10:N2}{6,10:N2}{7,12:N2}", pageTotal), font, XBrushes.Black, new XRect(xMin, yMax - height * 4, xMax, height), XStringFormats.TopLeft);
                tf.DrawString(String.Format("Park Total{0,16:G29}{1,10:N2}{2,10:N2}{3,10:N2}{4,10:N2}{5,10:N2}{6,10:N2}{7,12:N2}", parkTotal), font, XBrushes.Black, new XRect(xMin, yMax - height * 3, xMax, height), XStringFormats.TopLeft);
            }

            private void eleHeader(XTextFormatter tf, String format, String[] headers) {
                tf.DrawString(String.Format("{0}  #{1}", parkName, parkNumber), font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height, xMax, height), XStringFormats.TopLeft);
                tf.DrawString("Ele Utility Charge Report", font, XBrushes.Black, new XRect(xMin, yMin + height, xMax, height), XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMin, yMin + height, xMax - xMin, height), XStringFormats.TopLeft);
                tf.DrawString("Current Read Date " + end.Date.ToString("d"), font, XBrushes.Black, new XRect(xMin, yMin + height * 2, xMax - xMin, height), XStringFormats.TopLeft);
                tf.DrawString("Previous Read Date " + start.Date.ToString("d"), font, XBrushes.Black, new XRect(xMin, yMin + height * 3, xMax - xMin, height), XStringFormats.TopLeft);
                tf.DrawString("Billing Days " + (int)(end - start).TotalDays, font, XBrushes.Black, new XRect(xMin, yMin + height * 4, xMax - xMin, height), XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(String.Format(format, headers), font, XBrushes.Black, new XRect(xMin, yMin + height * 8, xMax, height), XStringFormats.TopLeft);
            }

            public void watUtilReport(List<Object[]> info) {
                String rowFormat = "{0,4}{1,8}{2,8}{3,6}{4,10:N2}{5,10:N2}{6,10:N2}{7,10:N2}{8,10:N2}{9,12:N2}";
                String[] headers = new String[] { "Sp#", "PrevRd", "CurrRd", "Units", "CustCh", "Base", "OverBase", "Surch", "Tax", "Total" };

                XTextFormatter tf = newPage();
                watHeader(tf, "{0,4}{1,8}{2,8}{3,6}{4,10}{5,10}{6,10}{7,10}{8,10}{9,12}", headers);

                object[] pageTotal = new object[7];
                object[] parkTotal = new object[7];
                for (int i = 0; i < 7; i++) {
                    pageTotal[i] = 0M;
                    parkTotal[i] = 0M;
                }

                int j = 0;
                foreach (Object[] row in info) {
                    if (j == 40) {
                        tf.DrawString(String.Format("Page Total{0,16:G29}{1,10:N2}{2,10:N2}{3,10:N2}{4,10:N2}{5,10:N2}{6,12:N2}", pageTotal), font, XBrushes.Black, new XRect(xMin, yMax - height * 4, xMax, height), XStringFormats.TopLeft);
                        tf = newPage();
                        watHeader(tf, "{0,4}{1,8}{2,8}{3,6}{4,10}{5,10}{6,10}{7,10}{8,10}{9,12}", headers);
                        for (int i = 0; i < 7; i++) pageTotal[i] = 0M;
                        j = 0;
                    }

                    string spaceId = row[0].ToString();
                    int prevRead = (int)row[1];
                    int currRead = (int)row[2];
                    Dictionary<string, decimal> costInfo = (Dictionary<string, decimal>)row[3];
                    Object[] result = new Object[] { spaceId, prevRead, currRead, currRead - prevRead, costInfo["CustCharge"], costInfo["Base"], costInfo["OverBase"], costInfo["Surcharge"], costInfo["Tax"], costInfo["Total"] };
                    for (int i = 0; i < 7; i++) {
                        pageTotal[i] = (decimal)pageTotal[i] + Convert.ToDecimal(result[i + 3]);
                        parkTotal[i] = (decimal)parkTotal[i] + Convert.ToDecimal(result[i + 3]);
                    }
                    tf.DrawString(String.Format(rowFormat, result), font, XBrushes.Black, new XRect(xMin, yMin + height * (10 + j), xMax, height), XStringFormats.TopLeft);
                    j++;
                }
                tf.DrawString(String.Format("Page Total{0,16:G29}{1,10:N2}{2,10:N2}{3,10:N2}{4,10:N2}{5,10:N2}{6,12:N2}", pageTotal), font, XBrushes.Black, new XRect(xMin, yMax - height * 4, xMax, height), XStringFormats.TopLeft);
                tf.DrawString(String.Format("Page Total{0,16:G29}{1,10:N2}{2,10:N2}{3,10:N2}{4,10:N2}{5,10:N2}{6,12:N2}", parkTotal), font, XBrushes.Black, new XRect(xMin, yMax - height * 3, xMax, height), XStringFormats.TopLeft);
            }

            private void watHeader(XTextFormatter tf, String format, String[] headers) {
                tf.DrawString(String.Format("{0}  #{1}", parkName, parkNumber), font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height, xMax, height), XStringFormats.TopLeft);
                tf.DrawString("Wat Utility Charge Report", font, XBrushes.Black, new XRect(xMin, yMin + height, xMax, height), XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMin, yMin + height, xMax - xMin, height), XStringFormats.TopLeft);
                tf.DrawString("Current Read Date " + end.Date.ToString("d"), font, XBrushes.Black, new XRect(xMin, yMin + height * 2, xMax - xMin, height), XStringFormats.TopLeft);
                tf.DrawString("Previous Read Date " + start.Date.ToString("d"), font, XBrushes.Black, new XRect(xMin, yMin + height * 3, xMax - xMin, height), XStringFormats.TopLeft);
                tf.DrawString("Billing Days " + (int)(end - start).TotalDays, font, XBrushes.Black, new XRect(xMin, yMin + height * 4, xMax - xMin, height), XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(String.Format(format, headers), font, XBrushes.Black, new XRect(xMin, yMin + height * 8, xMax, height), XStringFormats.TopLeft);
            }
        }

        public static void gasUtilReport(PdfDocument document, int parkId, ArrayList info, DateTime start, DateTime end) {
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            int marginSpace = 20;
            XUnit xMin = marginSpace; XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace; XUnit yMax = page.Height - marginSpace;
            

            int i = 0;

            String rowFormat = "{0,5}{1,10}{2,10}{3,10}";
            String[] headers = new String[] { "Space", "PrevRead", "CurrRead", "Units" };

            //tf.DrawString(parkName, font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString("Gas Utility Charge Report", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMax - 40, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Current Read Date {0, 10}", end.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Previous Read Date {0, 9}", start.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Billing Days {0, 10}", (int)(end - start).TotalDays), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format(rowFormat, headers), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            

            decimal[] finalTotals = new decimal[1];
            Object[] pageTotals = new Object[3];
            pageTotals[1] = "Page Total";
            for (int k = 2; k < pageTotals.Length; k++) pageTotals[k] = 0;
            for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] = 0;

            foreach (Object[] row in info) {
                String space = row[0].ToString();
                int prevRead = (int)row[1];
                int currRead = (int)row[2];
                int usage = currRead - prevRead;
                tf.DrawString(String.Format(rowFormat, space, prevRead, currRead, usage), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                for (int k = 2; k < pageTotals.Length; k++) {
                    pageTotals[k] = (int)pageTotals[k] + usage;
                }
                if (height * (i + 3) >= yMax) {
                    for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] += (decimal)pageTotals[k + 2];
                    tf.DrawString(String.Format(rowFormat, pageTotals), font, XBrushes.Black, new XRect(xMin, yMax - height, xMax, height), XStringFormats.TopLeft);
                    for (int k = 2; k < pageTotals.Length; k++) pageTotals[k] = 0M;

                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx);
                    i = 0;
                    //tf.DrawString(parkName, font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString("Gas Utility Charge Report", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMax - 40, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Current Read Date {0, 10}", end.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Previous Read Date {0, 9}", start.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Billing Days {0, 10}", (int)(end - start).TotalDays), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format(rowFormat, headers), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                }
            }
            for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] += (int)pageTotals[k + 2];
            tf.DrawString(String.Format("Page Total{0,30}", pageTotals[2]), font, XBrushes.Black, new XRect(xMin, yMax - height, xMax, height), XStringFormats.TopLeft);
            i += 2;
            tf.DrawString(String.Format("Park Total{0,30}", finalTotals[0]), font, XBrushes.Black, new XRect(xMin, yMax, xMax, height), XStringFormats.TopLeft);

        }

        public static void eleUtilReport(PdfDocument document, int parkNumber, String parkName, ArrayList info, DateTime start, DateTime end) {
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            int marginSpace = 20;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;
            String rowFormat = "{0,5} {1,10} {2,10} {3,10}";
            String[] headers = new String[] { "Space", "PrevRead", "CurrRead", "Units" };

            int i = 0;
            tf.DrawString(parkName, font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString("Ele Utility Charge Report", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMax - 40, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Curr. Read Date {0, 10}", end.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Prev. Read Date {0, 10}", start.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Billing Days {0, 10}", (int)(end-start).TotalDays), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format(rowFormat, headers), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;

            decimal[] finalTotals = new decimal[1];
            Object[] pageTotals = new Object[3];
            pageTotals[1] = "Page Total";
            for (int k = 2; k < pageTotals.Length; k++) pageTotals[k] = 0;
            for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] = 0;

            foreach (Object[] row in info) {
                String space = row[0].ToString();
                int prevRead = (int)row[3];
                int currRead = (int)row[4];
                int usage = currRead - prevRead;
                tf.DrawString(String.Format(rowFormat, space, prevRead, currRead, usage), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;

                if (height * (i + 3) >= yMax) {
                    for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] += (decimal)pageTotals[k + 2];
                    tf.DrawString(String.Format(rowFormat, pageTotals), font, XBrushes.Black, new XRect(xMin, yMax - height, xMax, height), XStringFormats.TopLeft);
                    for (int k = 2; k < pageTotals.Length; k++) pageTotals[k] = 0;

                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx);
                    i = 0;
                    tf.DrawString(parkName, font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString("Ele Utility Charge Report", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMax - 40, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Current Read Date {0, 10}", end.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Previous Read Date {0, 9}", start.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Billing Days {0, 10}", (int)(end - start).TotalDays), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format(rowFormat, headers), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                }

                for (int k = 2; k < pageTotals.Length; k++) {
                    pageTotals[k] = (int)pageTotals[k] + usage;
                }
            }
            for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] += (int)pageTotals[k + 2];
            tf.DrawString(String.Format("Page Total{0,25}", pageTotals[2].ToString()), font, XBrushes.Black, new XRect(xMin, yMax - height, xMax, height), XStringFormats.TopLeft);
            i += 2;
            tf.DrawString(String.Format("Park Total{0,25}", finalTotals[0]), font, XBrushes.Black, new XRect(xMin, yMax, xMax, height), XStringFormats.TopLeft);

        }

        public static void watUtilReport(PdfDocument document, int parkNumber, String parkName, ArrayList info, DateTime start, DateTime end) {
            // Create an empty page
            //PdfPage page = document.AddPage(); 
            PdfPage page = document.AddPage();
            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            int marginSpace = 20;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;
            String rowFormat = "{0,5} {1,10} {2,10} {3,10}";
            String[] headers = new String[] { "Space", "PrevRead", "CurrRead", "Units" };

            int i = 0;
            tf.DrawString(parkName, font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString("Wat Utility Charge Report", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
            tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMax - 40, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Current Read Date {0, 10}", end.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Previous Read Date {0, 9}", start.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format("Billing Days {0, 10}", (int)(end - start).TotalDays), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
            tf.DrawString(String.Format(rowFormat, headers), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;

            decimal[] finalTotals = new decimal[1];
            Object[] pageTotals = new Object[3];
            pageTotals[1] = "Page Total";
            for (int k = 2; k < pageTotals.Length; k++) pageTotals[k] = 0;
            for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] = 0;

            foreach (Object[] row in info) {
                String space = row[0].ToString();
                int prevRead = (int)row[3];
                int currRead = (int)row[4];
                int usage = currRead - prevRead;
                tf.DrawString(String.Format(rowFormat, space, prevRead, currRead, usage), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                for (int k = 2; k < pageTotals.Length; k++) {
                    pageTotals[k] = (int)pageTotals[k] + usage;
                }
                if (height * (i + 3) >= yMax) {
                    for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] += (decimal)pageTotals[k + 2];
                    tf.DrawString(String.Format(rowFormat, pageTotals), font, XBrushes.Black, new XRect(xMin, yMax - height, xMax, height), XStringFormats.TopLeft);
                    for (int k = 2; k < pageTotals.Length; k++) pageTotals[k] = 0;

                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx);
                    i = 0;
                    tf.DrawString(parkName, font, XBrushes.Black, new XRect((xMax - xMin) / 2, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString("Wat Utility Charge Report", font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft);
                    tf.DrawString("Page " + document.PageCount, font, XBrushes.Black, new XRect(xMax - 40, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Current Read Date {0, 10}", end.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Previous Read Date {0, 9}", start.Date.ToString("d")), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format("Billing Days {0, 10}", (int)(end - start).TotalDays), font, XBrushes.Black, new XRect(xMax - 120, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                    tf.DrawString(String.Format(rowFormat, headers), font, XBrushes.Black, new XRect(xMin, yMin + height * i, xMax, height), XStringFormats.TopLeft); i++;
                }
            }
            for (int k = 0; k < finalTotals.Length; k++) finalTotals[k] += (int)pageTotals[k + 2];
            tf.DrawString(String.Format("Page Total{0,25}", pageTotals[2]), font, XBrushes.Black, new XRect(xMin, yMax - height, xMax, height), XStringFormats.TopLeft);
            i += 2;
            tf.DrawString(String.Format("Park Total{0,25}", finalTotals[0]), font, XBrushes.Black, new XRect(xMin, yMax, xMax, height), XStringFormats.TopLeft);

        }

        public static void writeTenantPdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] staticPdf) {
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;
            
            double width = xMax - xMin;
            tf.DrawString(String.Format("{0}    Space # {1}", staticPdf[0], staticPdf[2]), font, XBrushes.Black, new XRect(xMin, yMin, width, height), XStringFormats.TopLeft);
            tf.DrawString(staticPdf[1].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height, width, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0}    {1}    {2}", staticPdf[3], staticPdf[4], staticPdf[5]), font, XBrushes.Black, new XRect(xMin, yMin + height * 2, width, height), XStringFormats.TopLeft);
        }

        public static void writeParkPdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] staticPdf) {
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;
            double width = xMax - xMin;
            tf.DrawString(staticPdf[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin, width, height), XStringFormats.TopLeft);
            tf.DrawString(staticPdf[1].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height, width, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0}    {1}    {2}", staticPdf[2], staticPdf[3], staticPdf[4]), font, XBrushes.Black, new XRect(xMin, yMin + height * 2, width, height), XStringFormats.TopLeft);
        }

        public static void writeElePdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] elePdf) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Left;
            XTextFormatter tf2 = new XTextFormatter(gfx);
            tf2.Alignment = XParagraphAlignment.Right;
            double width = xMax - xMin;
            int i = 0;

            if (elePdf == null) {
                tf1.DrawString("ELECTRIC", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
                tf1.DrawString("Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(0M.ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                return;
            }

            tf1.DrawString("ELECTRIC", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)elePdf[0]).ToString("G29"), font, XBrushes.Black, new XRect(xMin - 10, yMin + height * i, width, height), XStringFormats.TopLeft);

            Object[] custCharge = (Object[])elePdf[1];
            if ((decimal)custCharge[1] != 0.0M) {
                i += 2;
                tf1.DrawString(custCharge[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)custCharge[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            if ((decimal)elePdf[3] != 0.0M) {
                i += 2;
                List<Object[]> dItems = (List<Object[]>)elePdf[2];
                foreach (Object[] item in dItems) {
                    if ((decimal)item[1] == 0.0M) { continue; }
                    tf1.DrawString(" " + item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                    tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                    i += 1;
                }
                tf1.DrawString("Delivery Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)elePdf[3]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            if ((decimal)elePdf[5] != 0.0M) {
                i += 2;
                List<Object[]> gItems = (List<Object[]>)elePdf[4];
                foreach (Object[] item in gItems) {
                    if ((decimal)item[1] == 0.0M) { continue; }
                    tf1.DrawString(" " + item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                    tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                    i += 1;
                }
                tf1.DrawString("Generation Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)elePdf[5]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            i += 2;
            List<Object[]> sItems = (List<Object[]>)elePdf[6];
            foreach (Object[] item in sItems) {
                if ((decimal)item[1] == 0.0M) { continue; } 
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }

            //i += 1;
            List<Object[]> tItems = (List<Object[]>)elePdf[7];
            foreach (Object[] item in tItems) {
                if ((decimal)item[1] == 0.0M) { continue; } 
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }
            tf1.DrawString(elePdf[8].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);

            i += 1;
            tf1.DrawString("Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)elePdf[9]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
        }

        public static void writeGasPdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] gasPdf) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Left;
            XTextFormatter tf2 = new XTextFormatter(gfx);
            tf2.Alignment = XParagraphAlignment.Right;
            double width = xMax - xMin;
            int i = 0;

            if (gasPdf == null) {
                tf1.DrawString("GAS", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
                tf1.DrawString("Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(0M.ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                return;
            }

            tf1.DrawString("GAS", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)gasPdf[0]).ToString("G29"), font, XBrushes.Black, new XRect(xMin - 10, yMin + height * i, width, height), XStringFormats.TopLeft);

            Object[] custCharge = (Object[])gasPdf[1];
            if ((decimal)custCharge[1] != 0.0M) {
                i += 2;
                tf1.DrawString(custCharge[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)custCharge[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            if ((decimal)gasPdf[3] != 0.0M) {
                i += 2;
                List<Object[]> gItems = (List<Object[]>)gasPdf[2];
                foreach (Object[] item in gItems) {
                    if (item.Length == 1) {
                        tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                        continue;
                    }
                    if ((decimal)item[1] == 0.0M) { continue; }
                    tf1.DrawString(" " + item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                    tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                    i += 1;
                }
                tf1.DrawString("Generation Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)gasPdf[3]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            i += 2;
            List<Object[]> sItems = (List<Object[]>)gasPdf[4];
            foreach (Object[] item in sItems) {
                tf1.DrawString(" " + item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }

            //i += 1;
            List<Object[]> tItems = (List<Object[]>)gasPdf[5];
            foreach (Object[] item in tItems) {
                if ((decimal)item[1] == 0.0M) { continue; }
                tf1.DrawString(" " + item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }
            
            tf1.DrawString(gasPdf[6].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            
            i += 1;
            tf1.DrawString("Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)gasPdf[7]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
        }

        public static void writeWatPdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] watPdf) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Left;
            XTextFormatter tf2 = new XTextFormatter(gfx);
            tf2.Alignment = XParagraphAlignment.Right;
            double width = xMax - xMin;
            int i = 0;

            if (watPdf == null) {
                tf1.DrawString("WATER", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
                tf1.DrawString("Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(0M.ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                return;
            }

            tf1.DrawString("WATER", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)watPdf[0]).ToString("G29"), font, XBrushes.Black, new XRect(xMin - 10, yMin + height * i, width, height), XStringFormats.TopLeft);

            Object[] custCharge = (Object[])watPdf[1];
            if ((decimal)custCharge[1] != 0.0M) {
                i += 2;
                tf1.DrawString(custCharge[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)custCharge[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            if ((decimal)watPdf[3] != 0.0M) {
                i += 2;
                List<Object[]> gItems = (List<Object[]>)watPdf[2];
                foreach (Object[] item in gItems) {
                    if ((decimal)item[1] == 0.0M) { continue; }
                    tf1.DrawString(" " + item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                    tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                    i += 1;
                }
                tf1.DrawString("Generation Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)watPdf[3]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            i += 1;
            List<Object[]> sItems = (List<Object[]>)watPdf[4];
            foreach (Object[] item in sItems) {
                if ((decimal)item[1] == 0.0M) { continue; }
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }

            List<Object[]> tItems = (List<Object[]>)watPdf[5];
            foreach (Object[] item in tItems) {
                if ((decimal)item[1] == 0.0M) { continue; }
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }

            i += 1;
            tf1.DrawString("Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)watPdf[6]).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
        }

        public static void writeUsagePdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] usageData) {
            double width = xMax - xMin;
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;
            Object[] obj = (Object[])usageData[0];
            for (int i = 0; i < 9; i++) {
                if (obj[i] == null || (int)obj[i] < 0) obj[i] = null;
            }
            tf.DrawString(String.Format("{0, -13}{1, -8}{2, -11}", obj[0], obj[1], obj[2]), font, XBrushes.Black,
                        new XRect(xMin, yMin, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0, -13}{1, -8}{2, -11}", obj[3], obj[4], obj[5]), font, XBrushes.Black,
                new XRect((xMax - xMin) / 3 + 20, yMin, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0, -13}{1, -8}{2, -11}", obj[6], obj[7], obj[8]), font, XBrushes.Black,
                new XRect((xMax - xMin) * 2 / 3 + 30, yMin, xMax - xMin, height), XStringFormats.TopLeft);
            for (int j = 1; j <= 3; j++) {
                Object[] items = (Object[])usageData[j];
                tf.DrawString(String.Format("{0, 3}{1, 15}{2, 12}{3, 13}{4, 13}{5, 14}{6, 9}{7, 11}", items), font, XBrushes.Black,
                    new XRect(xMin, yMin + height * (j+2), xMax - xMin, height), XStringFormats.TopLeft);
            }
        }

        public static void writeUsagePdfPrint(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] usageData) {
            double width = xMax - xMin;
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;
            Object[] obj = (Object[])usageData[0];
            for (int i = 0; i < 9; i++) {
                if (obj[i] == null || (int)obj[i] < 0) obj[i] = null;
            }
            tf.DrawString(String.Format("{0, -13}{1, -8}{2, -11}", obj[0], obj[1], obj[2]), font, XBrushes.Black,
                        new XRect(xMin, yMin, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0, -13}{1, -8}{2, -11}", obj[3], obj[4], obj[5]), font, XBrushes.Black,
                new XRect((xMax - xMin) / 3 + 20, yMin, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0, -13}{1, -8}{2, -11}", obj[6], obj[7], obj[8]), font, XBrushes.Black,
                new XRect((xMax - xMin) * 2 / 3 + 30, yMin, xMax - xMin, height), XStringFormats.TopLeft);
            for (int j = 1; j <= 3; j++) {
                Object[] items = (Object[])usageData[j];
                tf.DrawString(String.Format("{0, 3}{1, 16}{2, 12}{3, 12}{4, 12}{5, 15}{6, 8}{7, 12}", items), font, XBrushes.Black,
                    new XRect(xMin, yMin + height * (j + 2), xMax - xMin, height), XStringFormats.TopLeft);
            }
        }

        public static void writeSummaryPdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, List<Object[]> summary) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Left;
            XTextFormatter tf2 = new XTextFormatter(gfx);
            tf2.Alignment = XParagraphAlignment.Right;
            double width = xMax - xMin;
            int i = 0;
            foreach (Object[] item in summary) {
                if ((decimal)item[1] == 0M) continue;
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(Decimal.Round((decimal)item[1], 2).ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }
        }

        public static void writePayTotalPdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, List<Object[]> summary) {
            decimal total = 0M;
            foreach (Object[] item in summary) total += Convert.ToDecimal(item[1]);
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Right;
            double width = xMax - xMin;
            total = Decimal.Round(total, 2);
            tf1.DrawString(total.ToString(), font, XBrushes.Black, new XRect(xMin, yMin, width, height), XStringFormats.TopLeft);
        }

        public static void writeStatementNum(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, int number) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Right;
            double width = xMax - xMin;
            tf1.DrawString(number.ToString(), font, XBrushes.Black, new XRect(xMin, yMin, width - 10, height), XStringFormats.TopLeft);
        }

        public static void writeTopMessagePdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, String topMessage) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Left;
            double width = xMax - xMin;
            tf1.DrawString(topMessage, font, XBrushes.Black, new XRect(xMin, yMin, width, height * 5), XStringFormats.TopLeft);
        }

        public static void writeBotMessagePdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, String botMessage) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Left;
            double width = xMax - xMin;
            tf1.DrawString(botMessage, font, XBrushes.Black, new XRect(xMin, yMin, width, height * 5), XStringFormats.TopLeft);
        }

        public static void historyReport(String report, Object[] park, int orderId, List<Object[]> bills, List<Object[]> readsInfo) {
            Directory.CreateDirectory("HistoryReports");
            PdfDocument document = new PdfDocument();
            document.Info.Title = report;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            XFont font;
            font = new XFont("Consolas", 10, XFontStyle.Regular);
            double height = 18;
            int marginSpace = 20;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;

            tf.DrawString(String.Format("Park: {0}", park[0]),
                font, XBrushes.Black, new XRect(xMin, yMin + height * 0, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("Space: {0}", orderId),
                font, XBrushes.Black, new XRect(xMin, yMin + height * 2, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{1} Date: {0, -20}", DateTime.Today.Date.ToString("d"), report),
                font, XBrushes.Black, new XRect(xMin, yMin + height * 3, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0, -28}{1, -14}{2, -10}{3, 8}{4, 8}{5, 12}{6, 10}{7, 14}", 
                "Tenant Name", "Due Date", "Read Date", "Days", "Usage", "Read Value", "Average", "Bill"),
                font, XBrushes.Black, new XRect(xMin, yMin + height * 4, xMax - xMin, height), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, xMin, yMin + height * 4, xMax, yMin + height * 4);
            for (int i = 0; i < readsInfo.Count && i < 25; i++) {
                Object[] readInfo = readsInfo[i];
                Object[] bill = bills[i];
                if (!bill[1].Equals(readInfo[0])) { MessageBox.Show("bad"); }
                String name;
                if (bill[0].ToString().Length > 25) name = bill[0].ToString().Substring(0, 25);
                else name = bill[0].ToString();
                Object[] rowInfo = new Object[] { bill[0], readInfo[0], readInfo[2], ((DateTime)readInfo[2] - (DateTime)readInfo[1]).Days, 
                    bill[2], readInfo[3], 1.0 * (int)bill[2] / ((DateTime)readInfo[2] - (DateTime)readInfo[1]).Days, bill[3] };
                String row = String.Format("{0, -28}{1, -14:MM-dd-yyyy}{2, -10:MM-dd-yyyy}{3, 8}{4, 8}{5, 12}{6, 10:N3}{7, 14:C2}", rowInfo);
                tf.DrawString(row, font, XBrushes.Black, new XRect(xMin, yMin + height * (5 + i), xMax - xMin, height), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.Black, xMin, yMin + height * (5 + i), xMax, yMin + height * (5 + i));
            }

            string path = String.Format("HistoryReports\\{0}Park{1}_Space{2}.pdf", report, park[5], orderId);
            try {
                document.Save(path);
            } catch {
                MessageBox.Show(path + " is in use");
                return;
            }
            Process.Start(path);
        }

        public static void readIssues(Object[] park, DateTime dueDate, List<Object[]> gas, List<Object[]> ele, List<Object[]> wat) {
            Directory.CreateDirectory("ReadReports");
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Read Flags";

            readIssueHelper(document, (int)park[5], dueDate, gas, "Gas");
            readIssueHelper(document, (int)park[5], dueDate, ele, "Electricity");
            readIssueHelper(document, (int)park[5], dueDate, wat, "Water");

            string path = String.Format("ReadReports\\Park{0}.pdf", park[5]);
            try {
                document.Save(path);
            } catch {
                MessageBox.Show(path + " is in use");
                return;
            }
            Process.Start(path);
        }

        public static void readIssueHelper(PdfDocument document, int parkNumber, DateTime dueDate, List<Object[]> reads, String utility) {
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            XFont font;
            font = new XFont("Consolas", 10, XFontStyle.Regular);
            double height = 18;
            int marginSpace = 20;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;

            tf.DrawString(String.Format("Park: {0}", parkNumber),
                font, XBrushes.Black, new XRect(xMin, yMin + height * 1, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("Due Date: {0}", dueDate.ToString("d")),
                font, XBrushes.Black, new XRect(xMin, yMin + height * 2, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0} Read Issues", utility),
                font, XBrushes.Black, new XRect(xMin, yMin + height * 3, xMax - xMin, height), XStringFormats.TopLeft);
            tf.DrawString(String.Format("{0, -6}{1, -34}{2, -12}{3, -12}{4, 10}{5, 10}{6, 10}{7, 10}",
                "Space", "Tenant", "Start Date", "Read Date", "PrevRead", "CurrRead", "PrevAvg", "CurrAvg"),
                font, XBrushes.Black, new XRect(xMin, yMin + height * 4, xMax - xMin, height), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, xMin, yMin + height * 4, xMax, yMin + height * 4);

            int i = 0;
            foreach (Object[] read in reads) {
                if (read[1].ToString().Length >= 32) read[1] = read[1].ToString().Substring(0, 30);
                String row = String.Format("{0, -6}{1, -34}{2, -12:MM-dd-yyyy}{3, -12:MM-dd-yyyy}{4, 10}{5, 10}{6, 10}{7, 10}", read);
                tf.DrawString(row, font, XBrushes.Black, new XRect(xMin, yMin + height * (5 + i), xMax - xMin, height), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.Black, xMin, yMin + height * (5 + i), xMax, yMin + height * (5 + i));
                i++;
                if (yMin + height * (5 + i) > yMax) {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx);
                    tf.DrawString(String.Format("Park: {0}", parkNumber),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * 1, xMax - xMin, height), XStringFormats.TopLeft);
                    tf.DrawString(String.Format("Due Date: {0}", dueDate.ToString("d")),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * 2, xMax - xMin, height), XStringFormats.TopLeft);
                    tf.DrawString(String.Format("{0} Read Issues", utility),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * 3, xMax - xMin, height), XStringFormats.TopLeft);
                    tf.DrawString(String.Format("{0, -6}{1, -34}{2, -12}{3, -12}{4, 10}{5, 10}{6, 10}{7, 10}",
                        "Space", "Tenant", "Start Date", "Read Date", "PrevRead", "CurrRead", "PrevAvg", "CurrAvg"),
                        font, XBrushes.Black, new XRect(xMin, yMin + height * 4, xMax - xMin, height), XStringFormats.TopLeft);
                    i = 0;
                }
            }
        }
    }
}
