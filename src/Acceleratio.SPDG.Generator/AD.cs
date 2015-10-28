using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator
{
    public class AD
    {
        public static List<string> GetAvailableDomains()
        {
            List<string> domains = new List<string>();

            using (var forest = Forest.GetCurrentForest())
            {
                foreach (Domain domain in forest.Domains)
                {
                    domains.Add(domain.Name);
                    domain.Dispose();
                }
            }

            return domains;
        }

        public static List<string> GetDomainList()
        {
            List<string> domainList = new List<string>();

            DirectoryEntry en = new DirectoryEntry("LDAP://");

            // Search for objectCategory type "Domain"
            DirectorySearcher srch = new DirectorySearcher("objectCategory=Domain");
            SearchResultCollection coll = srch.FindAll();

            // Enumerate over each returned domain.
            foreach (SearchResult rs in coll)
            {
                ResultPropertyCollection resultPropColl = rs.Properties;
                foreach (object domainName in resultPropColl["name"])
                {
                    domainList.Add(domainName.ToString());
                }
            }

            return domainList;
        }

        public static List<string> GetDomainList2()
        {

            List<string> domainList = new List<string>();
            string sRootDomain;
            System.DirectoryServices.DirectoryEntry deRootDSE;
            System.DirectoryServices.DirectoryEntry deSearchRoot;
            System.DirectoryServices.DirectorySearcher dsFindDomains;
            System.DirectoryServices.SearchResultCollection srcResults;

            deRootDSE = new System.DirectoryServices.DirectoryEntry("GC://RootDSE");
            sRootDomain = "GC://" + deRootDSE.Properties["rootDomainNamingContext"].Value.ToString();

            deSearchRoot = new System.DirectoryServices.DirectoryEntry(sRootDomain);
            dsFindDomains = new System.DirectoryServices.DirectorySearcher(deSearchRoot);
            dsFindDomains.Filter = "(objectCategory=domainDNS)";
            dsFindDomains.SearchScope = System.DirectoryServices.SearchScope.Subtree;

            srcResults = dsFindDomains.FindAll();
            foreach (System.DirectoryServices.SearchResult srDomain in srcResults)
            {
                //System.Console.WriteLine(srDomain.Properties["name"][0].ToString()
                //    + " - "
                //    + srDomain.Properties["distinguishedName"][0].ToString());

                domainList.Add(srDomain.Properties["name"][0].ToString());
            }

            return domainList;
        }

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

        public static List<string> ListOU(string name)
        {
            List<string> ous = new List<string>();
            using (DirectoryEntry root = new DirectoryEntry("LDAP://" + name))
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

        public static void createUsers(string domain, string ou, int numOfUsers)
        {
            ContextType contextType = ContextType.Domain;

            using (PrincipalContext ctx = new PrincipalContext(contextType, domain, ou))
            {
                for(int i=0; i<numOfUsers; i++)
                {
                    try
                    {
                        UserPrincipal userPrincipal = new UserPrincipal(ctx);
                        userPrincipal.Surname = SampleData.GetSampleValueRandom(SampleData.LastNames);
                        userPrincipal.GivenName = SampleData.GetSampleValueRandom(SampleData.FirstNames); ;
                        userPrincipal.SamAccountName = userPrincipal.GivenName.ToLower() + "." + userPrincipal.Surname.ToLower();
                        userPrincipal.Name = userPrincipal.GivenName + " " + userPrincipal.Surname;
                        userPrincipal.DisplayName = userPrincipal.GivenName + " " + userPrincipal.Surname;
                 
                        string pwdOfNewlyCreatedUser = "Acce1234!";

                        userPrincipal.SetPassword(pwdOfNewlyCreatedUser);
                        userPrincipal.Enabled = true;
                        userPrincipal.PasswordNeverExpires = true;
                        userPrincipal.Save();
                    }
                    catch (Exception ex)
                    {
                        Errors.Log(ex);
                    }
                }
            }
        }

        public static void createGroups(string domain, string ou, int numOfGroups)
        {
            ContextType contextType = ContextType.Domain;

            using (PrincipalContext ctx = new PrincipalContext(contextType, domain, ou))
            {
                for (int i = 0; i < numOfGroups; i++)
                {
                    try
                    {
                        GroupPrincipal groupPrincipal = new GroupPrincipal(ctx);
                        groupPrincipal.Name = SampleData.GetSampleValueRandom(SampleData.Accounts);
                        groupPrincipal.DisplayName = groupPrincipal.Name;

                        groupPrincipal.Save();
                    }
                    catch (Exception ex)
                    {
                        Errors.Log(ex);
                    }
                }
            }
        }
    }
}
