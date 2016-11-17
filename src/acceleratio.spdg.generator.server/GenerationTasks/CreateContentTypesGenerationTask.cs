using System;
using System.Collections.Generic;
using System.Linq;
using Acceleratio.SPDG.Generator.GenerationTasks;
using Acceleratio.SPDG.Generator.Structures;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Server.GenerationTasks
{
    class CreateContentTypesGenerationTask : DataGenerationTaskBase
    {
        private const double _contentTypeGroupModifier = 0.3;
        private const double _contentTypeListAssignmentModifier = 0.3;
        private List<string> _contentTypeGroupNames; 
        public override string Title
        {
            get { return "Creating Content Types"; }
        }

        new ServerGeneratorDefinition WorkingDefinition { get { return (ServerGeneratorDefinition) base.WorkingDefinition; } }

        public CreateContentTypesGenerationTask(IDataGenerationTaskOwner owner) : base(owner)
        {
        }
        public override int CalculateTotalSteps()
        {

            if (!WorkingDefinition.CreateContentTypes || WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection == 0)
            {
                return 0;
            }
            int totalSteps = Owner.WorkingSiteCollections.Count * WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection *
                       WorkingDefinition.NumberOfSitesToCreate;

            return totalSteps;
        }

        public override void Execute()
        {
            CreateContentTypes();
            AddCustomContentTypesToLists();           
        }

        private List<SPContentType> _newContentTypes;
        public void CreateContentTypes()
        {        
            foreach (SiteCollInfo siteCollInfo in Owner.WorkingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    _newContentTypes = createContentTypesOnRootWeb(siteColl.RootWeb);                    
                }
            }
        }

        private List<SPContentType> createContentTypesOnRootWeb(SPWeb web)
        {
            List<SPContentType> newContentTypes = new List<SPContentType>(); 
            generateCustomContentTypeGroupNames();
            for (int c = 0; c < WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection; c++)
            {
                try
                {
                    string contentTypeName = findAvailableContentTypeName(web);
                    Owner.IncrementCurrentTaskProgress("Creating Content Type '" + contentTypeName + "'");
                    SPContentType contentType = new SPContentType(web.ContentTypes["Document"], web.ContentTypes,
                        contentTypeName + " Document");
                    web.ContentTypes.Add(contentType);
                    contentType.Group = getCustomContentTypeName();
                    contentType.Description = contentTypeName + " content type";
                    List<string> randomSiteColumns = GetRandomSiteColumns();
                    foreach (string siteColumn in randomSiteColumns)
                    {
                        contentType.FieldLinks.Add(new SPFieldLink(web.Fields.GetField(siteColumn)));
                    }

                    contentType.Update();
                    newContentTypes.Add(contentType);

                    if (WorkingDefinition.ContentTypesCanInheritFromOtherContentType)
                    {
                        c++;
                        if (c < WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection)
                        {
                            contentTypeName = findAvailableContentTypeName(web);
                            Owner.IncrementCurrentTaskProgress("Creating Content Type '" + contentTypeName + "'");
                            SPContentType childContentType = new SPContentType(contentType, web.ContentTypes,
                                contentTypeName + " Document");
                            web.ContentTypes.Add(childContentType);
                            childContentType.Group = getCustomContentTypeName();
                            childContentType.Description = contentTypeName + " content type";
                            randomSiteColumns = GetRandomSiteColumns();
                            foreach (string siteColumn in randomSiteColumns)
                            {
                                contentType.FieldLinks.Add(new SPFieldLink(web.Fields.GetField(siteColumn)));
                            }
                            childContentType.Update();
                            newContentTypes.Add(childContentType);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                }                
            }

            return newContentTypes;
        } 

        public void AddCustomContentTypesToLists()
        {
            foreach (SiteCollInfo siteCollInfo in Owner.WorkingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            Log.Write("Applying Content Types to lists for site: " + web.Url);
                            int listCount = web.Lists.Count;
                            if (listCount == 0)
                            {
                                continue;
                            }

                            foreach (SPContentType contentType in _newContentTypes)
                            {
                                for (int i = 0; i < (listCount * _contentTypeListAssignmentModifier); i++)
                                {
                                    var listIndex = SampleData.GetRandomNumber(0, listCount);
                                    if (!web.Lists[listIndex].ContentTypesEnabled)
                                    {
                                        continue;
                                    }

                                    if (web.Lists[listIndex].ContentTypes[contentType.Name] == null)
                                    {
                                        web.Lists[listIndex].ContentTypes.Add(contentType);
                                    }
                                }
                            }
                                                        
                        }
                    }
                }
            }
        }

        private string getCustomContentTypeName()
        {
            int index = SampleData.GetRandomNumber(0, _contentTypeGroupNames.Count);
            return _contentTypeGroupNames[index];
        }

        private void generateCustomContentTypeGroupNames()
        {
            _contentTypeGroupNames = new List<string>();
            int numberOfGroups = Convert.ToInt32(Math.Ceiling(WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection*_contentTypeGroupModifier));
            for (int i = 0; i < numberOfGroups; i++)
            {
                _contentTypeGroupNames.Add(findAvailableContentTypeGroupName());
            }
        }

        private string findAvailableContentTypeGroupName()
        {
            string candidate = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes) + " Group";
            
            if (_contentTypeGroupNames.Contains(candidate))
            {
                return findAvailableContentTypeGroupName();
            }
            else
            {
                return candidate;
            }
        }

        private string findAvailableContentTypeName(SPWeb web)
        {
            string candidate = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);
            bool alreadyExists = false;
            foreach (SPContentType ct in web.ContentTypes)
            {
                if (ct.Name == candidate)
                {
                    alreadyExists = true;
                }
            }

            if (alreadyExists)
            {
                return findAvailableContentTypeName(web);
            }
            else
            {
                return candidate;
            }
        }


        public static List<string> _allSiteColumns;
        private static List<string> GetRandomSiteColumns()
        {
            if (_allSiteColumns == null)
            {
                _allSiteColumns = new List<string>();
                _allSiteColumns.Add("Address");
                _allSiteColumns.Add("Birthday");
                _allSiteColumns.Add("Business Phone");
                _allSiteColumns.Add("Car Phone");
                _allSiteColumns.Add("City");
                _allSiteColumns.Add("Company");
                _allSiteColumns.Add("Department");
                _allSiteColumns.Add("E-Mail");
                _allSiteColumns.Add("First Name");
                _allSiteColumns.Add("Home Phone");
                _allSiteColumns.Add("Other Address City");
                _allSiteColumns.Add("Related Company");
                _allSiteColumns.Add("Radio Phone");
                _allSiteColumns.Add("E-mail 2");
                _allSiteColumns.Add("E-mail 3");
            }

            List<string> randomSites = new List<string>();
            Random random = new Random();
            for (int i = 0; i < 7; i++)
            {
                int randomNumber = random.Next(0, _allSiteColumns.Count - 1);
                if (!randomSites.Any(x => x == _allSiteColumns[randomNumber]))
                {
                    randomSites.Add(_allSiteColumns[randomNumber]);
                }
            }

            return randomSites;
        }


    }
}
