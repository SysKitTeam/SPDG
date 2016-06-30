using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;
using Acceleratio.SPDG.Generator.Utilities;

namespace Acceleratio.SPDG.Generator
{
    public abstract partial class DataGenerator
    {
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged = delegate { };


        private readonly GeneratorDefinitionBase _workingDefinition;
        protected  GeneratorDefinitionBase WorkingDefinition
        {
            get { return _workingDefinition; }
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


        
        public delegate void OverallProgressHandler(EventArgs e);
        public EventArgs e = null;
        
        private int _overallProgressMaxSteps = 10;
        private int _overallCurrentStep = 0;
        private int _detailProgressMaxSteps = 0;
        private int _detailCurrentStep = 0;                

        public static string SessionID;        

        public DataGenerator(GeneratorDefinitionBase definition)
        {
            _workingDefinition = definition;
        }

        public static DataGenerator Create(GeneratorDefinitionBase definition)
        {
            string assemblyName = "";
            string typeName = "";
            if (!definition.IsClientObjectModel && SupportsServer)
            {
                assemblyName = "Acceleratio.SPDG.Generator.Server";
                typeName = "Acceleratio.SPDG.Generator.Server.ServerDataGenerator";
            }
            else if(definition.IsClientObjectModel && SupportsClient)
            {
                assemblyName = "Acceleratio.SPDG.Generator.Client";
                typeName = "Acceleratio.SPDG.Generator.Client.ClientDataGenerator";
            }
            else
            {
              throw new InvalidOperationException();
            }

            if(!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
            {
                var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                var type = assembly.GetType(typeName);
                return (DataGenerator) Activator.CreateInstance(type, definition);
            }       
            else
            {
                throw new InvalidOperationException();
            }
        }

        protected void updateProgressOverall(string overallCurrentStepDescription, int detailsMaxSteps)
        {
            _overallCurrentStep++;
            int pct = (int) ((float) _overallCurrentStep/_overallProgressMaxSteps*100);
            ProgressChanged(this, new ProgressChangedEventArgs(ProgressChangeType.Overall, overallCurrentStepDescription, pct));
                        
            _detailProgressMaxSteps = detailsMaxSteps;
            _detailCurrentStep = 0;            
            Log.Write("***" + overallCurrentStepDescription.ToUpper() + "***"); 
        }



        protected void updateProgressDetail(string detailStepDescription, int incrementInProgress=1)
        {
            _detailCurrentStep+= incrementInProgress;
            int pct = (int)((float)_detailCurrentStep / _detailProgressMaxSteps * 100);
            if (!string.IsNullOrEmpty(detailStepDescription))
            {                
                Log.Write(detailStepDescription);
            }
            ProgressChanged(this, new ProgressChangedEventArgs(ProgressChangeType.Details, detailStepDescription, pct));
        }

        protected abstract void CreateUsersAndGroups();

        protected virtual int CalculateTotalItemsForProgressReporting()
        {
           var totalProgress = _workingDefinition.NumberOfSitesToCreate *
                              (_workingDefinition.MaxNumberOfListsAndLibrariesPerSite *
                          (_workingDefinition.MaxNumberofItemsToGenerate + _workingDefinition.MaxNumberofDocumentLibraryItemsToGenerate) 
                          + _workingDefinition.NumberOfBigListsPerSite* _workingDefinition.MaxNumberofItemsBigListToGenerate);

            if (_workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * _workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateTotalListsForProgressReporting()
        {
            
            int progressTotal = _workingDefinition.MaxNumberOfListsAndLibrariesPerSite*_workingDefinition.NumberOfSitesToCreate;
            if (_workingDefinition.CreateNewSiteCollections > 0)
            {
                progressTotal = progressTotal*_workingDefinition.CreateNewSiteCollections;
            }
            return progressTotal;            
        }

        protected virtual int CalculateTotalFoldersForProgressReporting()
        {
            int totalProgress = _workingDefinition.NumberOfSitesToCreate *
                       _workingDefinition.MaxNumberOfFoldersToGenerate;

            if (_workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * _workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateTotalColumnsAndViewsForProgressReporting()
        {
            int totalProgress = _workingDefinition.MaxNumberOfColumnsPerList *
                      _workingDefinition.NumberOfSitesToCreate *
                      _workingDefinition.MaxNumberOfListsAndLibrariesPerSite +
                      (_workingDefinition.MaxNumberOfViewsPerList *
                      _workingDefinition.NumberOfSitesToCreate *
                      _workingDefinition.MaxNumberOfListsAndLibrariesPerSite);

            if (_workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * _workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateTotalSitesForProgressReporting()
        {
            int totalProgress = _workingDefinition.NumberOfSitesToCreate;
            if (_workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * _workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateTotalContentTypesForProgressReporting()
        {

            int totalProgress = _workingDefinition.MaxNumberOfContentTypesPerSiteCollection *
                        _workingDefinition.NumberOfSitesToCreate;

            if (_workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * _workingDefinition.CreateNewSiteCollections;
            }
            return totalProgress;
        }

        protected virtual int CalculateOverallPermissionsForProgressReporting(int totalInSiteCollection)
        {
            var total = totalInSiteCollection;
            if (_workingDefinition.CreateNewSiteCollections > 0)
            {
                total = totalInSiteCollection * _workingDefinition.CreateNewSiteCollections;
            }

            return total;
        }


        protected abstract void ResolveWebAppsAndSiteCollections();


        public virtual bool startDataGeneration()
        {
            try
            {                
                
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

                return true;
            }
            catch(Exception ex)
            {
                Errors.Log(ex);                
            }

            return false;

            
        }

        protected abstract void AssociateWorkflows();
        protected abstract void AssociateCustomWorkflows();



        private static bool? _supportsClient;
        public static bool SupportsClient
        {
            get
            {
                if (_supportsClient == null)
                {

                    _supportsClient = Environment.Version.Major == 4;
                }
                return _supportsClient.Value;
            }
        }

        private static bool? _supportsServer;
        public static bool SupportsServer
        {
            get
            {
                if (_supportsServer == null)
                {
                    try
                    {
                        DllExistanceTester.QueryAssemblyInfo("Microsoft.SharePoint");
                        _supportsServer = true;
                    }
                    catch (Exception ex)
                    {
                        _supportsServer = false;
                        
                    }
                }
                return _supportsServer.Value;
            }
        }

        public static Version GetSharePointOnPremVersion()
        {
            try
            {
               var result =  DllExistanceTester.QueryAssemblyInfo("Microsoft.SharePoint");
                if (!string.IsNullOrEmpty(result))
                {
                    var m = Regex.Match(result, "(\\d+\\.\\d+\\.\\d+\\.\\d+)_");
                    if (m.Success)
                    {
                       return new Version(m.Groups[1].Value);   
                    }
                }                
            }
            catch (Exception ex)
            {
                _supportsServer = false;

            }
            return null;
        }
    }
}
