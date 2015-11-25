using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Acceleratio.SPDG.Generator.Objects;

namespace Acceleratio.SPDG.Generator
{
    public abstract partial class DataGenerator
    {
        private GeneratorDefinitionBase workingDefinition;
        protected  GeneratorDefinitionBase WorkingDefinition
        {
            get { return workingDefinition; }
        }

        private SPDGObjectsFactory _objectsFactory;

        protected SPDGObjectsFactory ObjectsFactory
        {
            get
            {
                if (_objectsFactory == null)
                {
                    _objectsFactory = CreateObjectsFactory();
                }
                return _objectsFactory;
            }
        }

        protected abstract SPDGObjectsFactory CreateObjectsFactory();
        

        protected List<SiteCollInfo> workingSiteCollections = new List<SiteCollInfo>();


        public event OverallProgressHandler OverallProgressChanged;
        public EventArgs e = null;
        public delegate void OverallProgressHandler(EventArgs e);
        public int OverallProgressMaxSteps = 10;
        public int OverallCurrentStep = 0;
        public int DetailProgressMaxSteps = 0;
        public int DetailCurrentStep = 0;
        public string OverallCurrentStepDescription = string.Empty;
        public string DetailCurrentStepDescription = string.Empty;
        public static string SessionID;
        internal BackgroundWorker bgWorker;

        public DataGenerator(GeneratorDefinitionBase definition)
        {
            workingDefinition = definition;
        }

        public static DataGenerator Create(GeneratorDefinitionBase definition)
        {
            if (definition is ServerGeneratorDefinition)
            {
                return  new ServerDataGenerator((ServerGeneratorDefinition) definition);
            }
            else
            {
                return new ClientDataGenerator((ClientGeneratorDefinition) definition);
            }
        }

        internal void progressOverall(string overallCurrentStepDescription, int detailsMaxSteps)
        {
            OverallCurrentStep++;
            OverallCurrentStepDescription = overallCurrentStepDescription;
            DetailProgressMaxSteps = detailsMaxSteps;
            DetailCurrentStep = 0;
            bgWorker.ReportProgress(1);
            Log.Write("***" + overallCurrentStepDescription.ToUpper() + "***"); 
        }

        

        internal void progressDetail(string detailStepDescription, int incrementInProgress=1)
        {
            DetailCurrentStep+= incrementInProgress;
            if (!string.IsNullOrEmpty(detailStepDescription))
            {
                DetailCurrentStepDescription = detailStepDescription;
                Log.Write(detailStepDescription);
            }
            bgWorker.ReportProgress(2);
            
        }

        protected abstract void CreateUsersAndGroups();

        protected virtual int CalculateTotalItemsForProgressReporting()
        {
           var totalProgress = workingDefinition.NumberOfSitesToCreate *
                              (workingDefinition.MaxNumberOfListsAndLibrariesPerSite *
                          (workingDefinition.MaxNumberofItemsToGenerate + workingDefinition.MaxNumberofDocumentLibraryItemsToGenerate) 
                          + workingDefinition.NumberOfBigListsPerSite* workingDefinition.MaxNumberofItemsBigListToGenerate);

            if (workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateTotalListsForProgressReporting()
        {
            
            int progressTotal = workingDefinition.MaxNumberOfListsAndLibrariesPerSite*workingDefinition.NumberOfSitesToCreate;
            if (workingDefinition.CreateNewSiteCollections > 0)
            {
                progressTotal = progressTotal*workingDefinition.CreateNewSiteCollections;
            }
            return progressTotal;            
        }

        protected virtual int CalculateTotalFoldersForProgressReporting()
        {
            int totalProgress = workingDefinition.NumberOfSitesToCreate *
                       workingDefinition.MaxNumberOfFoldersToGenerate;

            if (workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateTotalColumnsAndViewsForProgressReporting()
        {
            int totalProgress = workingDefinition.MaxNumberOfColumnsPerList *
                      workingDefinition.NumberOfSitesToCreate *
                      workingDefinition.MaxNumberOfListsAndLibrariesPerSite +
                      (workingDefinition.MaxNumberOfViewsPerList *
                      workingDefinition.NumberOfSitesToCreate *
                      workingDefinition.MaxNumberOfListsAndLibrariesPerSite);

            if (workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateTotalSitesForProgressReporting()
        {
            int totalProgress = workingDefinition.NumberOfSitesToCreate;
            if (workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateTotalContentTypesForProgressReporting()
        {

            int totalProgress = workingDefinition.MaxNumberOfContentTypesPerSiteCollection *
                        workingDefinition.NumberOfSitesToCreate;

            if (workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateOverallPermissionsForProgressReporting(int totalInSiteCollection)
        {
            var total = totalInSiteCollection;
            if (workingDefinition.CreateNewSiteCollections > 0)
            {
                total = totalInSiteCollection * workingDefinition.CreateNewSiteCollections;
            }

            return total;
        }


        protected abstract void ResolveWebAppsAndSiteCollections();


        public virtual bool startDataGeneration(BackgroundWorker backgroundWorker)
        {
            try
            {
                bgWorker = backgroundWorker;
                
                Log.Write("*** SHAREPOINT DATA GENERATION SESSION STARTS ***");

                //Create AD users and groups
                CreateUsersAndGroups();
              
                //Creates or sets Web applications and Site Collections
                ResolveWebAppsAndSiteCollections();

                //Create sites in previously defined Site Collections
                CreateSites();

                //Create lists and libraries
                CreateLists();

                //Create folders with nested folder levels
                CreateFolders();

                //Create columns and views
                CreateColumnsAndViews();

                //Create content types
               // CreateContentTypes();

                //Create items and documents
                CreateItemsAndDocuments();

                //AssociateWorkflows                
                AssociateWorkflows();
                AssociateCustomWorkflows();

                //Create permissions
                CreatePermissions();

                Log.Write("*** SHAREPOINT DATA GENERATION SESSION COMPLETED ***");
                bgWorker.ReportProgress(3);

                return true;
            }
            catch(Exception ex)
            {
                Errors.Log(ex);
                bgWorker.ReportProgress(3);
            }

            return false;

            
        }

        protected abstract void AssociateWorkflows();
        protected abstract void AssociateCustomWorkflows();

    }
}
