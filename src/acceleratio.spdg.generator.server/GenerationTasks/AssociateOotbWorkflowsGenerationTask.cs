using System;
using System.Collections.Generic;
using System.Linq;
using Acceleratio.SPDG.Generator.GenerationTasks;
using Acceleratio.SPDG.Generator.Structures;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace Acceleratio.SPDG.Generator.Server.GenerationTasks
{
    class AssociateOotbWorkflowsGenerationTask : DataGenerationTaskBase
    {
        public override string Title
        {
            get { return "Adding OOTB Workflow Associations"; }
        }

        new ServerGeneratorDefinition WorkingDefinition { get { return (ServerGeneratorDefinition) base.WorkingDefinition; } }

        public AssociateOotbWorkflowsGenerationTask(IDataGenerationTaskOwner owner) : base(owner)
        {
        }

        public override void Execute()
        {
            associateWorkflows();
        }

        public override int CalculateTotalSteps()
        {
            if (!WorkingDefinition.CreateOutOfTheBoxWorkflowsToList)
            {
                return 0;
            }
            return Owner.WorkingSiteCollections.Sum(x => x.Sites.Sum(y => y.Lists.Count));
        }

      


        void associateWorkflows()
        {
            try
            {
                foreach (SiteCollInfo siteCollInfo in Owner.WorkingSiteCollections)
                {
                    using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                    {
                        // Activating out-of-the-box workflows
                        if (siteColl.Features[new Guid("0af5989a-3aea-4519-8ab0-85d91abe39ff")] == null)
                        {
                            siteColl.Features.Add(new Guid("0af5989a-3aea-4519-8ab0-85d91abe39ff"));
                        }

                        foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                        {
                            using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                            {                                
                                int workflowCount = web.WorkflowTemplates.Count;

                                foreach (ListInfo listInfo in siteInfo.Lists)
                                {
                                    try
                                    {
                                        SPList list = web.Lists[listInfo.Name];
                                        int randomNumberOfWF = SampleData.GetRandomNumber(1, 2);
                                        List<int> addedTemplates = new List<int>();

                                        for (int i = 0; i < randomNumberOfWF; i++)
                                        {
                                            Owner.IncrementCurrentTaskProgress("Adding Workflow Association to list '" + web.Url + "/" + list.RootFolder.Url + "'");
                                            int templateIndex = SampleData.GetRandomNumber(0, 5);

                                            if (addedTemplates.Any(x => x == templateIndex))
                                            {
                                                continue;
                                            }

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

                                            addedTemplates.Add(templateIndex);
                                        }
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


    }
}
