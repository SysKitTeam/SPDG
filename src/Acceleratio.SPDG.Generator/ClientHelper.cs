using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator
{
    public class ClientHelper
    {
        private readonly ClientGeneratorDefinition _generatorDefinition;

        public ClientHelper(ClientGeneratorDefinition generatorDefinition)
        {
            _generatorDefinition = generatorDefinition;
        }

        public List<string> GetAllSiteCollections()
        {
            var url = string.Format("https://{0}-admin.sharepoint.com", _generatorDefinition.TenantName);
            using (ClientContext context=new ClientContext(url))
            {
                context.Credentials = new SharePointOnlineCredentials(_generatorDefinition.Username, Utilities.Common.StringToSecureString(_generatorDefinition.Password));
                var officeTenant = new Microsoft.Online.SharePoint.TenantAdministration.Tenant(context);
                var siteProperties = officeTenant.GetSiteProperties(0, true);
                context.Load(siteProperties);                
                context.ExecuteQuery();
                
                return siteProperties.Select(x => x.Url).ToList();
            }
        }

        public void CreateNewSiteCollection(string title, string name, string owner)
        {
            var url = string.Format("https://{0}-admin.sharepoint.com", _generatorDefinition.TenantName);
            using (ClientContext context = new ClientContext(url))
            {
                context.Credentials = new SharePointOnlineCredentials(_generatorDefinition.Username, Utilities.Common.StringToSecureString(_generatorDefinition.Password));
                var officeTenant = new Microsoft.Online.SharePoint.TenantAdministration.Tenant(context);
                var newSiteProperties = new SiteCreationProperties()
                {
                    Url = string.Format("https://{0}.sharepoint.com/sites/{1}", _generatorDefinition.TenantName, name),
                    Owner = _generatorDefinition.SiteCollOwnerLogin,
                    Template = "STS#0",
                 
                };
                var spo= officeTenant.CreateSite(newSiteProperties);
                context.Load(spo, i => i.IsComplete);
                context.ExecuteQuery();

                //Check if provisioning of the SiteCollection is complete.
                while (!spo.IsComplete)
                {
                    //Wait for 30 seconds and then try again
                    System.Threading.Thread.Sleep(10000);
                    spo.RefreshLoad();
                    context.ExecuteQuery();
                }                                                
            }
        }
    }
}
