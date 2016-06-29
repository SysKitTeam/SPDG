using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Client
{
    public class SPDGClientDataHelper : SPDGDataHelper
    {
        private readonly ClientGeneratorDefinition _generatorDefinition;

        public SPDGClientDataHelper(ClientGeneratorDefinition generatorDefinition)
        {
            _generatorDefinition = generatorDefinition;
        }

        public override IEnumerable<SPDGWebApplication> GetWebApplications()
        {
            return new List<SPDGWebApplication>();
        }

        public override IEnumerable<string> GetAllSiteCollections(Guid webApplicationId)
        {
            try
            {
                var url = string.Format("https://{0}-admin.sharepoint.com", _generatorDefinition.TenantName);
                using (ClientContext context = new ClientContext(url))
                {
                    context.Credentials = new SharePointOnlineCredentials(_generatorDefinition.Username, Utilities.Common.StringToSecureString(_generatorDefinition.Password));
                    var officeTenant = new Microsoft.Online.SharePoint.TenantAdministration.Tenant(context);
                    var siteProperties = officeTenant.GetSiteProperties(0, true);
                    context.Load(siteProperties);
                    context.ExecuteQuery();

                    return siteProperties.Select(x => x.Url).ToList();
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }

            return new List<string>();
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
                                
                while (!spo.IsComplete)
                {                    
                    System.Threading.Thread.Sleep(10000);
                    spo.RefreshLoad();
                    context.ExecuteQuery();
                }                                                
            }
        }

        
    }
}
