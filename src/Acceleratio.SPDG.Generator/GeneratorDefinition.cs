using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Acceleratio.SPDG.Generator
{

    public abstract class GeneratorDefinitionBase
    {
        private int _maxNumberOfViewsPerList;
        private int _maxNumberOfColumnsPerList;
        private int _maxNumberofItemsToGenerate;
        private int _maxNumberofDocumentLibraryItemsToGenerate;
        private int _maxNumberofItemsBigListToGenerate;
        private int _maxNumberOfContentTypesPerSiteCollection;
        private int _numberSiteColumnsPerContentType;
        private int _numberOfUsersToCreate;
        private int _numberOfSecurityGroupsToCreate;
        private int _maxNumberOfUsersInCreatedSecurityGroups;
        [XmlIgnore]
        public abstract bool IsClientObjectModel { get; }
        public bool CredentialsOfCurrentUser { get; set; }
        private string _userName;
        public string Username {
            get
            {
                if (CredentialsOfCurrentUser)
                {
                    return "";
                }
                return _userName;
            }
            set { _userName = value; }
        }

        private string _password;
        public string Password {
            get
            {
                if (CredentialsOfCurrentUser)
                {
                    return "";
                }
                return _password;
            }
            set { _password = value; }
        }
        public string Domain { get; set; }
        public string SiteCollOwnerLogin { get; set; }
        public string SiteCollOwnerEmail { get; set; }
        public string SiteTemplate { get; set; }
        
        public int CreateNewSiteCollections { get; set; }
        public bool UseExistingSiteCollection { get; set; }
        public string SiteCollection { get; set; }
        public int NumberOfSitesToCreate { get; set; }
        public int MaxNumberOfLevelsForSites { get; set; }
        public int MaxNumberOfListsAndLibrariesPerSite { get; set; }
        public int NumberOfBigListsPerSite { get; set; }
        public bool LibTypeList { get; set; }
        public bool LibTypeDocument { get; set; }
        public bool LibTypeTasks { get; set; }
        public bool LibTypeCalendar { get; set; }
        public bool CreateSomeFoldersInDocumentLibraries { get; set; }
        public int MaxNumberOfFoldersToGenerate { get; set; }
        public int MaxNumberOfNestedFolderLevelPerLibrary { get; set; }
        public bool CreateViews { get; set; }

        public int MaxNumberOfViewsPerList
        {
            get
            {
                if (CreateViews)
                {
                    return _maxNumberOfViewsPerList;
                }
                else
                {
                    return 0;
                }
            }
            set { _maxNumberOfViewsPerList = value; }
        }

        public bool CreateColumns { get; set; }

        public int MaxNumberOfColumnsPerList
        {
            get
            {
                if (CreateColumns)
                {
                    return _maxNumberOfColumnsPerList;
                }
                else
                {
                    return 0;
                }
            }
            set { _maxNumberOfColumnsPerList = value; }
        }

        public bool PrefilListAndLibrariesWithItems { get; set; }

        public int MaxNumberofItemsToGenerate
        {
            get
            {
                if (PrefilListAndLibrariesWithItems)
                {
                    return _maxNumberofItemsToGenerate;
                }
                else
                {
                    return 0;
                }
            }
            set { _maxNumberofItemsToGenerate = value; }
        }

        public int MaxNumberofDocumentLibraryItemsToGenerate
        {
            get
            {
                if (PrefilListAndLibrariesWithItems)
                {
                    return _maxNumberofDocumentLibraryItemsToGenerate;
                }
                else
                {
                    return 0;
                }
            }
            set { _maxNumberofDocumentLibraryItemsToGenerate = value; }
        }

        public int MaxNumberofItemsBigListToGenerate
        {
            get
            {
                if (PrefilListAndLibrariesWithItems)
                {
                    return _maxNumberofItemsBigListToGenerate;
                }
                else
                {
                    return 0;
                }
            }
            set { _maxNumberofItemsBigListToGenerate = value; }
        }

        public bool IncludeDocTypeDOCX { get; set; }
        public bool IncludeDocTypeXLSX { get; set; }
        public bool IncludeDocTypePDF { get; set; }
        public bool IncludeDocTypeImages { get; set; }
        public int MinDocumentSizeKB { get; set; }
        public int MaxDocumentSizeMB { get; set; }
        public bool CreateContentTypes { get; set; }

        public int MaxNumberOfContentTypesPerSiteCollection
        {
            get
            {
                if (CreateContentTypes)
                {
                    return _maxNumberOfContentTypesPerSiteCollection;
                }
                else
                {
                    return 0;
                }
            }
            set { _maxNumberOfContentTypesPerSiteCollection = value; }
        }

        public bool AddSiteColumnsToContentTypes { get; set; }

        public int NumberSiteColumnsPerContentType
        {
            get
            {
                if (AddSiteColumnsToContentTypes)
                {
                    return _numberSiteColumnsPerContentType;
                }
                else
                {
                    return 0;
                }
            }
            set { _numberSiteColumnsPerContentType = value; }
        }

        public bool ContentTypesCanInheritFromOtherContentType { get; set; }
        public int PermissionsPercentOfSites { get; set; }
        public int PermissionsPercentOfLists { get; set; }
        public int PermissionsPercentOfFolders { get; set; }
        public int PermissionsPercentOfListItems { get; set; }
        public int PermissionsPercentForUsers { get; set; }
        public int PermissionsPercentForSPGroups { get; set; }
        public int PermissionsPerObject { get; set; }
        public bool GenerateUsersAndSecurityGroupsInDirectory { get; set; }

        public int NumberOfUsersToCreate
        {
            get
            {
                if (GenerateUsersAndSecurityGroupsInDirectory)
                {
                    return _numberOfUsersToCreate;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                _numberOfUsersToCreate = value;
            }
        }

        public int NumberOfSecurityGroupsToCreate
        {
            get
            {
                if (GenerateUsersAndSecurityGroupsInDirectory)
                {
                    return _numberOfSecurityGroupsToCreate;
                }
                else
                {
                    return 0;
                }
            }
            set { _numberOfSecurityGroupsToCreate = value; }
        }

        public int MaxNumberOfUsersInCreatedSecurityGroups
        {
            get
            {
                if (GenerateUsersAndSecurityGroupsInDirectory && NumberOfSecurityGroupsToCreate > 0)
                {
                    return _maxNumberOfUsersInCreatedSecurityGroups;
                }
                else
                {
                    return 0;
                }
            }
            set { _maxNumberOfUsersInCreatedSecurityGroups = value; }
        }       

    }
}
