using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        SPWebTemplate choosenTemplate = null;
        bool templateInitiated = false;
        uint lang;

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

                        progressDetail("Creating Site '" + siteCollInfo.URL + "/" + siteName + "'");

                        if(!templateInitiated)
                        {
                            //initweb = siteColl.AllWebs.Add(siteName);
                            lang = siteColl.RootWeb.Language;
                            InitWebTemplate(siteColl.RootWeb);
                        }

                        SPWeb web = null;
                        if(choosenTemplate == null )
                        {
                            web = siteColl.AllWebs.Add(siteName);
                        }
                        else
                        {
                            try
                            {
                                web = siteColl.AllWebs.Add(Common.GenerateSlug(siteName, 40), siteName, null, lang, choosenTemplate, false, false);
                            }
                            catch(Exception ex)
                            {
                                Log.Write("ERROR while trying to add site template '" + workingDefinition.SiteTemplate + "'");
                                Errors.Log(ex);
                                Log.Write("Reverting to 'Team Site' template");
                                choosenTemplate = null;
                                web = siteColl.AllWebs.Add(siteName);
                            }
                            
                        }


                        web.Title = siteName;
                        web.Update();
                        addQuickLaunch(web);

                        SiteInfo siteInfo = new SiteInfo();
                        siteInfo.URL = web.Url;
                        //siteInfo.URL = web.GetServerRelativeUrlFromUrl(web.Url);
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
                            addQuickLaunch(web);

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

        private void InitWebTemplate(SPWeb web)
        {
            if (!string.IsNullOrEmpty(workingDefinition.SiteTemplate) && !templateInitiated)
            {
                if (choosenTemplate == null)
                {
                    SPWebTemplateCollection templateCollection = web.GetAvailableWebTemplates(web.Language);
                    foreach( SPWebTemplate template in templateCollection)
                    {
                        if( template.Title == workingDefinition.SiteTemplate)
                        {
                            choosenTemplate = template;
                            break;
                        }
                    }
                }
            }

            templateInitiated = true;
        }

        private void addQuickLaunch(SPWeb childWeb)
        {
            SPNavigationNodeCollection topnav = childWeb.ParentWeb.Navigation.TopNavigationBar;
            SPNavigationNode node = new SPNavigationNode(childWeb.Title, childWeb.ServerRelativeUrl);
            node = topnav.AddAsLast(node);
        }

        private string findAvailableSiteName(SPSite siteColl)
        {
            string candidate = SampleData.Clean(SampleData.GetSampleValueRandom(SampleData.Accounts));

            while (siteColl.AllWebs.Any(s => s.Name.Equals(candidate)))
            {
                candidate = SampleData.Clean(SampleData.GetSampleValueRandom(SampleData.Accounts));
            }

            return candidate;
        }
    }
}
