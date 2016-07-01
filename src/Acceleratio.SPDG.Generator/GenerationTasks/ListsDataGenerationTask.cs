using System;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator.GenerationTasks
{
    public class ListsDataGenerationTask : DataGenerationTaskBase
    {
        SPDGListTemplateType _lastTemplateType = SPDGListTemplateType.NoListTemplate;
        string _lastListPrefix = "List";

        public ListsDataGenerationTask(IDataGenerationTaskOwner owner) : base(owner)
        {
        }

        public override string Title
        {
            get { return "Creating Lists and Libraries"; }
        }      

        public override int CalculateTotalSteps()
        {
            int totalSteps = WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite * WorkingDefinition.NumberOfSitesToCreate;
            totalSteps = totalSteps * Owner.WorkingSiteCollections.Count;
            return totalSteps;
        }

    
        public override void Execute()
        {            
            foreach (SiteCollInfo siteCollInfo in Owner.WorkingSiteCollections)
            {
                using (var siteColl = Owner.ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using(var web = siteColl.OpenWeb(siteInfo.ID)) 
                        {
                            Random rnd = new Random();
                            int listsToCreate = rnd.Next(WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite+1);                            
                            int bigListsToCreate = WorkingDefinition.NumberOfBigListsPerSite;
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
                                    Owner.IncrementCurrentTaskProgress("Created List '" + listName + "' in site '" + web.Url + "'");
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
                if( WorkingDefinition.LibTypeList)
                {
                    _lastTemplateType = SPDGListTemplateType.GenericList;
                    _lastListPrefix = "List";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeDocument)
                {
                    _lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    _lastListPrefix = "Library";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeTasks)
                {
                    _lastTemplateType = SPDGListTemplateType.Tasks;
                    _lastListPrefix = "Tasks";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeCalendar)
                {
                    _lastTemplateType = SPDGListTemplateType.Events;
                    _lastListPrefix = "Events";
                    return _lastTemplateType ;
                }
            }

            if (_lastTemplateType == SPDGListTemplateType.GenericList)
            {
                if (WorkingDefinition.LibTypeDocument)
                {
                    _lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    _lastListPrefix = "Library";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeTasks)
                {
                    _lastTemplateType = SPDGListTemplateType.Tasks;
                    _lastListPrefix = "Tasks";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeCalendar)
                {
                    _lastTemplateType = SPDGListTemplateType.Events;
                    _lastListPrefix = "Events";
                    return _lastTemplateType;
                }
            }

            if (_lastTemplateType == SPDGListTemplateType.DocumentLibrary)
            {
                if (WorkingDefinition.LibTypeTasks)
                {
                    _lastTemplateType = SPDGListTemplateType.Tasks;
                    _lastListPrefix = "Tasks";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeCalendar)
                {
                    _lastTemplateType = SPDGListTemplateType.Events;
                    _lastListPrefix = "Events";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeList)
                {
                    _lastTemplateType = SPDGListTemplateType.GenericList;
                    _lastListPrefix = "List";
                    return _lastTemplateType;
                }
            }

            if (_lastTemplateType == SPDGListTemplateType.Tasks)
            {
                if (WorkingDefinition.LibTypeCalendar)
                {
                    _lastTemplateType = SPDGListTemplateType.Events;
                    _lastListPrefix = "Events";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeList)
                {
                    _lastTemplateType = SPDGListTemplateType.GenericList;
                    _lastListPrefix = "List";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeDocument)
                {
                    _lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    _lastListPrefix = "Library";
                    return _lastTemplateType;
                }
            }

            if (_lastTemplateType == SPDGListTemplateType.Events)
            {
                if (WorkingDefinition.LibTypeList)
                {
                    _lastTemplateType = SPDGListTemplateType.GenericList;
                    _lastListPrefix = "List";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeDocument)
                {
                    _lastTemplateType = SPDGListTemplateType.DocumentLibrary;
                    _lastListPrefix = "Library";
                    return _lastTemplateType;
                }
                else if (WorkingDefinition.LibTypeTasks)
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
