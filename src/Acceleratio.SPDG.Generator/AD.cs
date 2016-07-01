using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
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
                domainList.Add(srDomain.Properties["name"][0].ToString());
            }

            return domainList;
        }

        public static List<string> GetUsersFromAD()
        {
            List<string> retVal=new List<string>();
            try
            {                
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
                        retVal.Add(domainName + "\\" + userPrincipal.SamAccountName);                        
                    }                    
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }
            return retVal;
        }

        public static List<string> GetGroupsFromAD()
        {
            List<string> retVal =new List<string>();
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
                        retVal.Add(domainName + "\\" + groupPrincipal.Name + ";");
                    }                   
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }

            return retVal;
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
    }
}
