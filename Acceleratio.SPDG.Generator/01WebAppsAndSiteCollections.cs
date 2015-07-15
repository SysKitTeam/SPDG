using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Security;
using System.IO;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        internal void ResolveWebAppsAndSiteCollections()
        {
            if( workingDefinition.CreateNewWebApplications > 0 )
            {
                createNewWebApplications();
            }
            else if (workingDefinition.CreateNewSiteCollections > 0)
            {
                createNewSiteCollections();
            }
            else
            {
                workingSiteCollections.Add(workingDefinition.SiteCollection);
            }

        }

        private void createNewSiteCollections()
        {
            try
            {
                SPWebService spWebService = SPWebService.ContentService;
                SPWebApplication webApp = spWebService.WebApplications.First(a => a.Id == new Guid(workingDefinition.UseExistingWebApplication));

                for (int s = 0; s < workingDefinition.CreateNewSiteCollections; s++)
                {
                    int siteCollNumber = s + 1 + webApp.Sites.Count;
                    SPSiteCollection siteCollections = webApp.Sites;
                    SPSite site = siteCollections.Add("/sites/SPDGSiteCollection" + siteCollNumber.ToString("00"), workingDefinition.OwnerLogin, workingDefinition.OwnerEmail);

                    workingSiteCollections.Add(site.Url);
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }
        }

        private void createNewWebApplications()
        {
            try
            {
                int currentPort = 80;
                for( int a=0; a < workingDefinition.CreateNewWebApplications; a++ )
                {
                    int appNumer = a + 1;
                    SPWebApplication newApplication;
                    SPWebApplicationBuilder webAppBuilder = new SPWebApplicationBuilder(SPFarm.Local);

                    currentPort = Common.GetNextAvailablePort(currentPort + 1);
                    webAppBuilder.Port = currentPort;
                    webAppBuilder.RootDirectory = new DirectoryInfo("C:\\inetpub\\wwwroot\\wss\\" + currentPort.ToString());
                    webAppBuilder.ApplicationPoolId = "SPDG Pool " + currentPort.ToString();
                    webAppBuilder.IdentityType = IdentityType.SpecificUser;
                    webAppBuilder.ApplicationPoolUsername = workingDefinition.OwnerLogin;
                    webAppBuilder.ApplicationPoolPassword = Common.StringToSecureString(workingDefinition.OwnerPassword);
                   

                    webAppBuilder.ServerComment = "SPDG Site " + currentPort.ToString("00");
                    webAppBuilder.CreateNewDatabase = true;
                    webAppBuilder.DatabaseServer = workingDefinition.DatabaseServer; // DB server name
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

                    for( int s=0; s<workingDefinition.CreateNewSiteCollections; s++)
                    {
                        int siteCollNumber = s + 1;
                        SPSiteCollection siteCollections = newApplication.Sites;
                        SPSite site = siteCollections.Add("/sites/SPDGSiteCollection" + siteCollNumber.ToString("00"), workingDefinition.OwnerLogin, workingDefinition.OwnerEmail);

                        workingSiteCollections.Add(site.Url);
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
