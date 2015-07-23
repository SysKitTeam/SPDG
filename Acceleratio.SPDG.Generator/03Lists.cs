using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        SPListTemplateType lastTemplateType = SPListTemplateType.NoListTemplate;
        string lastListPrefix = "List";

        public void CreateLists()
        {
            if (workingDefinition.MaxNumberOfListsAndLibrariesPerSite > 0 )
            {
                int progressTotal = workingDefinition.MaxNumberOfListsAndLibrariesPerSite * workingDefinition.NumberOfSitesToCreate;
                if (workingDefinition.CreateNewSiteCollections > 0)
                {
                    progressTotal = progressTotal * workingDefinition.CreateNewSiteCollections;
                }
                if (workingDefinition.CreateNewWebApplications > 0)
                {
                    progressTotal = progressTotal * workingDefinition.CreateNewWebApplications;
                }

                progressOverall("Creating Lists and Libraries", progressTotal);
            }

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using( SPWeb web = siteColl.OpenWeb(siteInfo.ID)) 
                        {
                            Log.Write("Creating lists in site '" + web.Url + "'");
                            for( int s = 0; s < workingDefinition.MaxNumberOfListsAndLibrariesPerSite; s++ )
                            {
                                try
                                {
                                    string listName = findAvailableListName(web);
                                    progressDetail("Crating List '" + web.Url +  "/" + listName + "'");
                                    getNextTemplateType();
                                    web.Lists.Add(listName, string.Empty, lastTemplateType);

                                    ListInfo listInfo = new ListInfo();
                                    listInfo.Name = listName;
                                    listInfo.isLib = (lastTemplateType == SPListTemplateType.DocumentLibrary ? true : false);

                                    siteInfo.Lists.Add(listInfo);

                                    
                                }
                                catch(Exception ex )
                                {
                                    Errors.Log(ex);
                                }
                                
                            }
                        }
                    }
                }
            }


        }

        private SPListTemplateType getNextTemplateType()
        {
            bool changed = false;


            if (!changed && workingDefinition.LibTypeList && lastTemplateType != SPListTemplateType.GenericList)
            {
                lastTemplateType = SPListTemplateType.GenericList;
                lastListPrefix = "List";
                changed = true;
            }

            if( !changed && workingDefinition.LibTypeDocument && lastTemplateType != SPListTemplateType.DocumentLibrary)
            {
                lastTemplateType = SPListTemplateType.DocumentLibrary;
                lastListPrefix = "Library";
                changed = true;
            }

            if (!changed && workingDefinition.LibTypeTasks && lastTemplateType != SPListTemplateType.Tasks)
            {
                lastTemplateType = SPListTemplateType.DocumentLibrary;
                lastListPrefix = "Tasks";
                changed = true;
            }

            return lastTemplateType;
        }

        private string findAvailableListName(SPWeb web)
        {
            string candidate = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);

            while (web.Lists.TryGetList(candidate) != null)
            {
                candidate = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);
            }

            return candidate;
        }
    }
}
