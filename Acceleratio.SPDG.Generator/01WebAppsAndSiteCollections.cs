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
            if (workingDefinition.CreateNewSiteCollections > 0)
            {
                int totalProgress = workingDefinition.CreateNewSiteCollections;
                if (workingDefinition.CreateNewWebApplications > 0 )
                {
                    totalProgress = (workingDefinition.CreateNewSiteCollections * workingDefinition.CreateNewWebApplications) + workingDefinition.CreateNewWebApplications;
                }
                progressOverall("Creating Web Applications / Site Collections", totalProgress);
            }


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
                SiteCollInfo siteCollInfo = new SiteCollInfo();
                siteCollInfo.URL = workingDefinition.SiteCollection;
                workingSiteCollections.Add(siteCollInfo);
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
                    string sitCollName = findAvailableSiteCollectionName(webApp);
                    progressDetail("Creating site collection '" + sitCollName + "'");
                    int siteCollNumber = s + 1 + webApp.Sites.Count;
                    
                    SPSiteCollection siteCollections = webApp.Sites;
                    SPSite site = siteCollections.Add("/sites/" + sitCollName, workingDefinition.SiteCollOwnerLogin, workingDefinition.SiteCollOwnerEmail);

                    SiteCollInfo siteCollInfo = new SiteCollInfo();
                    siteCollInfo.URL = site.Url;
                    
                    workingSiteCollections.Add(siteCollInfo);
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }
        }

        private string findAvailableSiteCollectionName(SPWebApplication webApp)
        {
            string siteCollCandidate = SampleData.Clean( SampleData.GetSampleValueRandom(SampleData.Countries) );

            while( webApp.Sites.Any(s => s.Url.Contains(siteCollCandidate)) )
            {
                siteCollCandidate = SampleData.Clean(SampleData.GetSampleValueRandom(SampleData.Countries));
            }
            
            return siteCollCandidate;
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
                    progressDetail("Creating Web application '" + "SPDG Site " + currentPort.ToString("00") + "'");
                    webAppBuilder.Port = currentPort;
                    webAppBuilder.RootDirectory = new DirectoryInfo("C:\\inetpub\\wwwroot\\wss\\" + currentPort.ToString());
                    webAppBuilder.ApplicationPoolId = "SPDG Pool " + currentPort.ToString();
                    webAppBuilder.IdentityType = IdentityType.SpecificUser;
                    webAppBuilder.ApplicationPoolUsername = workingDefinition.WebAppOwnerLogin;
                    webAppBuilder.ApplicationPoolPassword = Common.StringToSecureString(workingDefinition.WebAppOwnerPassword);
                   

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
                        string sitCollName = findAvailableSiteCollectionName(newApplication);
                        progressDetail("Creating site collection '" + sitCollName + "'");
                        SPSiteCollection siteCollections = newApplication.Sites;
                        SPSite site = siteCollections.Add("/sites/" + sitCollName, workingDefinition.WebAppOwnerLogin, workingDefinition.WebAppOwnerEmail);

                        SiteCollInfo siteCollInfo = new SiteCollInfo();
                        siteCollInfo.URL = site.Url;
                        workingSiteCollections.Add(siteCollInfo);
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
