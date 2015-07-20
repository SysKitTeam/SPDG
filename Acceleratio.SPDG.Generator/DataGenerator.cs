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
        public int DetailsProgressMaxSteps = 0;
        public int DetailsCurrentStep = 0;
        public string OverallCurrentStepDescription = string.Empty;
        public static string SessionID;

        public DataGenerator(GeneratorDefinition definition)
        {
            workingDefinition = definition;
        }

        public bool startDataGeneration(BackgroundWorker bgWorker)
        {
            try
            {   
                SessionID = "Session " + DateTime.Now.ToString("yy-MM-dd") + " " +  DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() ;
                Log.Write("SHAREPOINT DATA GENERATION SESSION STARTS");

                //Creates or sets Web applications and Site Collections
                OverallCurrentStep = 1;
                OverallCurrentStepDescription = "Creates or sets Web applications and Site Collections";
                Log.Write("CREATES OR SETS WEB APPLICATIONS AND SITE COLLECTIONS");
                bgWorker.ReportProgress(1);
                ResolveWebAppsAndSiteCollections();

                //Create sites in previously defined Site Collections
                OverallCurrentStep = 2;
                OverallCurrentStepDescription = "Create sites in previously defined Site Collections";
                Log.Write("CREATE SITES IN PREVIOUSLY DEFINED SITE COLLECTIONS");
                bgWorker.ReportProgress(1);
                CreateSites();

                //Create lists and libraries
                OverallCurrentStep = 3;
                OverallCurrentStepDescription = "Create lists and libraries";
                Log.Write("CREATE LISTS AND LIBRARIES");
                bgWorker.ReportProgress(1);
                CreateLists();

                //Create folders with nested folder levels
                OverallCurrentStep = 4;
                OverallCurrentStepDescription = "Create folders with nested folder levels";
                Log.Write("CREATE FOLDERS WITH NESTED FOLDER LEVELS");
                bgWorker.ReportProgress(1);
                CreateFolders();

                //Create folders with nested folder levels
                OverallCurrentStep = 5;
                OverallCurrentStepDescription = "Create list columns and views";
                Log.Write("CREATE LIST COLUMNS AND VIEW");
                bgWorker.ReportProgress(1);
                CreateColumnsAndViews();

                //Create content types
                OverallCurrentStep = 6;
                OverallCurrentStepDescription = "Create site content types";
                Log.Write("CREATE SITE CONTENT TYPES");
                bgWorker.ReportProgress(1);
                CreateContentTypes();

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
