using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Acceleratio.SPDG.Generator.Model;
using Acceleratio.SPDG.Generator.Objects;
using Acceleratio.SPDG.Generator.Server;
using Acceleratio.SPDG.Generator.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Workflow;

namespace Acceleratio.SPDG.Generator
{
    public partial class ServerDataGenerator : DataGenerator
    {
        static bool containsUrl(SPWebCollection SubWebsCollection, string Url)
        {            
            foreach (SPWeb web in SubWebsCollection)
            {
                if (web.Url.EndsWith(Url))
                {
                    return true;
                }
                web.Dispose();
            }

            return false;
        }

        public static string GenerateWebUrl(SPWeb parentWeb, string childWebName)
        {
            string sessionUrl = Utilities.Path.GenerateSlug(childWebName, 256 - parentWeb.Url.Length - 38 - 1);

            //if url already exists, add guid
            if (containsUrl(parentWeb.Webs, sessionUrl))
            {
                sessionUrl += "_" + Guid.NewGuid().ToString();
            }

            return sessionUrl;
        }

        public ServerDataGenerator(ServerGeneratorDefinition definition) : base(definition)
        {
        }

        protected new ServerGeneratorDefinition WorkingDefinition
        {
            get { return (ServerGeneratorDefinition) base.WorkingDefinition; }
        }

        protected override SPDGObjectsFactory CreateObjectsFactory()
        {
            return new SPDGServerObjectsFactory();
        }


        protected override int CalculateTotalItemsForProgressReporting()
        {
            var total = base.CalculateTotalItemsForProgressReporting();

            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateTotalListsForProgressReporting()
        {
            var total = base.CalculateTotalListsForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateTotalFoldersForProgressReporting()
        {
            var total = base.CalculateTotalFoldersForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }

            return total;
        }

        protected override int CalculateTotalColumnsAndViewsForProgressReporting()
        {
            var total = base.CalculateTotalColumnsAndViewsForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateTotalContentTypesForProgressReporting()
        {
            var total = base.CalculateTotalContentTypesForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateOverallPermissionsForProgressReporting(int totalInSiteCollection)
        {
            var total= base.CalculateOverallPermissionsForProgressReporting(totalInSiteCollection);
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateTotalSitesForProgressReporting()
        {
            var total= base.CalculateTotalSitesForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        public override bool startDataGeneration(BackgroundWorker backgroundWorker)
        {
            if (!WorkingDefinition.CredentialsOfCurrentUser)
            {
                var parts = WorkingDefinition.Username.Split('\\');

                if (Common.ImpersonateValidUser(parts[1], parts[0], WorkingDefinition.Password))
                {
                    try
                    {
                        return base.startDataGeneration(backgroundWorker);
                    }
                    finally
                    {
                        Common.UndoImpersonation();
                    }   
                }
            }
            else
            {
                return base.startDataGeneration(backgroundWorker);
            }
            return false;
        }


        protected override void CreateUsersAndGroups()
        {
            if (WorkingDefinition.NumberOfUsersToCreate > 0)
            {
                try
                {
                    Log.Write("Creating Active Directory users.");
                    AD.createUsers(WorkingDefinition.ADDomainName, WorkingDefinition.ADOrganizationalUnit, WorkingDefinition.NumberOfUsersToCreate);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                }
            }

            if (WorkingDefinition.NumberOfSecurityGroupsToCreate > 0)
            {
                try
                {
                    Log.Write("Creating Active Directory groups.");
                    AD.createGroups(WorkingDefinition.ADDomainName, WorkingDefinition.ADOrganizationalUnit, WorkingDefinition.NumberOfSecurityGroupsToCreate);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                }

            }


        }




        List<string> _allUsers;
        List<string> _allGroups;
        protected override List<string> GetAvailableUsersInDirectory()
        {
            if (_allUsers == null)
            {
                _allUsers = AD.GetUsersFromAD();
            }
            return _allUsers;
        }

        protected override List<string> GetAvailableGroupsInDirectory()
        {
            if (_allGroups == null)
            {
                _allGroups = AD.GetGroupsFromAD();
            }
            return _allGroups;
        }

        protected override void ResolveWebAppsAndSiteCollections()
        {
            if (WorkingDefinition.CreateNewSiteCollections > 0)
            {
                int totalProgress = WorkingDefinition.CreateNewSiteCollections;
                if (WorkingDefinition.CreateNewWebApplications > 0)
                {
                    totalProgress = (WorkingDefinition.CreateNewSiteCollections * WorkingDefinition.CreateNewWebApplications) + WorkingDefinition.CreateNewWebApplications;
                }
                updateProgressOverall("Creating Web Applications / Site Collections", totalProgress);
            }


            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                createNewWebApplications();
            }
            else if (WorkingDefinition.CreateNewSiteCollections > 0)
            {
                createNewSiteCollections();
            }
            else
            {
                SiteCollInfo siteCollInfo = new SiteCollInfo();
                siteCollInfo.URL = WorkingDefinition.SiteCollection;
                workingSiteCollections.Add(siteCollInfo);
            }

        }

        private void createNewSiteCollections()
        {
            try
            {
                SPWebService spWebService = SPWebService.ContentService;
                SPWebApplication webApp = spWebService.WebApplications.First(a => a.Id == new Guid(WorkingDefinition.UseExistingWebApplication));

                for (int s = 0; s < WorkingDefinition.CreateNewSiteCollections; s++)
                {
                    CreateSiteCollection(webApp);
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }
        }

        private void CreateSiteCollection(SPWebApplication webApp)
        {
            string sitCollName = "";
            string url = "";
            string baseName = "";

            findAvailableSiteCollectionName(webApp, out sitCollName, out url, out baseName);

            updateProgressDetail("Creating site collection '" + url + "'");

            SPSiteCollection siteCollections = webApp.Sites;
            SPSite site = siteCollections.Add("/sites/" + url, WorkingDefinition.SiteCollOwnerLogin,
                WorkingDefinition.SiteCollOwnerEmail);

            SPWeb web = site.RootWeb;
            web.Title = sitCollName;
            web.Update();

            SiteCollInfo siteCollInfo = new SiteCollInfo();
            siteCollInfo.URL = site.Url;

            workingSiteCollections.Add(siteCollInfo);
        }

        private void findAvailableSiteCollectionName(SPWebApplication webApp, out string siteName, out string url, out string baseName)
        {
            baseName = "";
            siteName = SampleData.GetSampleValueRandom(SampleData.Companies);
            string siteUrl = Utilities.Path.GenerateSlug(siteName, 25);

            int i = 0;
            while (webApp.Sites.Any(s => s.Url.Contains(siteUrl)))
            {
                siteName = SampleData.GetRandomName(SampleData.Companies, SampleData.Offices, null, ref i, out baseName);
                siteUrl = Utilities.Path.GenerateSlug(siteName, 25);
            }

            url = siteUrl;
        }

        private void createNewWebApplications()
        {
            try
            {
                int currentPort = 80;
                for (int a = 0; a < WorkingDefinition.CreateNewWebApplications; a++)
                {


                    int appNumer = a + 1;
                    SPWebApplication newApplication;
                    SPWebApplicationBuilder webAppBuilder = new SPWebApplicationBuilder(SPFarm.Local);

                    currentPort = Common.GetNextAvailablePort(currentPort + 1);
                    updateProgressDetail("Creating Web application '" + "SPDG Site " + currentPort.ToString("00") + "'");
                    webAppBuilder.Port = currentPort;
                    webAppBuilder.RootDirectory = new DirectoryInfo("C:\\inetpub\\wwwroot\\wss\\" + currentPort.ToString());
                    webAppBuilder.ApplicationPoolId = "SPDG Pool " + currentPort.ToString();
                    webAppBuilder.IdentityType = IdentityType.SpecificUser;
                    webAppBuilder.ApplicationPoolUsername = WorkingDefinition.WebAppOwnerLogin;
                    webAppBuilder.ApplicationPoolPassword = Common.StringToSecureString(WorkingDefinition.WebAppOwnerPassword);


                    webAppBuilder.ServerComment = "SPDG Site " + currentPort.ToString("00");
                    webAppBuilder.CreateNewDatabase = true;
                    webAppBuilder.DatabaseServer = WorkingDefinition.DatabaseServer; // DB server name
                    webAppBuilder.DatabaseName = "WSS_Content_SPDG_" + currentPort.ToString("00");// DB Name
                    //webAppBuilder.HostHeader = "SPDG" + appNumer.ToString("00") + ".com"; //if any 

                    webAppBuilder.UseNTLMExclusively = true;  // authentication provider for NTLM
                    webAppBuilder.AllowAnonymousAccess = true; // anonymous access permission

                    // Finally create web application
                    newApplication = webAppBuilder.Create();

                    //Enable Claims
                    newApplication.UseClaimsAuthentication = true;
                    newApplication.Update();
                    newApplication.Provision();


                    for (int s = 0; s < WorkingDefinition.CreateNewSiteCollections; s++)
                    {
                        CreateSiteCollection(newApplication);
                    }

                }

            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }
        }



        public static void AddWorklfowTemplate()
        {
            try
            {
                bool exists = false;
                SPSolutionCollection solutionCollection = SPFarm.Local.Solutions;
                foreach (SPSolution sol in solutionCollection)
                {
                    if (sol.Name.ToLower() == "spdg workflow2.wsp")
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    SPSolution solution = SPFarm.Local.Solutions.Add("SampleData\\SPDG Workflow2.wsp");
                    solution.DeployLocal(true, true);
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }
        }


        protected override void AssociateCustomWorkflows()
        {
            if (!WorkingDefinition.AttachCustomWorkflowToList)
            {
                return;
            }

            updateProgressOverall("Adding Declarative Workflow Associations", 20);

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

                                for (int i = 0; i < wfTemplates.Count; i++)
                                {
                                    SPWorkflowTemplate wfTemplate = wfTemplates[i];
                                    if (wfTemplate.Name == "SPDG Workflow2")
                                    {
                                        templateIndex = i;
                                    }
                                }

                                if (templateIndex == -1)
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

        protected override void AssociateWorkflows()
        {
            if (!WorkingDefinition.CreateOutOfTheBoxWorkflowsToList)
            {
                return;
            }

            updateProgressOverall("Adding Workflow Associations", 20);

            //Site Collection feature for more workflows 0af5989a-3aea-4519-8ab0-85d91abe39ff
            //SPFeatureDefinition featureDef = SPFarm.Local.FeatureDefinitions.First(x => x.DisplayName.Contains("SPDG Worfklow"));

            try
            {
                foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
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

                                // web.Features.Add(new Guid("17463962-06a6-4aba-b49f-2c13222f6213"));

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
                                            updateProgressDetail("Adding Workflow Association to list '" + web.Url + "/" + list.RootFolder.Url + "'");
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




        public static List<string> allSiteColumns;

        public void CreateContentTypes()
        {
            if (!WorkingDefinition.CreateContentTypes || WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection == 0)
            {
                return;
            }

            int totalProgress = CalculateTotalContentTypesForProgressReporting();

            updateProgressOverall("Creating Content Types", totalProgress);

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            Log.Write("Creating Content Types for site:" + web.Url);
                            for (int c = 0; c < WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection; c++)
                            {
                                try
                                {
                                    string contentTypeName = findAvailableContentTypeName(web);
                                    updateProgressDetail("Creating Content Type '" + contentTypeName + "'");
                                    SPContentType contentType = new SPContentType(web.ContentTypes["Document"], web.ContentTypes, contentTypeName + " Document");
                                    web.ContentTypes.Add(contentType);
                                    contentType.Group = "Custom SPDG Content Types";
                                    contentType.Description = contentTypeName + " content type";
                                    List<string> randomSiteColumns = GetRandomSiteColumns();
                                    foreach (string siteColumn in randomSiteColumns)
                                    {
                                        contentType.FieldLinks.Add(new SPFieldLink(siteColl.RootWeb.Fields.GetField(siteColumn)));
                                    }

                                    contentType.Update();


                                    if (WorkingDefinition.ContentTypesCanInheritFromOtherContentType)
                                    {
                                        c++;
                                        if (c < WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection)
                                        {
                                            contentTypeName = findAvailableContentTypeName(web);
                                            updateProgressDetail("Creating Content Type '" + contentTypeName + "'");
                                            SPContentType childContentType = new SPContentType(contentType, web.ContentTypes, contentTypeName + " Document");
                                            web.ContentTypes.Add(childContentType);
                                            childContentType.Group = "Custom SPDG Content Types";
                                            childContentType.Description = contentTypeName + " content type";
                                            randomSiteColumns = GetRandomSiteColumns();
                                            foreach (string siteColumn in randomSiteColumns)
                                            {
                                                contentType.FieldLinks.Add(new SPFieldLink(siteColl.RootWeb.Fields.GetField(siteColumn)));
                                            }
                                            childContentType.Update();
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
                }
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

        private static List<string> GetRandomSiteColumns()
        {
            if (allSiteColumns == null)
            {
                allSiteColumns = new List<string>();
                allSiteColumns.Add("Address");
                allSiteColumns.Add("Birthday");
                allSiteColumns.Add("Business Phone");
                allSiteColumns.Add("Car Phone");
                allSiteColumns.Add("City");
                allSiteColumns.Add("Company");
                allSiteColumns.Add("Department");
                allSiteColumns.Add("E-Mail");
                allSiteColumns.Add("First Name");
                allSiteColumns.Add("Home Phone");
                allSiteColumns.Add("Other Address City");
                allSiteColumns.Add("Related Company");
                allSiteColumns.Add("Radio Phone");
                allSiteColumns.Add("E-mail 2");
                allSiteColumns.Add("E-mail 3");
            }

            List<string> randomSites = new List<string>();
            Random random = new Random();
            for (int i = 0; i < 7; i++)
            {
                int randomNumber = random.Next(0, allSiteColumns.Count - 1);
                if (!randomSites.Any(x => x == allSiteColumns[randomNumber]))
                {
                    randomSites.Add(allSiteColumns[randomNumber]);
                }
            }

            return randomSites;
        }


    }
}