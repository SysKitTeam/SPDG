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
                bool exists = false;
                SPSolutionCollection solutionCollection = SPFarm.Local.Solutions;
                foreach( SPSolution sol in solutionCollection )
                {
                    if( sol.Name.ToLower() == "spdg workflow2.wsp")
                    {
                        exists = true;
                        break;
                    }
                }

                if( !exists )
                {
                    SPSolution solution = SPFarm.Local.Solutions.Add("SampleData\\SPDG Workflow2.wsp");
                    solution.DeployLocal(true, true);
                }
            }
            catch(Exception ex)
            {
                Errors.Log(ex);
            }
        }


        public void AssociateCustomWorkflows()
        {
            if( !workingDefinition.AttachCustomWorkflowToList)
            {
                return;
            }

            progressOverall("Adding Declarative Workflow Associations", 20);

            AddWorklfowTemplate();

            try
            {
                foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
                {
                    using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                    {

                        foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                        {
                            using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                            {
                                if (web.Features[new Guid("2e57115a-aa11-49ff-9113-b8e57afb8162")] == null)
                                {
                                    web.Features.Add(new Guid("2e57115a-aa11-49ff-9113-b8e57afb8162"));
                                }

                                SPWorkflowTemplateCollection wfTemplates = web.WorkflowTemplates;
                                int templateIndex = -1;

                                for (int i = 0; i < wfTemplates.Count; i++ )
                                {
                                    SPWorkflowTemplate wfTemplate = wfTemplates[i];
                                    if (wfTemplate.Name == "SPDG Workflow2")
                                    {
                                        templateIndex = i;
                                    }
                                }

                                if( templateIndex == -1)
                                {
                                    Log.Write("Declarative Workflow 'SPDG Workflow2' not found.");
                                    return;
                                }
                                

                                foreach (ListInfo listInfo in siteInfo.Lists)
                                {
                                    try
                                    {
                                        SPList list = web.Lists[listInfo.Name];
                                        SPWorkflowTemplate workflowTemplate = web.WorkflowTemplates[templateIndex];

                                        SPList wftasks = web.Lists.TryGetList("Workflow Tasks");
                                        if (wftasks == null)
                                        {
                                            //Check if Workflow List exists
                                            Guid listGuid = web.Lists.Add(
                                                "Workflow Tasks",
                                                string.Empty,
                                                SPListTemplateType.Tasks);
                                            wftasks = web.Lists.GetList(listGuid, false);
                                        }

                                        SPList wfhistory = web.Lists.TryGetList("Workflow History");
                                        if (wfhistory == null)
                                        {
                                            //Check if Workflow List exists
                                            Guid listGuid = web.Lists.Add(
                                                "Workflow History",
                                                string.Empty,
                                                SPListTemplateType.WorkflowHistory);
                                            wfhistory = web.Lists.GetList(listGuid, false);
                                            wfhistory.Hidden = true;
                                            wfhistory.Update();
                                        }


                                        // create the association
                                        SPWorkflowAssociation assoc =
                                            SPWorkflowAssociation.CreateListAssociation(
                                            workflowTemplate, workflowTemplate.Name,
                                            wftasks, wfhistory
                                            );
                                        assoc.AllowManual = true;

                                        //apply the association to the list
                                        list.WorkflowAssociations.Add(assoc);


                                    }
                                    catch (Exception ex)
                                    {
                                        Errors.Log(ex);
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }
        }

        public void AssociateWorkflows()
        {
            if( !workingDefinition.CreateOutOfTheBoxWorkflowsToList)
            {
                return;
            }

            progressOverall("Adding Workflow Associations", 20);

            //Site Collection feature for more workflows 0af5989a-3aea-4519-8ab0-85d91abe39ff
            //SPFeatureDefinition featureDef = SPFarm.Local.FeatureDefinitions.First(x => x.DisplayName.Contains("SPDG Worfklow"));

            try
            {
                foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
                {
                    using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                    {
                        // Activating out-of-the-box workflows
                        if( siteColl.Features[ new Guid("0af5989a-3aea-4519-8ab0-85d91abe39ff") ] == null )
                        {
                            siteColl.Features.Add(new Guid("0af5989a-3aea-4519-8ab0-85d91abe39ff"));
                        }

                        foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                        {
                            using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                            {

                               // web.Features.Add(new Guid("17463962-06a6-4aba-b49f-2c13222f6213"));

                                int workflowCount = web.WorkflowTemplates.Count;

                                foreach (ListInfo listInfo in siteInfo.Lists)
                                {
                                    try
                                    {
                                        SPList list = web.Lists[listInfo.Name];
                                        int randomNumberOfWF = SampleData.GetRandomNumber(1,2);
                                        List<int> addedTemplates = new List<int>();

                                        for(int i=0; i<randomNumberOfWF; i++)
                                        {
                                            progressDetail("Adding Workflow Association to list '" + web.Url + "/" + list.RootFolder.Url + "'");
                                            int templateIndex = SampleData.GetRandomNumber(0, 5);

                                            if(addedTemplates.Any(x => x == templateIndex ))
                                            {
                                                continue;
                                            }
                                        
                                            SPWorkflowTemplate workflowTemplate = web.WorkflowTemplates[templateIndex];

                                            SPList wftasks = web.Lists.TryGetList("Workflow Tasks");
                                            if(  wftasks == null )
                                            {
                                                //Check if Workflow List exists
                                                Guid listGuid = web.Lists.Add(
                                                    "Workflow Tasks",
                                                    string.Empty,
                                                    SPListTemplateType.Tasks);
                                                    wftasks = web.Lists.GetList(listGuid, false);
                                            }

                                            SPList wfhistory = web.Lists.TryGetList("Workflow History");
                                            if(  wfhistory == null )
                                            {
                                                //Check if Workflow List exists
                                                Guid listGuid = web.Lists.Add(
                                                    "Workflow History",
                                                    string.Empty,
                                                    SPListTemplateType.WorkflowHistory);
                                                    wfhistory = web.Lists.GetList(listGuid, false);
                                                    wfhistory.Hidden = true;
                                                    wfhistory.Update();
                                            }
                                        

                                            // create the association
                                            SPWorkflowAssociation assoc =
                                                SPWorkflowAssociation.CreateListAssociation(
                                                workflowTemplate, workflowTemplate.Name,
                                                wftasks, wfhistory
                                                );
                                            assoc.AllowManual = true;

                                            //apply the association to the list
                                            list.WorkflowAssociations.Add(assoc);

                                            addedTemplates.Add(templateIndex);
                                        }

                                    }
                                    catch( Exception ex )
                                    {
                                        Errors.Log(ex);
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Errors.Log(ex);
            }
        }
    }
}
