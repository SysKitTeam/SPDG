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

            foreach( SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    int exsistingSitesCount = siteColl.AllWebs.Count();
                    for (int counter = 1; counter <= workingDefinition.NumberOfSitesToCreate; counter++)
                    {
                        string siteName = findAvailableSiteName(siteColl);
                        SPWeb web = siteColl.AllWebs.Add(siteName);
                        web.Title = siteName;
                        web.Update();

                        SiteInfo siteInfo = new SiteInfo();
                        siteInfo.URL = siteName;
                        siteCollInfo.Sites.Add(siteInfo);

                        Log.Write("Site created: " + web.Url);

                        for (int l = 1; l < workingDefinition.MaxNumberOfLevelsForSites; l++)
                        {
                            counter++;
                            siteName = findAvailableSiteName(siteColl);

                            if (counter >= workingDefinition.NumberOfSitesToCreate)
                            {
                                break;
                            }

                            web = web.Webs.Add( siteName);
                            web.Title = siteName;
                            web.Update();

                            SiteInfo siteInfoLevel = new SiteInfo();
                            siteInfoLevel.URL = siteName;
                            siteCollInfo.Sites.Add(siteInfoLevel);

                            Log.Write("Site created: " + web.Url);
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
