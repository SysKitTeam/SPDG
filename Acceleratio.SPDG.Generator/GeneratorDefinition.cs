using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator
{
    public class GeneratorDefinition
    {
        public string SharePointURL { get; set; }
        public bool CredentialsOfCurrentUser { get; set; }
        public bool ConnectToSPOnPremise { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public bool GenerateUsersAndSecurityGroupsActiveInDirectory { get; set; }
        public string OrganizationalUnit { get; set; }
        public int NumberOfUsersToCreate { get; set; }
        public int NumberOfSecurityGroupsToCreate { get; set; }
        public int CreateNewWebApplications { get; set; }
        public string UseExistingWebApplication { get; set; }
        public int CreateNewSiteCollections { get; set; }
        public bool UseExistingSiteCollection { get; set; }
        public string SiteCollection { get; set; }
        public int NumberOfSitesToCreate { get; set; }
        public int MaxNumberOfLevelsForSites { get; set; }
        public int MaxNumberOfListsAndLibrariesPerSite { get; set; }
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
        public bool CreateOutOfTheBoxWorkflowsToList { get; set; }
        public bool AttachCustomWorkflowToList { get; set; }
        public bool AssignPermissions { get; set; }
        public bool CreateUniquePermissionsForPercentOfSites { get; set; }
        public bool CreateUniquePermissionsForPercentOfLists { get; set; }
        public bool CreateUniquePermissionsForPercentOfLibraries{ get; set; }
        public bool CreateUniquePermissionsForPercentOfListItems { get; set; }
        public int PermissionsPercentOfSites { get; set; }
        public int PermissionsPercentOfLists { get; set; }
        public int PermissionsPercentOfLibraries { get; set; }
        public int PermissionsPercentOfListItems { get; set; }
        public int AssignPercentPermissionsDirectlyToUsers { get; set; }
        public int CreateSPGroupsPercent { get; set; }
    }
}
