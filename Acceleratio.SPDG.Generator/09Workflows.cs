using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        public static void AddWorklfowTemplate()
        {
            try
            {
                SPSolution solution = SPFarm.Local.Solutions.Add("SampleData\\SPDG Workflow.wsp");
                //Collection selectedWebApps = new Collection();
                //SPWebApplication webApp = SPWebApplication.Lookup(new Uri("http://localhost"));
                //selectedWebApps.Add(webApp);

                solution.DeployLocal(true, true);
                
            }
            catch { }
        }


        public void AssociateWorkflows()
        {

            //Site Collection feature for more workflows 0af5989a-3aea-4519-8ab0-85d91abe39ff
            //SPFeatureDefinition featureDef = SPFarm.Local.FeatureDefinitions.First(x => x.DisplayName.Contains("SPDG Worfklow"));

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    if( siteColl.Features[ new Guid("0af5989a-3aea-4519-8ab0-85d91abe39ff") ] == null )
                    {
                        siteColl.Features.Add(new Guid("0af5989a-3aea-4519-8ab0-85d91abe39ff"));
                    }

                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                        {

                           // web.Features.Add(new Guid("17463962-06a6-4aba-b49f-2c13222f6213"));

                            int s = web.WorkflowTemplates.Count;

                        }
                    }
                }
            }
        }
    }
}
