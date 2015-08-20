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

        public static GeneratorDefinition WorkingDefinition { get; set; }
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
            WorkingDefinition.MaxNumberOfLevelsForSites = 0;
            WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite = 0;
            WorkingDefinition.MaxNumberOfNestedFolderLevelPerLibrary = 0;
            WorkingDefinition.MaxNumberOfViewsPerList = 0;
            WorkingDefinition.CreateNewWebApplications = 0;
            WorkingDefinition.PermissionsPerObject = 1;
            

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
