using System;
using Acceleratio.SPDG.Generator.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace Acceleratio.SPDG.Generator
{
    public class ServerGeneratorDefinition : GeneratorDefinitionBase
    {
        public override void ValidateCredentials()
        {
                       
            if (!this.CredentialsOfCurrentUser
                && (string.IsNullOrEmpty(this.Username) || string.IsNullOrEmpty(this.Password)))
            {
                throw  new CredentialValidationException("Credentials not supplied");
            }
            if (this.CredentialsOfCurrentUser)
            {
                return;
            }
            bool isFarmAdmin = false;            
            var accountName = this.Username;
            var domainName = Environment.UserDomainName;
            var parts = this.Username.Split('\\');
            if (parts.Length == 2)
            {
                accountName = parts[1];
                domainName = parts[0];
            }
            if (Common.ImpersonateValidUser(accountName, domainName, Password))
            {
                Common.UndoImpersonation();
            }
            else
            {
                throw new CredentialValidationException("Provided custom credentials are not valid!");                
            }

            //isFarmAdmin = SPFarm.Local.CurrentUserIsAdministrator();
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

            if (!isFarmAdmin)
            {
                throw new CredentialValidationException("The provided user is not a Farm Admin!");
            }
        }

        public override bool IsClientObjectModel { get { return false; } }
        public string SharePointURL { get; set; }
       
        //  public bool ConnectToSPOnPremise { get; set; }

        public string WebAppOwnerLogin { get; set; }
        public string WebAppOwnerPassword { get; set; }
        public string WebAppOwnerEmail { get; set; }
        public string DatabaseServer { get; set; }
        public string ADDomainName { get; set; }
        public string ADOrganizationalUnit { get; set; }
        public int CreateNewWebApplications { get; set; }
        public string UseExistingWebApplication { get; set; }
        public string UseExistingWebApplicationName { get; set; }
        public bool CreateOutOfTheBoxWorkflowsToList { get; set; }
        public bool AttachCustomWorkflowToList { get; set; }


        
    }
}