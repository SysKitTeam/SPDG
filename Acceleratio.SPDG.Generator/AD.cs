using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator
{
    public class AD
    {
        public static string GetUsersFromAD()
        {
            string output = string.Empty;

            try
            {
                //domainName = Server.UrlDecode(domainName);
                ContextType contextType = ContextType.Domain;
                string strName = System.Security.Principal.WindowsIdentity.GetCurrent().Name; 
                string domainName = strName.Split('\\')[0];

                using (PrincipalContext ctx = new PrincipalContext(contextType, domainName))
                {
                    var searchPrinciple = new UserPrincipal(ctx);
                    searchPrinciple.EmailAddress = "*";
                    searchPrinciple.Enabled = true;

                    System.DirectoryServices.AccountManagement.PrincipalSearcher search = new PrincipalSearcher();
                    search.QueryFilter = searchPrinciple;

                    PrincipalSearchResult<Principal> results = search.FindAll();

                    StringBuilder sb = new StringBuilder();

                    foreach (UserPrincipal userPrincipal in results)
                    {
                        sb.Append(domainName + "\\" + userPrincipal.SamAccountName + ";");
                    }

                    output = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }

            return output;
        }


        public static string GetGroupsFromAD()
        {
            string output = string.Empty;

            try
            {
                ContextType contextType = ContextType.Domain;
                string strName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string domainName = strName.Split('\\')[0];
      
                using (PrincipalContext ctx = new PrincipalContext(contextType, domainName))
                {
                    
                    var searchPrinciple = new GroupPrincipal(ctx);
                    searchPrinciple.IsSecurityGroup = true;

                    System.DirectoryServices.AccountManagement.PrincipalSearcher search = new PrincipalSearcher();
                    search.QueryFilter = searchPrinciple;

                    PrincipalSearchResult<Principal> results = search.FindAll();

                    StringBuilder sb = new StringBuilder();

                    foreach (GroupPrincipal groupPrincipal in results)
                    {
                        sb.Append(domainName + "\\" + groupPrincipal.Name + ";");
                    }

                    output = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }

            return output;
        }

        public static List<string> ListOU()
        {
            List<string> ous = new List<string>();
            using (DirectoryEntry root = new DirectoryEntry("LDAP://acceleratio.hr"))
            {
                DirectorySearcher searcher = new DirectorySearcher(root);
                searcher.Filter = "(&(objectClass=organizationalUnit))";
                searcher.SearchScope = SearchScope.Subtree;
                searcher.PropertiesToLoad.Add("distinguishedName");

                var result = searcher.FindAll();
                foreach (SearchResult entry in result)
                {
                    ous.Add(entry.GetDirectoryEntry().Properties["distinguishedName"].Value.ToString());
                }

                result.Dispose();
                searcher.Dispose();
            }
            return ous;
        }

        public static void createUser()
        {
            try
            {
                ContextType contextType = ContextType.Domain;
                string strName = System.Security.Principal.WindowsIdentity.GetCurrent().Name; 
                string domainName = strName.Split('\\')[0];

                using (PrincipalContext ctx = new PrincipalContext(contextType, "test"))
                {
                    UserPrincipal userPrincipal = new UserPrincipal(ctx);
                    userPrincipal.Surname = SampleData.GetSampleValueRandom(SampleData.LastNames);
                    userPrincipal.GivenName = SampleData.GetSampleValueRandom(SampleData.FirstNames); ;
                    userPrincipal.SamAccountName = userPrincipal.GivenName.ToLower() + "." + userPrincipal.Surname.ToLower();

                    string pwdOfNewlyCreatedUser = "Acce1234!";

                    userPrincipal.SetPassword(pwdOfNewlyCreatedUser);
                    userPrincipal.Enabled = true;
                    userPrincipal.PasswordNeverExpires = true;
                    userPrincipal.Save();
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }

        }
    }
}
