using System;
using System.IO;
using System.Xml.Serialization;
using Acceleratio.SPDG.Generator;

namespace Acceleratio.SPDG.UI
{
    
    public class Common
    {
        public const string APP_TITLE = "SharePoint Data Generator";

        public static bool PreventAppClosing { get; set; }

        public static GeneratorDefinitionBase WorkingDefinition { get; set; }


        public static void InitClientDefinition()
        {
            var clientDefinition = new ClientGeneratorDefinition();
            SetCommonDefaults(clientDefinition);
            WorkingDefinition = clientDefinition;
        }

        public static void InitServerDefinition()
        {
            var serverDefinition = new ServerGeneratorDefinition();
            SetCommonDefaults(serverDefinition);

            serverDefinition.CredentialsOfCurrentUser = true;
            serverDefinition.CreateNewWebApplications = 0;

            WorkingDefinition = serverDefinition;
        }

        public class SerializeWrapper
        {
            public GeneratorDefinitionBase Definition { get; set; }
        }
        public static void SerializeDefinition(string path)
        {            
            XmlSerializer serializer = new XmlSerializer(typeof(SerializeWrapper), new Type[] { typeof(ClientGeneratorDefinition), typeof(ServerGeneratorDefinition) });
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, new SerializeWrapper() { Definition = WorkingDefinition });
            }            
        }

        public static void DeserializeDefinition(string path)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(SerializeWrapper), new Type[] { typeof(ClientGeneratorDefinition), typeof(ServerGeneratorDefinition) });
            TextReader reader = new StreamReader(path);
            object obj = deserializer.Deserialize(reader);
            WorkingDefinition = ((SerializeWrapper)obj).Definition;
            reader.Close();            
        }


        


        private static void SetCommonDefaults(GeneratorDefinitionBase definition)
        {          
            definition.NumberOfSecurityGroupsToCreate = 0;
            definition.NumberOfUsersToCreate = 0;
            definition.NumberOfSitesToCreate = 50;
            definition.MaxNumberOfColumnsPerList = 0;
            definition.MaxNumberOfContentTypesPerSiteCollection = 0;
            definition.MaxNumberOfFoldersToGenerate = 0;
            definition.MaxNumberofItemsToGenerate = 0;
            definition.MaxNumberOfLevelsForSites = 3;
            definition.MaxNumberOfListsAndLibrariesPerSite = 3;
            definition.MaxNumberOfViewsPerList = 0;         
            definition.CreateNewSiteCollections = 1;
            definition.SiteTemplate = "Team Site";
            definition.LibTypeList = true;
            definition.LibTypeDocument = true;
            definition.LibTypeCalendar = true;
            definition.LibTypeTasks = true;
            definition.CreateSomeFoldersInDocumentLibraries = true;
            definition.MaxNumberOfFoldersToGenerate = 10;
            definition.MaxNumberOfNestedFolderLevelPerLibrary = 3;
            definition.CreateColumns = true;
            definition.MaxNumberOfColumnsPerList = 3;
            definition.PrefilListAndLibrariesWithItems = true;
            definition.MaxNumberofItemsToGenerate = 30;
            definition.IncludeDocTypeDOCX = true;
            definition.IncludeDocTypePDF = true;
            definition.IncludeDocTypeImages = true;
            definition.IncludeDocTypeXLSX = true;
            definition.MinDocumentSizeKB = 100;
            definition.MaxDocumentSizeMB = 1;
            definition.ContentTypesCanInheritFromOtherContentType = true;
            definition.CreateContentTypes = true;
            definition.MaxNumberOfContentTypesPerSiteCollection = 10;
            definition.PermissionsPercentOfSites = 30;
            definition.PermissionsPercentOfLists = 30;
            definition.PermissionsPerObject = 10;
            definition.PermissionsPercentForUsers = 20;
            definition.PermissionsPercentForSPGroups = 40;
            definition.PermissionsPercentOfListItems = 5;


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
