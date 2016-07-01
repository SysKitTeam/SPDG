using System;
using System.Collections.Generic;
using System.Linq;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator.GenerationTasks
{
    public class SitesDataGenerationTask : DataGenerationTaskBase
    {
        string _templateName = "STS#0";

        public override string Title
        {
            get { return "Creating Sites"; }
        }

        public SitesDataGenerationTask(IDataGenerationTaskOwner owner) : base(owner)
        {

        }


        public override int CalculateTotalSteps()
        {
            int totalSteps = WorkingDefinition.NumberOfSitesToCreate;
            totalSteps = totalSteps * Owner.WorkingSiteCollections.Count;
            return totalSteps;
        }        

        public override bool IsActive
        {
            get { return true; }
        }

        internal void CreateSubsites(ref List<SiteInfo> sites, SPDGWeb parentWeb, int currentLevel, int maxLevels, ref int siteCounter, int maxSitesToCreate, string parentBaseName)
        {            
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
        
        public override void Execute()
        {
            
            foreach (SiteCollInfo siteCollInfo in Owner.WorkingSiteCollections)
            {
                using (var siteColl = Owner.ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    
                    InitWebTemplate(siteColl.RootWeb);

                    
                    int sitecounter = 0;

                    List<SiteInfo> sites = new List<SiteInfo>(); 
                    CreateSubsites(ref sites, siteColl.RootWeb, 0, WorkingDefinition.MaxNumberOfLevelsForSites, ref sitecounter, WorkingDefinition.NumberOfSitesToCreate, "");
                   
                    siteCollInfo.Sites = sites;
                }
            }
        }
      
        private SPDGWeb CreateSubsite(SPDGWeb parentWeb, string parentBaseName, int level, out string baseName)
        {
            string siteName, url;
            findAvailableSiteName(parentWeb, out siteName, out url, parentBaseName, level, out baseName);            
            Owner.IncrementCurrentTaskProgress("Creating Site '" + parentWeb.Url + "/" + url + "'",0);

            SPDGWeb childWeb = null;
            try
            {              
                
                childWeb = parentWeb.AddWeb(url, siteName, null, parentWeb.Language, _templateName, false, false);
                AddToNavigationBar(childWeb);

                Owner.IncrementCurrentTaskProgress("Site created '" + childWeb.Url + "'");
            }
            catch (Exception ex)
            {
                Log.Write("Could not create site '" + url + "'");
                Owner.IncrementCurrentTaskProgress("");
                Errors.Log(ex);
            }
           
            return childWeb;
        }
        
        private void InitWebTemplate(SPDGWeb web)
        {
            if (!string.IsNullOrEmpty(WorkingDefinition.SiteTemplate))
            {                 
                foreach (var template in web.GetWebTemplates(web.Language))
                {
                    if (template.Title == WorkingDefinition.SiteTemplate)
                    {
                        _templateName = template.Name;
                        break;
                    }
                }
            }
        }

        private void AddToNavigationBar(SPDGWeb childWeb)
        {
            childWeb.ParentWeb.AddNavigationNode(childWeb.Title, childWeb.ServerRelativeUrl, NavigationNodeLocation.TopNavigationBar);            
        }

        private void findAvailableSiteName(SPDGWeb web, out string siteName, out string siteUrl, string parentBaseName, int level, out string baseName)
        {
            baseName = "";
            IList<string> primaryCollection;
            IList<string> secondaryCollection;

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
            string leafName = Utils.GenerateSlug(candidate, 7);
            baseName = candidate;
            
            int i = 0;
            while (candidate==parentBaseName || web.Webs.Any(s => s.Url.Equals(web.Url + "/" + leafName)))
            {
                candidate = SampleData.GetRandomName(primaryCollection, secondaryCollection, null, ref i, out baseName);
                if (i < 3)
                {
                    leafName = Utils.GenerateSlug(candidate, 7);
                }
                else if(i<5)
                {
                    leafName = Utils.GenerateSlug(candidate, 15);
                }
                else
                {
                    leafName = Utils.GenerateSlug(candidate, 100);
                }
            }

            siteName = candidate;
            siteUrl = leafName;
        }
    }
}
