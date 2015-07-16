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

                        string siteNumber = (counter + exsistingSitesCount).ToString("00");
                        SPWeb web = siteColl.AllWebs.Add("Site" + siteNumber);
                        web.Title = "Site" + siteNumber;
                        web.Update();

                        SiteInfo siteInfo = new SiteInfo();
                        siteInfo.URL = "Site" + siteNumber;
                        siteCollInfo.Sites.Add(siteInfo);

                        for (int l = 1; l < workingDefinition.MaxNumberOfLevelsForSites; l++)
                        {
                            counter++;
                            if (counter >= workingDefinition.NumberOfSitesToCreate)
                            {
                                break;
                            }

                            web = web.Webs.Add("Site" + siteNumber);
                            web.Title = "Site" + siteNumber;
                            web.Update();

                            SiteInfo siteInfoLevel = new SiteInfo();
                            siteInfoLevel.URL = "Site" + siteNumber;
                            siteCollInfo.Sites.Add(siteInfoLevel);
                        }
                    }
                }
            }
        }
    }
}
