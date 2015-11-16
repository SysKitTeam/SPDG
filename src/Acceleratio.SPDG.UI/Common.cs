using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator;
using System.Xml.Serialization;
using System.IO;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace Acceleratio.SPDG.UI
{
    
    public class Common
    {
        public const string APP_TITLE = "SharePoint Data Generator";
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        public static GeneratorDefinitionBase WorkingDefinition { get; set; }
        public static string impersonateUserName { get; set; }
        public static string impersonateDomain { get; set; }
        public static string impersonatePassword { get; set; }

        internal static WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        public static bool PreventAppClosing { get; set; }
        

        public static void SerializeDefinition(string path)
        {
            //GeneratorDefinition definition = new GeneratorDefinition();
            //definition.SharePointURL = "http://sp-kreso";
            //definition.CredentialsOfCurrentUser = false;
            //definition.Username = "kresimir.korovljevic";
            //definition.Password = "XXXX";
            //definition.Domain = "acceleratio";

            XmlSerializer serializer = new XmlSerializer(typeof(ServerGeneratorDefinition), new Type[] { typeof(ClientGeneratorDefinition), typeof(ServerGeneratorDefinition)});
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, WorkingDefinition);
            } 
        }

        public static void DeserializeDefinition(string path)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ServerGeneratorDefinition));
            TextReader reader = new StreamReader(path);
            object obj = deserializer.Deserialize(reader);
            WorkingDefinition = (ServerGeneratorDefinition)obj;
            reader.Close();
        }


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


        private static void SetCommonDefaults(GeneratorDefinitionBase definition)
        {
           // WorkingDefinition = new ServerGeneratorDefinition();

            //WorkingDefinition.ConnectToSPOnPremise = true;
          //  WorkingDefinition.CredentialsOfCurrentUser = true;

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
           // WorkingDefinition.CreateNewWebApplications = 0;
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

        internal static bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        internal static void undoImpersonation()
        {
            impersonationContext.Undo();
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
