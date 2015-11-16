using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Security;
using System.IO;
using Acceleratio.SPDG.Generator.Utilities;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.SiteHealth;

namespace Acceleratio.SPDG.Generator
{
    public partial class ServerDataGenerator
    {
        protected override void ResolveWebAppsAndSiteCollections()
        {
            if (WorkingDefinition.CreateNewSiteCollections > 0)
            {
                int totalProgress = WorkingDefinition.CreateNewSiteCollections;
                if (WorkingDefinition.CreateNewWebApplications > 0 )
                {
                    totalProgress = (WorkingDefinition.CreateNewSiteCollections * WorkingDefinition.CreateNewWebApplications) + WorkingDefinition.CreateNewWebApplications;
                }
                progressOverall("Creating Web Applications / Site Collections", totalProgress);
            }


            if( WorkingDefinition.CreateNewWebApplications > 0 )
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

            progressDetail("Creating site collection '" + url + "'");

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
                for( int a=0; a < WorkingDefinition.CreateNewWebApplications; a++ )
                {
                    

                    int appNumer = a + 1;
                    SPWebApplication newApplication;
                    SPWebApplicationBuilder webAppBuilder = new SPWebApplicationBuilder(SPFarm.Local);

                    currentPort = Common.GetNextAvailablePort(currentPort + 1);
                    progressDetail("Creating Web application '" + "SPDG Site " + currentPort.ToString("00") + "'");
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


                    for( int s=0; s<WorkingDefinition.CreateNewSiteCollections; s++)
                    {
                        CreateSiteCollection(newApplication);
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
