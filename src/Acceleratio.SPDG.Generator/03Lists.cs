using System;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        SPDGListTemplateType _lastTemplateType = SPDGListTemplateType.NoListTemplate;
        string _lastListPrefix = "List";

        public void CreateLists()
        {
            if (_workingDefinition.MaxNumberOfListsAndLibrariesPerSite > 0 || _workingDefinition.NumberOfBigListsPerSite>0)
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
                            int listsToCreate = rnd.Next(_workingDefinition.MaxNumberOfListsAndLibrariesPerSite+1);                            
                            int bigListsToCreate = _workingDefinition.NumberOfBigListsPerSite;
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
                                        
                                        listTemplate = _lastTemplateType;
                                    }
                                    else
                                    {
                                        _lastListPrefix = "List";
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
            if (_lastTemplateType == SPDGListTemplateType.NoListTemplate)
            {
                if( _workingDefinition.LibTypeList)
                {
                    _lastTemplateType = SPDGListTemplateType.GenericList;
                    _lastListPrefix = "List";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeDocument)
                {
                    _lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    _lastListPrefix = "Library";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeTasks)
                {
                    _lastTemplateType = SPDGListTemplateType.Tasks;
                    _lastListPrefix = "Tasks";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeCalendar)
                {
                    _lastTemplateType = SPDGListTemplateType.Events;
                    _lastListPrefix = "Events";
                    return _lastTemplateType ;
                }
            }

            if (_lastTemplateType == SPDGListTemplateType.GenericList)
            {
                if (_workingDefinition.LibTypeDocument)
                {
                    _lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    _lastListPrefix = "Library";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeTasks)
                {
                    _lastTemplateType = SPDGListTemplateType.Tasks;
                    _lastListPrefix = "Tasks";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeCalendar)
                {
                    _lastTemplateType = SPDGListTemplateType.Events;
                    _lastListPrefix = "Events";
                    return _lastTemplateType;
                }
            }

            if (_lastTemplateType == SPDGListTemplateType.DocumentLibrary)
            {
                if (_workingDefinition.LibTypeTasks)
                {
                    _lastTemplateType = SPDGListTemplateType.Tasks;
                    _lastListPrefix = "Tasks";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeCalendar)
                {
                    _lastTemplateType = SPDGListTemplateType.Events;
                    _lastListPrefix = "Events";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeList)
                {
                    _lastTemplateType = SPDGListTemplateType.GenericList;
                    _lastListPrefix = "List";
                    return _lastTemplateType;
                }
            }

            if (_lastTemplateType == SPDGListTemplateType.Tasks)
            {
                if (_workingDefinition.LibTypeCalendar)
                {
                    _lastTemplateType = SPDGListTemplateType.Events;
                    _lastListPrefix = "Events";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeList)
                {
                    _lastTemplateType = SPDGListTemplateType.GenericList;
                    _lastListPrefix = "List";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeDocument)
                {
                    _lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    _lastListPrefix = "Library";
                    return _lastTemplateType;
                }
            }

            if (_lastTemplateType == SPDGListTemplateType.Events)
            {
                if (_workingDefinition.LibTypeList)
                {
                    _lastTemplateType = SPDGListTemplateType.GenericList;
                    _lastListPrefix = "List";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeDocument)
                {
                    _lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    _lastListPrefix = "Library";
                    return _lastTemplateType;
                }
                else if (_workingDefinition.LibTypeTasks)
                {
                    _lastTemplateType = SPDGListTemplateType.Tasks;
                    _lastListPrefix = "Tasks";
                    return _lastTemplateType;
                }
            }

            return _lastTemplateType;
        }

        private string findAvailableListName(SPDGWeb web)
        {
            string candidate = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);

            if (_lastTemplateType == SPDGListTemplateType.Tasks)
            {
                candidate += " Tasks";
            }
            else if (_lastTemplateType == SPDGListTemplateType.Events)
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
