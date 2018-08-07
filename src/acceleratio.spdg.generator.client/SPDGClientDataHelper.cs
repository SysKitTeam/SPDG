using System;
using System.Collections.Generic;
using System.Linq;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using Group = Microsoft.Azure.ActiveDirectory.GraphClient.Group;
using User = Microsoft.Azure.ActiveDirectory.GraphClient.User;

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
                    context.Credentials = new SharePointOnlineCredentials(_generatorDefinition.Username, Utils.StringToSecureString(_generatorDefinition.Password));
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

        public override void ValidateCredentials()
        {
            try
            {
                var url = string.Format("https://{0}-admin.sharepoint.com", _generatorDefinition.TenantName);
                using (ClientContext context = new ClientContext(url))
                {
                    context.Credentials = new SharePointOnlineCredentials(_generatorDefinition.Username, Utils.StringToSecureString(_generatorDefinition.Password));
                    var officeTenant = new Microsoft.Online.SharePoint.TenantAdministration.Tenant(context);
                    context.Load(officeTenant);
                    context.ExecuteQuery();
                }
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
                throw new CredentialValidationException(ex.Message);
            }            
        }


       public void CreateNewSiteCollection(string title, string siteCollectionUrl, string owner)
        {
            bool isSiteCollectionExists = false;

            var url = string.Format("https://{0}-admin.sharepoint.com", _generatorDefinition.TenantName);
            

            using (ClientContext siteContext = new ClientContext(siteCollectionUrl))
            {
                try
                {
                    var site = siteContext.Site;
                    siteContext.Load(site);
                    siteContext.ExecuteQuery();
                    isSiteCollectionExists = true;
                }
                catch (Exception ex)
                {
                    isSiteCollectionExists = false;
                }
            }

            if (!isSiteCollectionExists)
            {
                using (ClientContext context = new ClientContext(url))
                {


                    context.Credentials = new SharePointOnlineCredentials(_generatorDefinition.Username,
                        Utils.StringToSecureString(_generatorDefinition.Password));
                    var officeTenant = new Microsoft.Online.SharePoint.TenantAdministration.Tenant(context);
                    var newSiteProperties = new SiteCreationProperties()
                    {
                        Url = siteCollectionUrl,
                        Owner = _generatorDefinition.SiteCollOwnerLogin,
                        Template = "STS#0"
                    };
                    var spo = officeTenant.CreateSite(newSiteProperties);
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

        private string GetToken()
        {
            return TokenHelper.GetUserAccessToken(_generatorDefinition.Username, _generatorDefinition.Password);
        }

        private ActiveDirectoryClient _adClient = null;
        public ActiveDirectoryClient GetADClient()
        {
            if (_adClient == null)
            {
                Uri servicePointUri = new Uri("https://graph.windows.net");

                var serviceRoot = new Uri(servicePointUri, string.Format("{0}.onmicrosoft.com", _generatorDefinition.TenantName));

                _adClient = new ActiveDirectoryClient(serviceRoot,
                    async () => GetToken());
            }
            return _adClient;
        }

        List<User> _allUsers;
        List<Group> _allGroups;
        public List<Group> GetAvailableGroupObjectsInDirectory()
        {
            if (_allGroups == null)
            {
                var adClient = GetADClient();
                _allGroups = new List<Group>();

                IPagedCollection<IGroup> result = null;
                do
                {
                    if (result == null)
                    {
                        result = adClient.Groups.ExecuteAsync().Result;
                    }
                    else
                    {
                        result = result.GetNextPageAsync().Result;
                    }
                    foreach (Group group in result.CurrentPage)
                    {
                        _allGroups.Add(group);
                    }
                } while (result.MorePagesAvailable);
            }
            return _allGroups;
        }

        public List<User> GetAvailableUserObjectsInDirectory()
        {
            if (_allUsers == null)
            {
                var adClient = GetADClient();
                _allUsers = new List<User>();

                IPagedCollection<IUser> result = null;

                do
                {
                    if (result == null)
                    {
                        result = adClient.Users.ExecuteAsync().Result;
                    }
                    else
                    {
                        result = result.GetNextPageAsync().Result;
                    }
                    foreach (User user in result.CurrentPage)
                    {
                        _allUsers.Add(user);
                    }
                } while (result.MorePagesAvailable);
            }
            return _allUsers;
        }


    }
}
