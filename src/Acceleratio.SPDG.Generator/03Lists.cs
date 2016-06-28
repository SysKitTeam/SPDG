using System;
using Acceleratio.SPDG.Generator.Model;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        SPDGListTemplateType lastTemplateType = SPDGListTemplateType.NoListTemplate;
        string lastListPrefix = "List";

        public void CreateLists()
        {
            if (workingDefinition.MaxNumberOfListsAndLibrariesPerSite > 0 || workingDefinition.NumberOfBigListsPerSite>0)
            {
                int progressTotal = CalculateTotalListsForProgressReporting();
                updateProgressOverall("Creating Lists and Libraries", progressTotal);
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
                                    SPDGListTemplateType listTemplate;
                                    if (bigListsCreated >= bigListsToCreate)
                                    {
                                        getNextTemplateType();
                                        
                                        listTemplate = lastTemplateType;
                                    }
                                    else
                                    {
                                        lastListPrefix = "List";
                                        listTemplate = SPDGListTemplateType.GenericList;                                        
                                    }
                                    
                                    string listName = findAvailableListName(web);
                                    Guid listGuid = web.AddList(listName, string.Empty, (int)listTemplate);
                                    updateProgressDetail("Created List '" + listName + "' in site '" + web.Url + "'");
                                    var list = web.GetList(listGuid);                                    
                                    web.AddNavigationNode(list.Title, list.DefaultViewUrl, NavigationNodeLocation.QuickLaunchLists);
                                    ListInfo listInfo = new ListInfo();
                                    listInfo.Name = listName;
                                    listInfo.TemplateType = listTemplate;
                                    listInfo.isLib = (listTemplate == SPDGListTemplateType.DocumentLibrary ? true : false);
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

        private SPDGListTemplateType getNextTemplateType()
        {
            if (lastTemplateType == SPDGListTemplateType.NoListTemplate)
            {
                if( workingDefinition.LibTypeList)
                {
                    lastTemplateType = SPDGListTemplateType.GenericList;
                    lastListPrefix = "List";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeDocument)
                {
                    lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    lastListPrefix = "Library";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeTasks)
                {
                    lastTemplateType = SPDGListTemplateType.Tasks;
                    lastListPrefix = "Tasks";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeCalendar)
                {
                    lastTemplateType = SPDGListTemplateType.Events;
                    lastListPrefix = "Events";
                    return lastTemplateType ;
                }
            }

            if (lastTemplateType == SPDGListTemplateType.GenericList)
            {
                if (workingDefinition.LibTypeDocument)
                {
                    lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    lastListPrefix = "Library";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeTasks)
                {
                    lastTemplateType = SPDGListTemplateType.Tasks;
                    lastListPrefix = "Tasks";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeCalendar)
                {
                    lastTemplateType = SPDGListTemplateType.Events;
                    lastListPrefix = "Events";
                    return lastTemplateType;
                }
            }

            if (lastTemplateType == SPDGListTemplateType.DocumentLibrary)
            {
                if (workingDefinition.LibTypeTasks)
                {
                    lastTemplateType = SPDGListTemplateType.Tasks;
                    lastListPrefix = "Tasks";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeCalendar)
                {
                    lastTemplateType = SPDGListTemplateType.Events;
                    lastListPrefix = "Events";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeList)
                {
                    lastTemplateType = SPDGListTemplateType.GenericList;
                    lastListPrefix = "List";
                    return lastTemplateType;
                }
            }

            if (lastTemplateType == SPDGListTemplateType.Tasks)
            {
                if (workingDefinition.LibTypeCalendar)
                {
                    lastTemplateType = SPDGListTemplateType.Events;
                    lastListPrefix = "Events";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeList)
                {
                    lastTemplateType = SPDGListTemplateType.GenericList;
                    lastListPrefix = "List";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeDocument)
                {
                    lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    lastListPrefix = "Library";
                    return lastTemplateType;
                }
            }

            if (lastTemplateType == SPDGListTemplateType.Events)
            {
                if (workingDefinition.LibTypeList)
                {
                    lastTemplateType = SPDGListTemplateType.GenericList;
                    lastListPrefix = "List";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeDocument)
                {
                    lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    lastListPrefix = "Library";
                    return lastTemplateType;
                }
                else if (workingDefinition.LibTypeTasks)
                {
                    lastTemplateType = SPDGListTemplateType.Tasks;
                    lastListPrefix = "Tasks";
                    return lastTemplateType;
                }
            }

            return lastTemplateType;
        }

        private string findAvailableListName(SPDGWeb web)
        {
            string candidate = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);

            if (lastTemplateType == SPDGListTemplateType.Tasks)
            {
                candidate += " Tasks";
            }
            else if (lastTemplateType == SPDGListTemplateType.Events)
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
