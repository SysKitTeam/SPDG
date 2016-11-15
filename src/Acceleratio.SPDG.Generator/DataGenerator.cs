using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Acceleratio.SPDG.Generator.GenerationTasks;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator
{
    public abstract class DataGenerator : IDataGenerationTaskOwner
    {
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged = delegate { };

        List<DataGenerationTaskBase> _tasks = new List<DataGenerationTaskBase>();
        private readonly GeneratorDefinitionBase _workingDefinition;
        protected  GeneratorDefinitionBase WorkingDefinition
        {
            get { return _workingDefinition; }
        }

        SPDGObjectsFactory IDataGenerationTaskOwner.ObjectsFactory
        {
            get { return ObjectsFactory; }
        }

        public List<SiteCollInfo> WorkingSiteCollections
        {
            get { return _workingSiteCollections; }
        }

        public void IncrementCurrentTaskProgress(string message, int incrementInProgress = 1)
        {
            updateProgressDetail(message, incrementInProgress);
        }

        
        GeneratorDefinitionBase IDataGenerationTaskOwner.WorkingDefinition
        {
            get { return WorkingDefinition; }
        }

        protected abstract SPDGObjectsFactory CreateObjectsFactory();

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

               
        protected List<SiteCollInfo> _workingSiteCollections = new List<SiteCollInfo>();                       
        
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
            int pct;
            if (_overallProgressMaxSteps == 0)
            {
                pct = 100;
            }
            else
            {
                pct = (int)((float)_overallCurrentStep / _overallProgressMaxSteps * 100);
            }
            
            ProgressChanged(this, new ProgressChangedEventArgs(ProgressChangeType.Overall, overallCurrentStepDescription, pct));
                        
            _detailProgressMaxSteps = detailsMaxSteps;
            _detailCurrentStep = 0;            
            Log.Write("***" + overallCurrentStepDescription.ToUpper() + "***"); 
        }

        protected void updateProgressDetail(string detailStepDescription, int incrementInProgress=1)
        {
            _detailCurrentStep+= incrementInProgress;
            int pct;
            if (_detailProgressMaxSteps == 0)
            {
                pct = _detailProgressMaxSteps;
            }
            else
            {
                pct = (int)((float)_detailCurrentStep / _detailProgressMaxSteps * 100);
            }
            
            if (!string.IsNullOrEmpty(detailStepDescription))
            {                
                Log.Write(detailStepDescription);
            }
            ProgressChanged(this, new ProgressChangedEventArgs(ProgressChangeType.Details, detailStepDescription, pct));
        }
        
        protected void RegisterTask<T>() where T : DataGenerationTaskBase
        {
            _tasks.Add((DataGenerationTaskBase)Activator.CreateInstance(typeof(T), this));
            
        }
        
        public virtual bool startDataGeneration()
        {
            try
            {                                
                Log.Write("*** SHAREPOINT DATA GENERATION SESSION STARTS ***");

                //if we are using and existing site collection and not creating any new ones
                if (!string.IsNullOrEmpty(WorkingDefinition.SiteCollection))
                {
                    SiteCollInfo siteCollInfo = new SiteCollInfo();
                    siteCollInfo.URL = WorkingDefinition.SiteCollection;
                    siteCollInfo.Sites = getSitesForSiteCollection();
                    WorkingSiteCollections.Add(siteCollInfo);
                } 
                _overallProgressMaxSteps = _tasks.Count();
                foreach (var task in _tasks)
                {
                    if (task.IsActive)
                    {
                        var totalSteps = task.CalculateTotalSteps();
                        updateProgressOverall(task.Title, totalSteps);
                        task.Execute();
                    }
                }                                
                Log.Write("*** SHAREPOINT DATA GENERATION SESSION COMPLETED ***");                
                return true;
            }
            catch(Exception ex)
            {
                Errors.Log(ex);                
            }
            return false;
        }

        private List<SiteInfo> getSitesForSiteCollection()
        {
            try
            {
                List<SiteInfo> sites = new List<SiteInfo>();
                var rootSite = ObjectsFactory.GetSite(WorkingDefinition.SiteCollection);

                foreach (var site in rootSite.RootWeb.Webs)
                {
                    sites.Add(new SiteInfo() { URL = site.Url, ID = site.ID });
                }

                return sites;
            }
            catch
            {
                return new List<SiteInfo>();
            }
        } 

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
                        Utils.QueryAssemblyInfo("Microsoft.SharePoint");
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
               var result = Utils.QueryAssemblyInfo("Microsoft.SharePoint");
                if (!string.IsNullOrEmpty(result))
                {
                    var m = Regex.Match(result, "(\\d+\\.\\d+\\.\\d+\\.\\d+)_");
                    if (m.Success)
                    {
                       return new Version(m.Groups[1].Value);   
                    }
                }                
            }
            catch (Exception)
            {
                _supportsServer = false;
            }
            return null;
        }   
    }
}
