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
            get { return !WorkingDefinition.UseOnlyExistingSites; }
        }

        private int SiteCounter { get; set; }
        private List<SiteInfo> Sites { get; set; }
       
        internal void CreateSubsites(SPDGWeb parentWeb, int currentLevel, int maxLevels, int maxSitesToCreate, string parentBaseName)
        {            
            Random rnd = new Random();
            string baseName = "";

            int value = rnd.Next(7) + 1;

            int sitesToCreate = (int)(value / ((currentLevel+1) / (float)maxLevels));

            Log.Write($"{parentWeb.Title} (int)({value}) / (({currentLevel + 1}) / (float){maxLevels})), sitesToCreate: {sitesToCreate}");

            for (int i = 0; i < sitesToCreate; i++)
            {
                if (SiteCounter < maxSitesToCreate)
                {
                    var childSubsite = CreateSubsite(parentWeb, parentBaseName, currentLevel, out baseName);
                    SiteCounter++;
                    if (childSubsite != null)
                    {
                        SiteInfo siteInfo = new SiteInfo();
                        siteInfo.URL = childSubsite.Url;
                        Guid siteID = childSubsite.ID;
                        siteInfo.ID = siteID;
                        Sites.Add(siteInfo);
                        if (currentLevel < maxLevels)
                        {
                            CreateSubsites(childSubsite, currentLevel + 1, maxLevels, maxSitesToCreate, baseName);
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
                    
                    this.Sites = new List<SiteInfo>(); 
                    CreateSubsites(siteColl.RootWeb, 0, WorkingDefinition.MaxNumberOfLevelsForSites, WorkingDefinition.NumberOfSitesToCreate, "");
                   
                    siteCollInfo.Sites.AddRange(Sites);
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
                primaryCollection = SampleData.Departments.GroupBy(s => s.Department).Select(grp => grp.First()).Select(d => d.Department).ToList();
                secondaryCollection = SampleData.Dates;
            }
            else if (level % 3 == 1)
            {
                primaryCollection = SampleData.Departments.Where(s => s.Department == parentBaseName).Select(s => s.Subdepartment).ToList();
                secondaryCollection = SampleData.Dates;
            }
            else
            {
                primaryCollection = SampleData.Offices;
                secondaryCollection = SampleData.Dates;
            }

            string candidate = SampleData.GetSampleValueRandom(primaryCollection);
            string leafName = Utils.GenerateSlug(candidate, 7);
            baseName = candidate;
            
            int i = 0;
            while (candidate==parentBaseName || web.Webs.Any(s => s.Url.Equals(web.Url + "/" + leafName) || s.Title == candidate))
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
