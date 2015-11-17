using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator.Objects;
using Acceleratio.SPDG.Generator.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        string _templateName = "STS#0";
        //bool templateInitiated = false;
        uint lang;

        public static string GenerateWebUrl(SPWeb parentWeb, string childWebName)
        {
            string sessionUrl =  Utilities.Path.GenerateSlug(childWebName, 256 - parentWeb.Url.Length - 38 - 1);

            //if url already exists, add guid
            if (Path.ContainsUrl(parentWeb.Webs, sessionUrl))
            {
                sessionUrl += "_" + Guid.NewGuid().ToString();
            }

            return sessionUrl;
        }

        internal void CreateSubsites(ref List<SiteInfo> sites, SPDGWeb parentWeb, int currentLevel, int maxLevels, ref int siteCounter, int maxSitesToCreate, string parentBaseName)
        {
            progressOverall("Creating Sites", siteCounter);
            Random rnd = new Random();
            string baseName = "";

            int sitesToCreate = (int)((rnd.Next(3) + 1) / ((currentLevel+1) / (float)maxLevels));

            for (int i = 0; i < sitesToCreate; i++)
            {
                if (siteCounter < maxSitesToCreate)
                {
                    var childSubsite = CreateSubsite(parentWeb, parentBaseName, currentLevel, out baseName);
                    siteCounter++;
                    if (childSubsite != null)
                    {
                        SiteInfo siteInfo = new SiteInfo();
                        siteInfo.URL = childSubsite.Url;
                        Guid siteID = childSubsite.ID;
                        siteInfo.ID = siteID;
                        sites.Add(siteInfo);
                        if (currentLevel < maxLevels)
                        {
                            CreateSubsites(ref sites, childSubsite, currentLevel + 1, maxLevels, ref siteCounter, maxSitesToCreate, baseName);
                        }
                    }

                    

                    
                }
            }
        }


        

        internal void CreateSites()
        {
            foreach(SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (var siteColl = ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    
                    //InitWebTemplate(siteColl.RootWeb);

                    //SPWeb web = CreateSubsite(siteColl.RootWeb);
                    int sitecounter = 0;

                    List<SiteInfo> sites = new List<SiteInfo>(); 
                    CreateSubsites(ref sites, siteColl.RootWeb, 0, workingDefinition.MaxNumberOfLevelsForSites, ref sitecounter, workingDefinition.NumberOfSitesToCreate, "");

                    siteCollInfo.Sites = sites;
                }
            }
        }
      
        private SPDGWeb CreateSubsite(SPDGWeb parentWeb, string parentBaseName, int level, out string baseName)
        {
            string siteName, url;
            findAvailableSiteName(parentWeb, out siteName, out url, parentBaseName, level, out baseName);

            progressDetail("Creating Site '" + parentWeb.Url + "/" + url + "'");

            SPDGWeb childWeb = null;
            try
            {              
                
                childWeb = parentWeb.AddWeb(url, siteName, null, lang, _templateName, false, false);
                AddToNavigationBar(childWeb);

                Log.Write("Site created '" + childWeb.Url + "'");
            }
            catch (Exception ex)
            {
                Log.Write("Could not create site '" + url + "'");
                Errors.Log(ex);
            }
           
            return childWeb;
        }


        //TODO:rf nepotrebno, mozemo u sucelju prikazati displayname, a pamtiti name
        //private void InitWebTemplate(SPWeb web)
        //{
        //    if (!string.IsNullOrEmpty(workingDefinition.SiteTemplate))
        //    {
        //        SPWebTemplateCollection templateCollection = web.GetAvailableWebTemplates(web.Language);
        //        foreach( SPWebTemplate template in templateCollection)
        //        {
        //            if(template.Title == workingDefinition.SiteTemplate)
        //            {
        //                _templateName = template.Name;
        //                break;
        //            }
        //        }
        //    }
        //}

        private void AddToNavigationBar(SPDGWeb childWeb)
        {
            childWeb.ParentWeb.AddNavigationNode(childWeb.Title, childWeb.ServerRelativeUrl, NavigationNodeLocation.TopNavigationBar);            
        }

        private void findAvailableSiteName(SPDGWeb web, out string siteName, out string siteUrl, string parentBaseName, int level, out string baseName)
        {
            baseName = "";
            List<string> primaryCollection;
            List<string> secondaryCollection;

            if (level%3 == 0)
            {
                primaryCollection = SampleData.Offices;
                secondaryCollection = SampleData.Dates;
            }
            else if (level%3 == 1)
            {
                primaryCollection = SampleData.Departments.GroupBy(s => s.Department).Select(grp => grp.First()).Select(d => d.Department).ToList();
                secondaryCollection = SampleData.Dates;
            }
            else 
            {
                primaryCollection = SampleData.Departments.Where(s => s.Department == parentBaseName).Select(s => s.Subdepartment).ToList();
                secondaryCollection = SampleData.Dates;
            }

            
            string candidate = SampleData.GetSampleValueRandom(primaryCollection);
            string leafName = Utilities.Path.GenerateSlug(candidate, 7);
            baseName = candidate;
            
            int i = 0;
            while (candidate==parentBaseName || web.Webs.Any(s => s.Url.Equals(web.Url + "/" + leafName)))
            {
                candidate = SampleData.GetRandomName(primaryCollection, secondaryCollection, null, ref i, out baseName);
                leafName = Utilities.Path.GenerateSlug(candidate, 7);
            }

            siteName = candidate;
            siteUrl = leafName;
        }
    }
}
