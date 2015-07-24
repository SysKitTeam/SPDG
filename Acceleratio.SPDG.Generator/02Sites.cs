using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        internal void CreateSites()
        {
            int totalProgress = workingDefinition.NumberOfSitesToCreate;
            if( workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * (workingDefinition.CreateNewSiteCollections );
            }
            if (workingDefinition.CreateNewWebApplications > 0)
            {
                totalProgress = totalProgress * (workingDefinition.CreateNewWebApplications );
            }

            progressOverall("Creating Sites", totalProgress);

            foreach( SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    int exsistingSitesCount = siteColl.AllWebs.Count();
                    for (int counter = 1; counter <= workingDefinition.NumberOfSitesToCreate; counter++)
                    {
                        string siteName = findAvailableSiteName(siteColl);

                        progressDetail("Creating Site '" + siteCollInfo.URL + "//" + siteName + "'");

                        SPWeb web = siteColl.AllWebs.Add(siteName);
                        web.Title = siteName;
                        web.Update();

                        SiteInfo siteInfo = new SiteInfo();
                        siteInfo.URL = web.GetServerRelativeUrlFromUrl(web.Url);
                        Guid siteID = web.ID;
                        siteInfo.ID = siteID;
                        siteCollInfo.Sites.Add(siteInfo);

                        Log.Write("Site created '" + web.Url + "'");
                        

                        for (int l = 0; l < workingDefinition.MaxNumberOfLevelsForSites; l++)
                        {
                            counter++;
                            siteName = findAvailableSiteName(siteColl);
                            progressDetail("Creating Site '" + web.Url + "/" + siteName + "'");

                            if (counter >= workingDefinition.NumberOfSitesToCreate)
                            {
                                break;
                            }

                            web = web.Webs.Add( siteName);
                            web.Title = siteName;
                            web.Update();

                            SiteInfo siteInfoLevel = new SiteInfo();
                            siteInfoLevel.URL = web.Url;
                            Guid site2ID = web.ID;
                            siteInfoLevel.ID = site2ID;
                            siteCollInfo.Sites.Add(siteInfoLevel);

                            Log.Write("Site created '" + web.Url + "'");
                        }
                    }
                }
            }
        }

        private string findAvailableSiteName(SPSite siteColl)
        {
            string candidate = SampleData.Clean(SampleData.GetSampleValueRandom(SampleData.Years));

            while (siteColl.AllWebs.Any(s => s.Name.Equals(candidate)))
            {
                candidate = SampleData.Clean(SampleData.GetSampleValueRandom(SampleData.Years));
            }

            return candidate;
        }
    }
}
