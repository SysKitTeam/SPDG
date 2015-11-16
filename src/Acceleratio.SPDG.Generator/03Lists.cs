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
        SPListTemplateType lastTemplateType = SPListTemplateType.NoListTemplate;
        string lastListPrefix = "List";

        public void CreateLists()
        {
            if (workingDefinition.MaxNumberOfListsAndLibrariesPerSite > 0 )
            {
                int progressTotal = CalculateTotalListsForProgressReporting();
                progressOverall("Creating Lists and Libraries", progressTotal);
            }
            else
            {
                return;
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
                            Random rnd = new Random();

                            int listsToCreate = Math.Max(rnd.Next(workingDefinition.MaxNumberOfListsAndLibrariesPerSite) + 1, workingDefinition.MaxNumberOfListsAndLibrariesPerSite);

                            for( int s = 0; s < listsToCreate; s++ )
                            {
                                try
                                {
                                    getNextTemplateType();
                                    string listName = findAvailableListName(web);
                                    progressDetail("Creating List '" + listName + "' in site '" + web.Url + "'");
                                    Guid listGuid = web.Lists.Add(listName, string.Empty, lastTemplateType);

                                    SPList list = web.Lists.GetList(listGuid, false);
                                    // Check for an existing link to the list.
                                    SPNavigationNode listNode = web.Navigation.GetNodeByUrl(list.DefaultViewUrl);

                                    // No link, so create one.
                                    if (listNode == null)
                                    {
                                        // Create the node.
                                        listNode = new SPNavigationNode(list.Title, list.DefaultViewUrl);

                                        // Add it to Quick Launch.
                                        listNode = web.Navigation.AddToQuickLaunch(listNode, SPQuickLaunchHeading.Lists);
                                    }

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
