using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator.Utilities;
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

        public override IEnumerable<SPDGWebApplication> GetWebApplications()
        {
            SPWebService spWebService = SPWebService.ContentService;
            SPWebApplicationCollection webAppColl = spWebService.WebApplications;
            

            return webAppColl.Select(x => new SPDGWebApplication() {Id = x.Id, Name = x.Name});

        }

        public override IEnumerable<string> GetAllSiteCollections(Guid webApplicationId)
        {
            
            SPWebService spWebService = SPWebService.ContentService;
            SPWebApplication webApp = spWebService.WebApplications.First(a => a.Id == webApplicationId);

            foreach (SPSite siteColl in webApp.Sites)
            {
                yield return siteColl.Url;                
            }
            
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
            if (Common.ImpersonateValidUser(accountName, domainName, _generatorDefinition.Password))
            {
                Common.UndoImpersonation();
            }
            else
            {
                throw new CredentialValidationException("Provided custom credentials are not valid!");
            }

            //isFarmAdmin = SPFarm.Local.CurrentUserIsAdministrator();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
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

            if (!isFarmAdmin)
            {
                throw new CredentialValidationException("The provided user is not a Farm Admin!");
            }
        }
    }
}
