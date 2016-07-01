using System;
using System.Linq;
using Acceleratio.SPDG.Generator.GenerationTasks;
using Acceleratio.SPDG.Generator.Structures;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Workflow;

namespace Acceleratio.SPDG.Generator.Server.GenerationTasks
{
    class AddDeclarativeAssociateWorkflowsGenerationTask : DataGenerationTaskBase
    {
        public override string Title
        {
            get { return "Adding Declarative Workflow Associations"; }
        }

        new ServerGeneratorDefinition WorkingDefinition { get { return (ServerGeneratorDefinition) base.WorkingDefinition; } }

        public AddDeclarativeAssociateWorkflowsGenerationTask(IDataGenerationTaskOwner owner) : base(owner)
        {
        }

        public override void Execute()
        {            
            associateCustomWorkflows();
        }

        public override int CalculateTotalSteps()
        {
            if (!WorkingDefinition.AttachCustomWorkflowToList)
            {
                return 0;
            }
            return Owner.WorkingSiteCollections.Sum(x => x.Sites.Sum(y => y.Lists.Count));
        }

        public static void AddWorklfowTemplate()
        {
            try
            {
                bool exists = false;
                SPSolutionCollection solutionCollection = SPFarm.Local.Solutions;
                foreach (SPSolution sol in solutionCollection)
                {
                    if (sol.Name.ToLower() == "spdg workflow.wsp")
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {                    
                    SPSolution solution = SPFarm.Local.Solutions.Add("SampleData\\SPDG Workflow.wsp");
                    solution.DeployLocal(true, true);
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }
        }

        protected void associateCustomWorkflows()
        {           
            AddWorklfowTemplate();

            try
            {
                foreach (SiteCollInfo siteCollInfo in Owner.WorkingSiteCollections)
                {
                    using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                    {

                        foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                        {
                            using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                            {
                                if (web.Features[new Guid("00bfea71-f600-43f6-a895-40c0de7b0117")] == null)
                                {
                                    web.Features.Add(new Guid("00bfea71-f600-43f6-a895-40c0de7b0117"));
                                }
                                else
                                {
                                    Owner.IncrementCurrentTaskProgress("",siteInfo.Lists.Count);
                                }

                                SPWorkflowTemplateCollection wfTemplates = web.WorkflowTemplates;
                                int templateIndex = -1;

                                for (int i = 0; i < wfTemplates.Count; i++)
                                {
                                    SPWorkflowTemplate wfTemplate = wfTemplates[i];
                                    if (wfTemplate.Name == "SPDG Workflow")
                                    {
                                        templateIndex = i;
                                    }
                                }

                                if (templateIndex == -1)
                                {
                                    Log.Write("Declarative Workflow 'SPDG Workflow' not found.");
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
                                        Owner.IncrementCurrentTaskProgress(string.Format("Added declarative workflow to {0} on subsite{1}", listInfo.Name, web.Url));

                                    }
                                    catch (Exception ex)
                                    {
                                        Errors.Log(ex);
                                        Owner.IncrementCurrentTaskProgress(string.Format("Error while adding declarative workflow to {0} on subsite{1}", listInfo.Name, web.Url));
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
    }
}
