using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            Random random = new Random();
            int randomNumber = random.Next(0, sampleCollection.Count()-1);

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
    }

}
