using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator;
using System.Xml.Serialization;
using System.IO;

namespace Acceleratio.SPDG.UI
{
    
    public class Common
    {
        public const string APP_TITLE = "SharePoint Data Generator";

        public static GeneratorDefinition WorkingDefinition {get; set;}

        public static void SerializeDefinition(string path)
        {
            //GeneratorDefinition definition = new GeneratorDefinition();
            //definition.SharePointURL = "http://sp-kreso";
            //definition.CredentialsOfCurrentUser = false;
            //definition.Username = "kresimir.korovljevic";
            //definition.Password = "XXXX";
            //definition.Domain = "acceleratio";

            XmlSerializer serializer = new XmlSerializer(typeof(GeneratorDefinition));
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, WorkingDefinition);
            } 
        }

        public static void DeserializeDefinition(string path)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(GeneratorDefinition));
            TextReader reader = new StreamReader(path);
            object obj = deserializer.Deserialize(reader);
            WorkingDefinition = (GeneratorDefinition)obj;
            reader.Close();
        }

        public static void InitEmptyDefinition()
        {
            WorkingDefinition = new GeneratorDefinition();

            WorkingDefinition.ConnectToSPOnPremise = true;
            WorkingDefinition.CredentialsOfCurrentUser = true;

            WorkingDefinition.NumberOfSecurityGroupsToCreate = 0;
            WorkingDefinition.NumberOfUsersToCreate = 0;
            WorkingDefinition.NumberOfSitesToCreate = 1;
            WorkingDefinition.MinDocumentSizeKB = 0;
            WorkingDefinition.MaxDocumentSizeMB = 0;
            WorkingDefinition.MaxNumberOfColumnsPerList = 0;
            WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection = 0;
            WorkingDefinition.MaxNumberOfFoldersToGenerate = 0;
            WorkingDefinition.MaxNumberofItemsToGenerate = 0;
            WorkingDefinition.MaxNumberOfLevelsForSites = 1;
            WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite = 1;
            WorkingDefinition.MaxNumberOfNestedFolderLevelPerLibrary = 1;
            WorkingDefinition.MaxNumberOfViewsPerList = 0;
            WorkingDefinition.CreateNewWebApplications = 0;
            WorkingDefinition.OwnerLogin = "acceleratio\\kresimir.korovljevic";
            WorkingDefinition.OwnerPassword = "12Kres.1";
            WorkingDefinition.OwnerEmail = "kresimir.korovljevic@acceleratio.net";
            WorkingDefinition.DatabaseServer = "SQL-KRESO";

        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
