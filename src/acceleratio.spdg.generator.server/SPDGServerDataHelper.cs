using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace Acceleratio.SPDG.Generator.Server
{
    class SPDGServerDataHelper : SPDGDataHelper
    {
        private readonly GeneratorDefinitionBase _generatorDefinition;

        public SPDGServerDataHelper(GeneratorDefinitionBase definition)
        {
            _generatorDefinition = definition;
        }

        private void runAs(Action action)
        {
            if (!_generatorDefinition.CredentialsOfCurrentUser)
            {
                var parts = _generatorDefinition.Username.Split('\\');

                if (Utils.ImpersonateValidUser(parts[1], parts[0], _generatorDefinition.Password))
                {
                    try
                    {
                        action();
                    }
                    finally
                    {
                        Utils.UndoImpersonation();
                    }
                    
                }
            }
            else
            {
                action();
            }
        }

        public override IEnumerable<SPDGWebApplication> GetWebApplications()
        {
            List<SPDGWebApplication> webApps=new List<SPDGWebApplication>();
            runAs(() =>
            {
                SPWebService spWebService = SPWebService.ContentService;
                SPWebApplicationCollection webAppColl = spWebService.WebApplications;
                webApps = webAppColl.Select(x => new SPDGWebApplication() { Id = x.Id, Name = x.Name }).ToList();
            });           
            return webApps;            
        }

        public override IEnumerable<string> GetAllSiteCollections(Guid webApplicationId)
        {
            List<string> siteCollections=new List<string>();
            runAs(() =>
            {
                SPWebService spWebService = SPWebService.ContentService;
                SPWebApplication webApp = spWebService.WebApplications.First(a => a.Id == webApplicationId);

                foreach (SPSite siteColl in webApp.Sites)
                {
                    siteCollections.Add(siteColl.Url);

                }
            });
           
            return siteCollections;
        }

        public override void ValidateCredentials()
        {

            if (!_generatorDefinition.CredentialsOfCurrentUser
                && (string.IsNullOrEmpty(_generatorDefinition.Username) || string.IsNullOrEmpty(_generatorDefinition.Password)))
            {
                throw new CredentialValidationException("Credentials not supplied");
            }
            if (_generatorDefinition.CredentialsOfCurrentUser)
            {
                return;
            }
            bool isFarmAdmin = false;
            var accountName = _generatorDefinition.Username;
            var domainName = Environment.UserDomainName;
            var parts = _generatorDefinition.Username.Split('\\');
            if (parts.Length == 2)
            {
                accountName = parts[1];
                domainName = parts[0];
            }
            if (Utils.ImpersonateValidUser(accountName, domainName, _generatorDefinition.Password))
            {
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        SPGroup adminGroup = SPAdministrationWebApplication.Local.Sites[0].AllWebs[0].SiteGroups["Farm Administrators"];
                        foreach (SPUser user in adminGroup.Users)
                        {
                            if (user.LoginName.ToLower() == domainName + "\\" + accountName)
                            {
                                isFarmAdmin = true;
                                break;
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    isFarmAdmin = false;
                    Errors.Log(ex);
                }
                finally
                {
                    Utils.UndoImpersonation();
                }

                if (!isFarmAdmin)
                {
                    throw new CredentialValidationException("The provided user is not a Farm Admin!");
                }
            }
            else
            {
                throw new CredentialValidationException("Provided custom credentials are not valid!");
            }
        }
    }
}
