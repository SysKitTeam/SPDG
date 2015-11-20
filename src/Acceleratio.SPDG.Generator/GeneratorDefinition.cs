using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator
{

    public abstract class GeneratorDefinitionBase
    {
        public abstract bool IsClientObjectModel { get; }
        public bool CredentialsOfCurrentUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string SiteCollOwnerLogin { get; set; }
        public string SiteCollOwnerEmail { get; set; }
        public string SiteTemplate { get; set; }
        public int NumberOfUsersToCreate { get; set; }
        public int NumberOfSecurityGroupsToCreate { get; set; }
        public int CreateNewSiteCollections { get; set; }
        public bool UseExistingSiteCollection { get; set; }
        public string SiteCollection { get; set; }
        public int NumberOfSitesToCreate { get; set; }
        public int MaxNumberOfLevelsForSites { get; set; }
        public int MaxNumberOfListsAndLibrariesPerSite { get; set; }
        public bool LibTypeList { get; set; }
        public bool LibTypeDocument { get; set; }
        public bool LibTypeTasks { get; set; }
        public bool LibTypeCalendar { get; set; }
        public bool CreateSomeFoldersInDocumentLibraries { get; set; }
        public int MaxNumberOfFoldersToGenerate { get; set; }
        public int MaxNumberOfNestedFolderLevelPerLibrary { get; set; }
        public bool CreateViews { get; set; }
        public int MaxNumberOfViewsPerList { get; set; }
        public bool CreateColumns { get; set; }
        public int MaxNumberOfColumnsPerList { get; set; }
        public bool PrefilListAndLibrariesWithItems { get; set; }
        public int MaxNumberofItemsToGenerate { get; set; }

        public int MaxNumberofDocumentLibraryItemsToGenerate { get; set; }
        public bool IncludeDocTypeDOCX { get; set; }
        public bool IncludeDocTypeXLSX { get; set; }
        public bool IncludeDocTypePDF { get; set; }
        public bool IncludeDocTypeImages { get; set; }
        public int MinDocumentSizeKB { get; set; }
        public int MaxDocumentSizeMB { get; set; }
        public bool CreateContentTypes { get; set; }
        public int MaxNumberOfContentTypesPerSiteCollection { get; set; }
        public bool AddSiteColumnsToContentTypes { get; set; }
        public int NumberSiteColumnsPerContentType { get; set; }
        public bool ContentTypesCanInheritFromOtherContentType { get; set; }
        public int PermissionsPercentOfSites { get; set; }
        public int PermissionsPercentOfLists { get; set; }
        public int PermissionsPercentOfFolders { get; set; }
        public int PermissionsPercentOfListItems { get; set; }
        public int PermissionsPercentForUsers { get; set; }
        public int PermissionsPercentForSPGroups { get; set; }
        public int PermissionsPerObject { get; set; }
        public bool GenerateUsersAndSecurityGroupsInDirectory { get; set; }

        public abstract void ValidateCredentials();

    }

    public class ClientGeneratorDefinition : GeneratorDefinitionBase
    {
        public string TenantName { get; set; }
        public override bool IsClientObjectModel { get { return true; } }

        public string AzureAdAccessToken { get; private set; }
        public override void ValidateCredentials()
        {
            AzureAdAccessToken = TokenHelper.GetUserAccessToken(Username, Password);
            
            //var onlineCredentials = new SharePointOnlineCredentials(Username, Utilities.Common.StringToSecureString(Password));
            //using (ClientContext context = new ClientContext(string.Format("https://{0}-admin.sharepoint.com", TenantName)))
            //{
            //    context.Credentials = onlineCredentials;
            //    var tenant = new Tenant(context);
            //    context.Load(tenant);
            //    context.ExecuteQuery();
            //}
        }
    }

    public class ServerGeneratorDefinition : GeneratorDefinitionBase
    {
        public override void ValidateCredentials()
        {
            
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
