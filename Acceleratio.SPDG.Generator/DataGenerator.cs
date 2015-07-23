using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        GeneratorDefinition workingDefinition;
        List<SiteCollInfo> workingSiteCollections = new List<SiteCollInfo>();
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

        public DataGenerator(GeneratorDefinition definition)
        {
            workingDefinition = definition;
        }

        internal void progressOverall(string overallCurrentStepDescription, int detailsMaxSteps)
        {
            OverallCurrentStep++;
            OverallCurrentStepDescription = overallCurrentStepDescription;
            DetailProgressMaxSteps = detailsMaxSteps;
            bgWorker.ReportProgress(1);
            Log.Write("***" + overallCurrentStepDescription.ToUpper() + "***"); 
        }

        internal void progressDetail(string detailStepDescription)
        {
            DetailCurrentStep++;
            DetailCurrentStepDescription = detailStepDescription;
            bgWorker.ReportProgress(2);
            Log.Write(detailStepDescription);
        }

        public bool startDataGeneration(BackgroundWorker backgroundWorker)
        {
            try
            {
                bgWorker = backgroundWorker;
                SessionID = "Session " + DateTime.Now.ToString("yy-MM-dd") + " " +  DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() ;
                Log.Write("*** SHAREPOINT DATA GENERATION SESSION STARTS ***");

                //Creates or sets Web applications and Site Collections
                ResolveWebAppsAndSiteCollections();

                //Create sites in previously defined Site Collections
                CreateSites();

                //Create lists and libraries
                CreateLists();

                //Create folders with nested folder levels
                CreateFolders();

                //Create folders with nested folder levels
                CreateColumnsAndViews();

                //Create content types
                CreateContentTypes();

                //Create items and documents
                CreateItemsAndDocuments();

                //Create permissions
                CreatePermissions();

                Log.Write("*** SHAREPOINT DATA GENERATION SESSION COMPLETED ***");
                bgWorker.ReportProgress(3);

                return true;
            }
            catch(Exception ex)
            {
                Errors.Log(ex);
            }

            return false;

            
        }
    }
}
