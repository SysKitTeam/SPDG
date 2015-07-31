using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace Acceleratio.SPDG.Generator
{
    public class SampleData
    {
        internal static List<string> Accounts;
        internal static List<string> Years;
        internal static List<string> BusinessDocsTypes;
        internal static List<string> Countries;
        internal static List<string> FirstNames;
        internal static List<string> LastNames;
        internal static List<string> Addresses;
        internal static List<string> Companies;
        internal static List<string> Cities;
        internal static List<string> PhoneNumbers;
        internal static List<string> EmailAddreses;
        internal static List<string> WebSites;
        private static Random randomGen = new Random();

        public static void PrepareSampleCollections()
        {
            Accounts = CreateSampleCollection("Accounts.csv");
            Years = CreateSampleCollection("Years.csv");
            BusinessDocsTypes = CreateSampleCollection("BusinessDocsTypes.csv");
            Countries = CreateSampleCollection("Countries.csv");
            Cities = CreateSampleCollection("Cities.csv");
            Companies = CreateSampleCollection("Companies.csv");
            FirstNames = CreateSampleCollection("FirstName.csv");
            LastNames = CreateSampleCollection("LastName.csv");
            Addresses = CreateSampleCollection("address.csv");
            PhoneNumbers = CreateSampleCollection("PhoneNumbers.csv");
            EmailAddreses = CreateSampleCollection("emails.csv");
            WebSites = CreateSampleCollection("WebSites.csv");
        }

        internal static List<string> CreateSampleCollection(string csvFileName)
        {
            var reader = new StreamReader(File.OpenRead(@"SampleData\" + csvFileName));
            List<string> listA = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                listA.Add(line);
            }

            return listA;
        }

        internal static string GetSampleValueAt(List<string> sampleCollection, int index)
        {
            return sampleCollection[index];
        }

        internal static string GetSampleValueRandom(List<string> sampleCollection)
        {
            int randomNumber = randomGen.Next(0, sampleCollection.Count()-1);

            return sampleCollection[randomNumber];
        }

        internal static string Clean(string val)
        {
            val = val.Replace(" ", "");
            val = val.Replace(",", "");
            val = val.Replace(".", "");
            val = val.Replace("/", "");
            val = val.Replace("\\", "");
            val = val.Replace("(", "");
            val = val.Replace(")", "");
            val = val.Replace("&", "");

            return val;
        }

        internal static int GetRandomNumber(int min, int max)
        {
            return randomGen.Next(min, max);
        }

        internal static DateTime GetRandomDate(int yearMin, int yearMax)
        {
            DateTime randomDate = new DateTime(GetRandomNumber(yearMin, yearMax), GetRandomNumber(1, 12), GetRandomNumber(1, 28));
            return randomDate;
        }

        internal static DateTime GetRandomDateCurrentMonth()
        {
            DateTime randomDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, GetRandomNumber(1, 28));
            return randomDate;
        }

        internal static byte[] CreateDocx()
        {
            string text = File.ReadAllText("SampleData\\document.xml");
            text = text.Replace("SPDGTitle", SampleData.GetSampleValueRandom ( SampleData.BusinessDocsTypes) );
            text = text.Replace("SPDGDate", "Date: " + SampleData.GetRandomDate(1990, 2015).ToShortDateString());
            text = text.Replace("SPDGUser", "Author: " + SampleData.GetSampleValueRandom(SampleData.FirstNames) + " " + SampleData.GetSampleValueRandom(SampleData.LastNames));
            text = text.Replace("SPDGIdentity", "ID: " + SampleData.GetRandomNumber(100000, 1000000).ToString());

            string lorem = File.ReadAllText("SampleData\\loreIpsum.txt");
            text = text.Replace("SPDGLoreIpsum", getMultipleLoremIpsum(lorem, 1));

            File.WriteAllText("SampleData\\SampleDocx\\word\\document.xml", text);

            string path = "SampleData\\SampleDocx";
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(path + "\\[Content_Types].xml", string.Empty);
                zip.AddFile(path + "\\_rels\\.rels", "_rels");
                zip.AddFile(path + "\\docProps\\app.xml", "docProps");
                zip.AddFile(path + "\\docProps\\core.xml", "docProps");
                zip.AddFile(path + "\\word\\_rels\\document.xml.rels", "word\\_rels");
                zip.AddFile(path + "\\word\\theme\\theme1.xml", "word\\theme");
                zip.AddFile(path + "\\word\\document.xml", "word");
                zip.AddFile(path + "\\word\\fontTable.xml", "word");
                zip.AddFile(path + "\\word\\settings.xml", "word");
                zip.AddFile(path + "\\word\\styles.xml", "word");
                zip.AddFile(path + "\\word\\webSettings.xml", string.Empty);

                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");

                MemoryStream stream = new MemoryStream();
                zip.Save(stream);

                byte[] bytes = new byte[stream.Length];
                bytes = stream.ToArray();
                stream.Close();

                return bytes;
            }
        }

        private static string getMultipleLoremIpsum(string lorem, int repeat)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0; i<repeat; i++)
            {
                if( i == 0 )
                {
                    sb.Append(lorem);
                }
                else
                {
                    //string[] sentences = lorem.Split('.');
                    //foreach(string s in sentences)
                    //{
                    //    sb.Append(s + Guid.NewGuid().ToString());
                    //}

                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var random = new Random();
                    //char ch1 = chars[random.Next(chars.Length)];
                    //char ch2 = chars[random.Next(chars.Length)];
                    string modifiedLorem = lorem;

                    for (int a = 0; a < 100; a++ )
                    {
                        char ch1 = chars[random.Next(chars.Length)];
                        modifiedLorem = modifiedLorem.Insert(SampleData.GetRandomNumber(0, modifiedLorem.Length), Convert.ToString(ch1));
                    }

                    sb.Append(modifiedLorem);
                    sb.Append(Guid.NewGuid().ToString());
                        
                }
            }

            return sb.ToString();
        }

        internal static byte[] CreateExcel()
        {
            string text = File.ReadAllText("SampleData\\sharedStrings.xml");
            text = text.Replace("SPDGTitle", SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes));
            text = text.Replace("SPDGDate", "Date: " + SampleData.GetRandomDate(1990, 2015).ToShortDateString());
            text = text.Replace("SPDGAuthor", "Author: " + SampleData.GetSampleValueRandom(SampleData.FirstNames) + " " + SampleData.GetSampleValueRandom(SampleData.LastNames));
            text = text.Replace("SPDGIdentity", "ID: " + SampleData.GetRandomNumber(100000, 1000000).ToString());

            File.WriteAllText("SampleData\\SampleExcel\\xl\\sharedStrings.xml", text);

            string path = "SampleData\\SampleExcel";
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(path + "\\[Content_Types].xml", string.Empty);
                zip.AddFile(path + "\\_rels\\.rels", "_rels");
                zip.AddFile(path + "\\docProps\\app.xml", "docProps");
                zip.AddFile(path + "\\docProps\\core.xml", "docProps");
                zip.AddFile(path + "\\xl\\_rels\\workbook.xml.rels", "xl\\_rels");
                zip.AddFile(path + "\\xl\\printerSettings\\printerSettings1.bin", "xl\\printerSettings");
                zip.AddFile(path + "\\xl\\theme\\theme1.xml", "xl\\theme");
                zip.AddFile(path + "\\xl\\worksheets\\_rels\\sheet1.xml.rels", "xl\\worksheets\\_rels");
                zip.AddFile(path + "\\xl\\worksheets\\sheet1.xml", "xl\\worksheets");
                zip.AddFile(path + "\\xl\\calcChain.xml", "xl");
                zip.AddFile(path + "\\xl\\sharedStrings.xml", "xl");
                zip.AddFile(path + "\\xl\\styles.xml", "xl");
                zip.AddFile(path + "\\xl\\workbook.xml", "xl");

                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");

                MemoryStream stream = new MemoryStream();
                zip.Save(stream);

                byte[] bytes = new byte[stream.Length];
                bytes = stream.ToArray();
                stream.Close();

                return bytes;
            }
        }

        internal static byte[] CreatePDF(int minKB, int maxKB)
        {
            PdfDocument pdfDoc = new PdfDocument();
            pdfDoc.Info.Title = "SPDG created document";
            pdfDoc.Info.Author = "SPDG";
            pdfDoc.Info.Subject = "Sample SharePoint document";

            PdfPage page = pdfDoc.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            XGraphics gfx = XGraphics.FromPdfPage(page);


            DrawText(gfx, 30, 30, page.Width, 20, XFontStyle.Bold, 20, SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes));

            DrawText(gfx, 30, 60, page.Width, 20, XFontStyle.Regular, 12, "Date: " + SampleData.GetRandomDate(1990, 2015).ToShortDateString());
            DrawText(gfx, 30, 80, page.Width, 20, XFontStyle.Regular, 12, "Author: " + SampleData.GetSampleValueRandom(SampleData.FirstNames) + " " + SampleData.GetSampleValueRandom(SampleData.LastNames));
            DrawText(gfx, 30, 100, page.Width-60, 20, XFontStyle.Regular, 12, "ID: " +  SampleData.GetRandomNumber(100000, 1000000).ToString());

            string lore = File.ReadAllText("SampleData\\loreIpsum.txt");
            DrawText(gfx, 30, 120, page.Width, page.Height - 100, XFontStyle.Regular, 12, lore);
            gfx.Dispose();

            int minRepeat = minKB / 13;
            int maxRepeat = maxKB / 13;
            int finalRepeat = 1;

            if (minRepeat > 20 && maxRepeat > 20 && minRepeat < maxKB)
            {
                finalRepeat = SampleData.GetRandomNumber(minRepeat, maxRepeat);
            }

            for (int i = 0; i < finalRepeat; i++ )
            { 
                page = pdfDoc.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Portrait;
                gfx = XGraphics.FromPdfPage(page);
                DrawText(gfx, 30, 30, page.Width-60, page.Height-100, XFontStyle.Regular, 12, lore);
                gfx.Dispose();
            }

            MemoryStream memoryStream = new MemoryStream();
            pdfDoc.Save(memoryStream);

            return memoryStream.ToArray();

        }

        private static void DrawText(XGraphics gfx, double x, double y, double width, double height, XFontStyle fontStyle, int size, string text)
        {
            DrawText(gfx, x, y, width, height, fontStyle, size, text, XBrushes.Black, XStringFormats.TopLeft);
        }

        private static void DrawText(XGraphics gfx, double x, double y, double width, double height, XFontStyle fontStyle, int size, string text, XBrush brush, XStringFormat stringformat)
        {
            XFont font = new XFont("Calibri", size, fontStyle);
            XTextFormatter tf = new XTextFormatter(gfx);
            XRect rect = new XRect(x, y, width, height);
            tf.DrawString(text, font, brush, rect, stringformat);
        }

        internal static byte[] AddRandomPngFile()
        {
            DirectoryInfo dInfo = new System.IO.DirectoryInfo("SampleData\\SamplePng");
            FileInfo[] fileInfos = dInfo.GetFiles();

            int index = SampleData.GetRandomNumber(0, fileInfos.Length-1);
            return File.ReadAllBytes(fileInfos[index].FullName);
        }
    }


}
