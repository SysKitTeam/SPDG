using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator.Objects;
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
            if (workingDefinition.MaxNumberOfListsAndLibrariesPerSite > 0 || workingDefinition.NumberOfBigListsPerSite>0)
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
                using (var siteColl = ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using(var web = siteColl.OpenWeb(siteInfo.ID)) 
                        {
                            Random rnd = new Random();
                            int listsToCreate = rnd.Next(workingDefinition.MaxNumberOfListsAndLibrariesPerSite+1);                            
                            int bigListsToCreate = workingDefinition.NumberOfBigListsPerSite;
                            Log.Write("Creating lists in site '" + web.Url + "'");
                            listsToCreate += bigListsToCreate;
                            int bigListsCreated = 0;
                            for( int s = 0; s < listsToCreate; s++ )
                            {
                                try
                                {
                                    SPListTemplateType listTemplate;
                                    if (bigListsCreated >= bigListsToCreate)
                                    {
                                        getNextTemplateType();
                                        
                                        listTemplate = lastTemplateType;
                                    }
                                    else
                                    {
                                        lastListPrefix = "List";
                                        listTemplate = SPListTemplateType.GenericList;                                        
                                    }
                                    
                                    string listName = findAvailableListName(web);
                                    progressDetail("Creating List '" + listName + "' in site '" + web.Url + "'");
                                    
                                    Guid listGuid = web.AddList(listName, string.Empty, (int)listTemplate);

                                    var list = web.GetList(listGuid);                                    
                                    web.AddNavigationNode(list.Title, list.DefaultViewUrl, NavigationNodeLocation.QuickLaunchLists);
                                    ListInfo listInfo = new ListInfo();
                                    listInfo.Name = listName;
                                    listInfo.TemplateType = listTemplate;
                                    listInfo.isLib = (listTemplate == SPListTemplateType.DocumentLibrary ? true : false);
                                    if (!listInfo.isLib && bigListsCreated < bigListsToCreate)
                                    {
                                        listInfo.isBigList = true;
                                        bigListsCreated++;
                                    }
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

        private string findAvailableListName(SPDGWeb web)
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

            while (web.TryGetList(candidate) != null)
            {
                candidate = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);
            }

            return candidate;
        }
    }
}
