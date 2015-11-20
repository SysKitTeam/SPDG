using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator.Objects;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {

        int numSitesPermissions = 0;
        int numListsPermissions = 0;
        int numFoldersPermissions = 0;
        int numItemsPermissions = 0;
        int totalPermissions = 0;
        int countSitesPermissions = 0;
        int countListsPermissions = 0;
        int countFoldersPermissions = 0;
        int countItemsPermissions = 0;

        int numUserPermissions = 0;
        int numGroupPermissions = 0;
        int numADPermissions = 0;
        int countUserPermissions = 0;
        int countGroupPermissions = 0;
        int countADPermissions = 0;

        int permissionsPerObject = 1;

        string roleAssignemnetType = "user";
        List<SPDGUser> _siteSpUsers = null ;
        List<SPDGGroup> _siteSpGroups = null;
        List<SPDGUser> _siteAdGroupSpUsers = null;

        public void CreatePermissions()
        {
            
            //Per site collection
            permissionsPerObject = workingDefinition.PermissionsPerObject;
            numSitesPermissions = (int) Math.Floor(Convert.ToDecimal( workingDefinition.NumberOfSitesToCreate * workingDefinition.PermissionsPercentOfSites / 100 ));
            numListsPermissions = (int) Math.Floor(Convert.ToDecimal((workingDefinition.MaxNumberOfListsAndLibrariesPerSite * workingDefinition.NumberOfSitesToCreate) * workingDefinition.PermissionsPercentOfLists / 100));
            
            numFoldersPermissions = (int) Math.Floor(Convert.ToDecimal((workingDefinition.MaxNumberOfFoldersToGenerate * workingDefinition.MaxNumberOfListsAndLibrariesPerSite * workingDefinition.NumberOfSitesToCreate) * workingDefinition.PermissionsPercentOfFolders / 100));
            numItemsPermissions = (int) Math.Floor(Convert.ToDecimal((workingDefinition.MaxNumberofItemsToGenerate * workingDefinition.MaxNumberOfListsAndLibrariesPerSite * workingDefinition.NumberOfSitesToCreate) * workingDefinition.PermissionsPercentOfListItems / 100));
            totalPermissions = (numSitesPermissions + numListsPermissions + numFoldersPermissions + numItemsPermissions + totalPermissions) * permissionsPerObject;

            numUserPermissions = (int)Math.Floor(Convert.ToDecimal(totalPermissions * workingDefinition.PermissionsPercentForUsers / 100));
            numGroupPermissions = (int)Math.Floor(Convert.ToDecimal(totalPermissions * workingDefinition.PermissionsPercentForSPGroups / 100));
            numADPermissions = (int)Math.Floor(Convert.ToDecimal(totalPermissions * (100 - workingDefinition.PermissionsPercentForSPGroups - workingDefinition.PermissionsPercentForUsers) / 100));

            if (totalPermissions == 0)
            {
                return;
            }

            int totalProgress = CalculateOverallPermissionsForProgressReporting(totalPermissions);
            
            progressOverall("Creating Permissions", totalProgress);

            
            List<SiteInfo> allSitesWithUniquePermissions= new List<SiteInfo>();
            foreach (var site in workingSiteCollections.SelectMany(x => x.Sites))
            {
                if (SampleData.GetRandomNumber(0, 100) <= workingDefinition.PermissionsPercentOfSites)
                {
                    allSitesWithUniquePermissions.Add(site);
                }
            }

            List<ListInfo> allListsWithUniquePermissions = new List<ListInfo>();
            foreach (var list in workingSiteCollections.SelectMany(x => x.Sites.SelectMany(y=>y.Lists)))
            {
                if (SampleData.GetRandomNumber(0, 100) <= workingDefinition.PermissionsPercentOfLists)
                {
                    allListsWithUniquePermissions.Add(list);
                }
            }

            List<FolderInfo> allFoldersWithUniquePermissions = new List<FolderInfo>();
            foreach (var folder in workingSiteCollections.SelectMany(x => x.Sites.SelectMany(y => y.Lists.SelectMany(z=>z.Folders))))
            {
                if (SampleData.GetRandomNumber(0, 100) <= workingDefinition.PermissionsPercentOfLists)
                {
                    allFoldersWithUniquePermissions.Add(folder);
                }
            }

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (var siteColl = ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    _siteSpUsers = new List<SPDGUser>();
                    _siteAdGroupSpUsers = new List<SPDGUser>();
                    _siteSpGroups=new List<SPDGGroup>();
                    EnsureUsersAndGroups(siteColl.RootWeb);

                    _siteSpGroups = siteColl.RootWeb.SiteGroups.ToList();
                    _siteSpUsers = siteColl.RootWeb.SiteUsers.Where(x => !x.IsDomainGroup).ToList();
                    _siteAdGroupSpUsers = siteColl.RootWeb.SiteUsers.Where(x => x.IsDomainGroup).ToList();

                    countSitesPermissions = 0;
                    countListsPermissions = 0;
                    countFoldersPermissions = 0;
                    countItemsPermissions = 0;
                    countUserPermissions = 0;
                    countGroupPermissions = 0;
                    countADPermissions = 0;
                  
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (var web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            if (countSitesPermissions < numSitesPermissions || countListsPermissions < numListsPermissions || countFoldersPermissions < numFoldersPermissions || countItemsPermissions < numItemsPermissions)
                            {
                               
                            }

                            if(allSitesWithUniquePermissions.Contains(siteInfo))
                            {
                                if (!web.HasUniqueRoleAssignments)
                                {
                                    web.BreakRoleInheritance(true);
                                }
                                progressDetail("Adding permissions to site " + web.Url + "'");
                                setSitePermissions(web);
                                
                            }

                           
                            foreach (ListInfo listInfo in siteInfo.Lists)
                            {
                                if (allListsWithUniquePermissions.Contains(listInfo))
                                {
                                    progressDetail("Adding permissions to list " + web.Url + "/" + listInfo.Name);
                                    setListPermissions(web, listInfo.Name);                                    
                                }

                                foreach (FolderInfo folderInfo in listInfo.Folders)
                                {
                                    if (allFoldersWithUniquePermissions.Contains(folderInfo))
                                    {
                                         setFolderPermissions(web, folderInfo.URL);
                                        progressDetail("Adding permissions to folder " + web.Url + "/" + folderInfo.URL);
                                    }
                                }

                                if (workingDefinition.PermissionsPercentOfListItems > 0)
                                {
                                    setItemPermissions(web, listInfo.Name);
                                    Log.Write("Unique permissions added to items in list:" + listInfo.Name + " in site: " + web.Url);
                                }

                            }


                        }
                    }
                }
            }

        }

        protected abstract List<string> GetAvailableUsersInDirectory();
        protected abstract List<string> GetAvailableGroupsInDirectory();
        

        private void EnsureUsersAndGroups(SPDGWeb web)
        {
            /// ENSURE AD USERS TO SHAREPOINT
            var availableUsers = GetAvailableUsersInDirectory();
            int numOfUsers = availableUsers.Count > 20 ? 20 : availableUsers.Count;
            for (int i = 0; i < numOfUsers; i++)
            {
                string userName = availableUsers[SampleData.GetRandomNumber(0, availableUsers.Count)];

                if (userName == string.Empty) continue;

                try
                {
                    var user = web.EnsureUser(userName);                                
                    Log.Write("Ensured user:" + userName + " for site: " + web.Url);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                    Log.Write("Error adding user:" + userName);
                }
            }

            /// ENSURE AD GROUPS TO SHAREPOINT
            var adGrups = GetAvailableGroupsInDirectory();
            numOfUsers = adGrups.Count > 20 ? 20 : adGrups.Count;
            for (int i = 0; i < numOfUsers; i++)
            {
                string groupName = adGrups[SampleData.GetRandomNumber(0, adGrups.Count)];

                if (groupName == string.Empty) continue;

                try
                {
                    var user = web.EnsureUser(groupName);                                    
                    Log.Write("Ensured user:" + groupName + " for site: " + web.Url);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                    Log.Write("Error adding user:" + groupName);
                }
            }          

            /// CREATE SHAREPOINT GROUPS
            if (numGroupPermissions > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    string nameCandidate = SampleData.GetSampleValueRandom(SampleData.Accounts) + " Group";
                    try
                    {
                        web.AddSiteGroup(nameCandidate,                        
                        web.CurrentUser,
                        web.CurrentUser,
                        "SPDG generated group");
                    }
                    catch (Exception ex)
                    {
                        Errors.Log(ex);
                    }


                }                
            }
        }

        private void setSitePermissions(SPDGWeb web)
        {
            for (int i = 0; i < permissionsPerObject; i++)
            {
                try
                {
                    setupNextRoleAssignment(web, web);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                }
               
            }

            Log.Write("Unique permissions added to site: " + web.Url);
            countSitesPermissions++;
        }

        private void setListPermissions(SPDGWeb web, string listName)
        {
            var list = web.GetList(listName);
            if (!list.HasUniqueRoleAssignments)
            {
                list.BreakRoleInheritance(true);
            }

            for (int i = 0; i < permissionsPerObject; i++)
            {
                setupNextRoleAssignment(web, list);                
            }

            Log.Write("Unique permissions added to list: " + listName + ", site: " + web.Url);

            countListsPermissions++;
        }

        private void setFolderPermissions(SPDGWeb web, string folderURL)
        {
            SPDGFolder folder = web.GetFolder(folderURL);
            if (!folder.Item.HasUniqueRoleAssignments)
            {
                folder.Item.BreakRoleInheritance(true);
            }

            for (int i = 0; i < permissionsPerObject; i++)
            {
               setupNextRoleAssignment(web, folder.Item);
            }

            Log.Write("Unique permissions added to folder: " + folderURL + " in site: " + web.Url);
            countFoldersPermissions++;
        }

        private void setItemPermissions(SPDGWeb web, string listName)
        {
            var list = web.GetList(listName);
            foreach (var item in list.Items)
            {
                countItemsPermissions++;
                if (workingDefinition.PermissionsPercentOfListItems <= SampleData.GetRandomNumber(0,100))
                {
                    continue;
                }

                if (!item.HasUniqueRoleAssignments)
                {
                    item.BreakRoleInheritance(true);
                }

                for (int i = 0; i < permissionsPerObject; i++)
                {
                    setupNextRoleAssignment(web, item);                    
                }

                progressDetail("Adding permissions for item/document '" + item.DisplayName + "' in list '"+list.Title);
            }
        }


        private void setupNextRoleAssignment(SPDGWeb web, SPDGSecurableObject securableObject)
        {
            SPRoleAssignment roleAssignement = null;
            SPDGPrincipal principal = null;
            if( roleAssignemnetType == "user")
            {
                principal = _siteSpUsers[ SampleData.GetRandomNumber(0, _siteSpUsers.Count-1) ];                
                countUserPermissions++;

                if( countGroupPermissions < numGroupPermissions )
                {
                    roleAssignemnetType = "group";
                }
                else if( countADPermissions < numADPermissions)
                {
                    roleAssignemnetType = "ad";
                }
            }
            else if( roleAssignemnetType == "group")
            {
                principal = _siteSpGroups[SampleData.GetRandomNumber(0, _siteSpGroups.Count - 1)];
                
                countGroupPermissions++;

                if( countADPermissions < numADPermissions)
                {
                    roleAssignemnetType = "ad";
                }
                else if( countUserPermissions < numUserPermissions)
                {
                    roleAssignemnetType = "user";
                }
            }
            else if (roleAssignemnetType == "ad")
            {
                principal = _siteAdGroupSpUsers[SampleData.GetRandomNumber(0, _siteAdGroupSpUsers.Count - 1)];
             
                countADPermissions++;

                if( countUserPermissions < numUserPermissions)
                {
                    roleAssignemnetType = "user";
                }
                else if( countGroupPermissions < numGroupPermissions )
                {
                    roleAssignemnetType = "group";
                }
            }

            if (securableObject.GetRoleAssignmentByPrincipal(principal) == null)
            {
                var availableRoledefinitions = web.RoleDefinitions.Where(x=>!x.IsGuestRole).ToList();
                
                var selected = availableRoledefinitions[SampleData.GetRandomNumber(0, availableRoledefinitions.Count - 1)];
                securableObject.AddRoleAssignment(principal, new List<SPDGRoleDefinition> {selected});
            }
        }
    }
}
