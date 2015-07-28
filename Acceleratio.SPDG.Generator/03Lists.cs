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
                                    getNextTemplateType();
                                    string listName = findAvailableListName(web);
                                    progressDetail("Crating List '" + web.Url +  "/" + listName + "'");
                                    web.Lists.Add(listName, string.Empty, lastTemplateType);

                                    ListInfo listInfo = new ListInfo();
                                    listInfo.Name = listName;
                                    listInfo.TemplateType = lastTemplateType;
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
            if (lastTemplateType == SPListTemplateType.NoListTemplate)
            {
                if( workingDefinition.LibTypeList)
                {
                    lastTemplateType = SPListTemplateType.GenericList;
                    lastListPrefix = "List";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeDocument)
                {
                    lastTemplateType = SPListTemplateType.DocumentLibrary;
                    lastListPrefix = "Library";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeTasks)
                {
                    lastTemplateType = SPListTemplateType.Tasks;
                    lastListPrefix = "Tasks";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeCalendar)
                {
                    lastTemplateType = SPListTemplateType.Events;
                    lastListPrefix = "Events";
                    return lastTemplateType ;
                }
            }

            if (lastTemplateType == SPListTemplateType.GenericList)
            {
                if (workingDefinition.LibTypeDocument)
                {
                    lastTemplateType = SPListTemplateType.DocumentLibrary;
                    lastListPrefix = "Library";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeTasks)
                {
                    lastTemplateType = SPListTemplateType.Tasks;
                    lastListPrefix = "Tasks";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeCalendar)
                {
                    lastTemplateType = SPListTemplateType.Events;
                    lastListPrefix = "Events";
                    return lastTemplateType;
                }
            }

            if (lastTemplateType == SPListTemplateType.DocumentLibrary)
            {
                if (workingDefinition.LibTypeTasks)
                {
                    lastTemplateType = SPListTemplateType.Tasks;
                    lastListPrefix = "Tasks";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeCalendar)
                {
                    lastTemplateType = SPListTemplateType.Events;
                    lastListPrefix = "Events";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeList)
                {
                    lastTemplateType = SPListTemplateType.GenericList;
                    lastListPrefix = "List";
                    return lastTemplateType;
                }
            }

            if (lastTemplateType == SPListTemplateType.Tasks)
            {
                if (workingDefinition.LibTypeCalendar)
                {
                    lastTemplateType = SPListTemplateType.Events;
                    lastListPrefix = "Events";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeList)
                {
                    lastTemplateType = SPListTemplateType.GenericList;
                    lastListPrefix = "List";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeDocument)
                {
                    lastTemplateType = SPListTemplateType.DocumentLibrary;
                    lastListPrefix = "Library";
                    return lastTemplateType;
                }
            }

            if (lastTemplateType == SPListTemplateType.Events)
            {
                if (workingDefinition.LibTypeList)
                {
                    lastTemplateType = SPListTemplateType.GenericList;
                    lastListPrefix = "List";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeDocument)
                {
                    lastTemplateType = SPListTemplateType.DocumentLibrary;
                    lastListPrefix = "Library";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeTasks)
                {
                    lastTemplateType = SPListTemplateType.Tasks;
                    lastListPrefix = "Tasks";
                    return lastTemplateType;
                }
            }

            return lastTemplateType;
        }

        private string findAvailableListName(SPWeb web)
        {
            string candidate = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);

            if (lastTemplateType == SPListTemplateType.Tasks)
            {
                candidate += " Tasks";
            }
            else if (lastTemplateType == SPListTemplateType.Events)
            {
                candidate += " Calendar";
            }

            while (web.Lists.TryGetList(candidate) != null)
            {
                candidate = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);
            }

            return candidate;
        }
    }
}
