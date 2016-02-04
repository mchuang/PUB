using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUB {
    class PdfControl {
        static XFont font = new XFont("OCR A Extended", 8, XFontStyle.Regular);
        static double height = 8;
        public static void createBillPdf(Object[] bill) {
            int parkNumber = (int)bill[0];
            String spaceId = bill[1].ToString();
            Object[] tenantPdf = (Object[])bill[2];
            Object[] parkPdf = (Object[])bill[3];
            Object[] usagePdf = (Object[])bill[4];
            Object[] gasPdf = (Object[])bill[5];
            Object[] elePdf = (Object[])bill[6];
            Object[] watPdf = (Object[])bill[7];
            Object[] summaryPdf = (Object[])bill[8];
            
            String billForm = "form.pdf";
            PdfDocument billDocument = PdfReader.Open(billForm, PdfDocumentOpenMode.Import);

            Directory.CreateDirectory("Records");
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Test";
            // Create an empty page
            //PdfPage page = document.AddPage(); 
            PdfPage page = billDocument.Pages[0];
            document.AddPage(page);
            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(document.Pages[0]);

            int marginSpace = 0;
            XUnit xMin = marginSpace;
            XUnit xMax = page.Width - marginSpace;
            XUnit yMin = marginSpace;
            XUnit yMax = page.Height - marginSpace;

            double line = height;

            writeTenantPdf(gfx, xMin + 10, yMin + line * 16, xMin + 10 + xMax / 2, yMin + line * 20, tenantPdf);
            writeParkPdf(gfx, xMin + 10 + xMax / 2, yMin + line * 16, xMax, yMin + line * 20, parkPdf);
            writeUsagePdf(gfx, xMin + 20, yMin + line * 32, xMax, yMin + line * 40, usagePdf);
            writeGasPdf(gfx, xMin + 20, yMin + line * 44, xMin + (xMax - xMin) * 1 / 3 - 50, yMin + line * 60, gasPdf);
            writeElePdf(gfx, xMin + (xMax - xMin) / 3, yMin + line * 44, xMin + (xMax - xMin) * 2 / 3 - 50, yMin + line * 60, elePdf);
            writeWatPdf(gfx, xMin + (xMax - xMin) * 2/ 3, yMin + line * 44, xMin + (xMax - xMin) - 50, yMin + line * 60, watPdf);
            writeSummaryPdf(gfx, xMin + (xMax - xMin) * 2 / 3, yMin + line * 64, xMin + (xMax - xMin) - 50, yMin + line * 80, summaryPdf);

            string path = String.Format("Records\\{0}_{1}.pdf", parkNumber, spaceId);
            document.Save(path);
            Process.Start(path);
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
            tf1.DrawString("ELECTRIC", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)elePdf[0]).ToString("G29"), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);

            Object[] custCharge = (Object[])elePdf[1];
            if ((decimal)custCharge[1] != 0.0M) {
                i += 2;
                tf1.DrawString(custCharge[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)custCharge[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            if ((decimal)elePdf[3] != 0.0M) {
                i += 2;
                List<Object[]> dItems = (List<Object[]>)elePdf[2];
                foreach (Object[] item in dItems) {
                    if ((decimal)item[1] == 0.0M) { continue; }
                    tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                    tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                    i += 1;
                }
                tf1.DrawString("Delivery Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)elePdf[3]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            if ((decimal)elePdf[5] != 0.0M) {
                i += 2;
                List<Object[]> gItems = (List<Object[]>)elePdf[4];
                foreach (Object[] item in gItems) {
                    if ((decimal)item[1] == 0.0M) { continue; }
                    tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                    tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                    i += 1;
                }
                tf1.DrawString("Generation Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)elePdf[5]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            i += 2;
            List<Object[]> sItems = (List<Object[]>)elePdf[6];
            foreach (Object[] item in sItems) {
                if ((decimal)item[1] == 0.0M) { continue; } 
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }

            i += 1;
            List<Object[]> tItems = (List<Object[]>)elePdf[7];
            foreach (Object[] item in tItems) {
                if ((decimal)item[1] == 0.0M) { continue; } 
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }
            tf1.DrawString(elePdf[8].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);

            i += 1;
            tf1.DrawString("Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)elePdf[9]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
        }

        public static void writeGasPdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] gasPdf) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Left;
            XTextFormatter tf2 = new XTextFormatter(gfx);
            tf2.Alignment = XParagraphAlignment.Right;
            double width = xMax - xMin;
            int i = 0;
            tf1.DrawString("GAS", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)gasPdf[0]).ToString("G29"), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);

            Object[] custCharge = (Object[])gasPdf[1];
            if ((decimal)custCharge[1] != 0.0M) {
                i += 2;
                tf1.DrawString(custCharge[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)custCharge[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            if ((decimal)gasPdf[3] != 0.0M) {
                i += 2;
                List<Object[]> gItems = (List<Object[]>)gasPdf[2];
                foreach (Object[] item in gItems) {
                    if ((decimal)item[1] == 0.0M) { continue; }
                    tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                    tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                    i += 1;
                }
                tf1.DrawString("Generation Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)gasPdf[3]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            i += 2;
            List<Object[]> sItems = (List<Object[]>)gasPdf[4];
            foreach (Object[] item in sItems) {
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }

            i += 1;
            List<Object[]> tItems = (List<Object[]>)gasPdf[5];
            foreach (Object[] item in tItems) {
                if ((decimal)item[1] == 0.0M) { continue; }
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }
            
            tf1.DrawString(gasPdf[6].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
            
            i += 1;
            tf1.DrawString("Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)gasPdf[7]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
        }

        public static void writeWatPdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] watPdf) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Left;
            XTextFormatter tf2 = new XTextFormatter(gfx);
            tf2.Alignment = XParagraphAlignment.Right;
            double width = xMax - xMin;
            int i = 0;
            tf1.DrawString("WAT", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)watPdf[0]).ToString("G29"), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);

            Object[] custCharge = (Object[])watPdf[1];
            if ((decimal)custCharge[1] != 0.0M) {
                i += 2;
                tf1.DrawString(custCharge[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)custCharge[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            if ((decimal)watPdf[3] != 0.0M) {
                i += 2;
                List<Object[]> gItems = (List<Object[]>)watPdf[2];
                foreach (Object[] item in gItems) {
                    if ((decimal)item[1] == 0.0M) { continue; }
                    tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                    tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                    i += 1;
                }
                tf1.DrawString("Generation Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)watPdf[3]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
            }

            i += 2;
            List<Object[]> sItems = (List<Object[]>)watPdf[4];
            foreach (Object[] item in sItems) {
                if ((decimal)item[1] == 0.0M) { continue; }
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }

            i += 1;
            List<Object[]> tItems = (List<Object[]>)watPdf[5];
            foreach (Object[] item in tItems) {
                if ((decimal)item[1] == 0.0M) { continue; }
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }

            i += 1;
            tf1.DrawString("Total", font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
            tf2.DrawString(((decimal)watPdf[6]).ToString("C2"), font, XBrushes.Black, new XRect(xMin + 10, yMin + height * i, width, height), XStringFormats.TopLeft);
        }

        public static void writeUsagePdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] usageData) {
            double width = xMax - xMin;
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Right;
            Object[] obj = (Object[])usageData[0];
            for (int i = 0; i < 9; i++) {
                if (obj[i] != null) tf.DrawString(obj[i].ToString(), font, XBrushes.Black,
                        new XRect(xMin, yMin, xMin + (xMax - xMin) * i / 9, height), XStringFormats.TopLeft);
            }
            for (int j = 1; j <= 3; j++) {
                Object[] items = (Object[])usageData[j];
                for (int i = 0; i < 8; i++) {
                    if (items[i] != null) tf.DrawString(items[i].ToString(), font, XBrushes.Black,
                        new XRect(xMin, yMin + height * (j + 3), xMin + (xMax - xMin) * i / 8, height), XStringFormats.TopLeft);
                }
            }
        }

        public static void writeSummaryPdf(XGraphics gfx, XUnit xMin, XUnit yMin, XUnit xMax, XUnit yMax, Object[] summary) {
            XTextFormatter tf1 = new XTextFormatter(gfx);
            tf1.Alignment = XParagraphAlignment.Left;
            XTextFormatter tf2 = new XTextFormatter(gfx);
            tf2.Alignment = XParagraphAlignment.Right;
            double width = xMax - xMin;
            int i = 0;
            foreach (Object[] item in summary) {
                tf1.DrawString(item[0].ToString(), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                tf2.DrawString(((decimal)item[1]).ToString("G29"), font, XBrushes.Black, new XRect(xMin, yMin + height * i, width, height), XStringFormats.TopLeft);
                i += 1;
            }
        }
    }
}
