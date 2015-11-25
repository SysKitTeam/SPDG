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
        

        int permissionsPerObject = 1;
        
        List<SPDGUser> _siteSpUsers = null ;
        List<SPDGGroup> _siteSpGroups = null;
        List<SPDGUser> _siteAdGroupSpUsers = null;

        public void CreatePermissions()
        {
            var allSites=workingSiteCollections.SelectMany(x => x.Sites).ToList();
            var allLists = workingSiteCollections.SelectMany(x => x.Sites.SelectMany(y => y.Lists)).ToList();
            var allFolders = workingSiteCollections.SelectMany(x => x.Sites.SelectMany(y => y.Lists.SelectMany(z => z.Folders))).ToList();
            var allItemsCount = allLists.Sum(x => x.ItemCount);

            bool doSitePermissions = workingDefinition.PermissionsPercentOfSites > 0 && allSites.Count > 0;
            bool doListPermissions = workingDefinition.PermissionsPercentOfLists > 0 && allLists.Count > 0;
            bool doListItemPermissions = workingDefinition.PermissionsPercentOfListItems > 0 && allItemsCount > 0;
            bool dofolderPermissions = workingDefinition.PermissionsPercentOfFolders > 0 && allFolders.Count > 0;
            bool stuffTodo = doSitePermissions
                             || doListPermissions
                             || doListItemPermissions
                             || dofolderPermissions;
            if (!stuffTodo)
            {
                return;
            }

            List<SiteInfo> allSitesWithUniquePermissions = new List<SiteInfo>();
            foreach (var site in allSites)
            {
                if (SampleData.GetRandomNumber(1, 100) <= workingDefinition.PermissionsPercentOfSites)
                {
                    allSitesWithUniquePermissions.Add(site);
                }
            }

            List<ListInfo> allListsWithUniquePermissions = new List<ListInfo>();
            foreach (var list in allLists)
            {
                if (SampleData.GetRandomNumber(1, 100) <= workingDefinition.PermissionsPercentOfLists)
                {
                    allListsWithUniquePermissions.Add(list);
                }
            }

            List<FolderInfo> allFoldersWithUniquePermissions = new List<FolderInfo>();
            foreach (var folder in allFolders)
            {
                if (SampleData.GetRandomNumber(1, 100) <= workingDefinition.PermissionsPercentOfLists)
                {
                    allFoldersWithUniquePermissions.Add(folder);
                }
            }

            int totalProgress =
                //user ensure
                workingSiteCollections.Count
                //enum + eventual permissions change
                + allSites.Count + allSitesWithUniquePermissions.Count;
            if (doListItemPermissions || doListPermissions || dofolderPermissions)
            {
                //enum + eventual permissions change
                totalProgress += allLists.Count + allListsWithUniquePermissions.Count;
            }
            if (dofolderPermissions)
            {
                //enum + eventual permissions change
                totalProgress += allFolders.Count+ allFoldersWithUniquePermissions.Count;
            }
            if (doListItemPermissions)
            {
                var withUnique=(allItemsCount*workingDefinition.PermissionsPercentOfListItems)/100;
                 //enum + eventual permissions change
                 totalProgress += allItemsCount+ withUnique;
            }                
            progressOverall("Creating Permissions", totalProgress);

            
           

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (var siteColl = ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    _siteSpUsers = new List<SPDGUser>();
                    _siteAdGroupSpUsers = new List<SPDGUser>();
                    _siteSpGroups=new List<SPDGGroup>();

                    progressDetail("Ensuring users and groups on '" + siteCollInfo.URL + "'");
                    EnsureUsersAndGroups(siteColl.RootWeb);

                 
                  
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (var web = siteColl.OpenWeb(siteInfo.ID))
                        {                          
                            progressDetail("Crawling site",0);
                            if(allSitesWithUniquePermissions.Contains(siteInfo))
                            {                             
                                setSitePermissions(web);
                                
                            }

                            if (doListPermissions || doListItemPermissions || dofolderPermissions)
                            {
                                foreach (ListInfo listInfo in siteInfo.Lists)
                                {
                                    progressDetail("Crawling list " + web.Url + "/" + listInfo.Name, 0);
                                    if (allListsWithUniquePermissions.Contains(listInfo))
                                    {                                        
                                        setListPermissions(web, listInfo.Name);
                                    }

                                    if (dofolderPermissions)
                                    {
                                        foreach (FolderInfo folderInfo in listInfo.Folders)
                                        {
                                            if (allFoldersWithUniquePermissions.Contains(folderInfo))
                                            {                                                
                                                setFolderPermissions(web, folderInfo.URL);
                                            }
                                            progressDetail("Crawled folder" + web.Url + "/" + listInfo.Name);
                                        }
                                    }

                                    if (doListItemPermissions)
                                    {
                                        setItemPermissions(web, listInfo.Name);
                                        Log.Write("Unique permissions added to items in list:" + listInfo.Name + " in site: " + web.Url);
                                    }
                                    progressDetail("Crawled list " + web.Url + "/" + listInfo.Name);
                                }
                            }

                            progressDetail("Crawled site "+ web.Url);
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

            HashSet<string> createdGroups=new HashSet<string>();
            /// CREATE SHAREPOINT GROUPS            
            if (WorkingDefinition.PermissionsPercentForSPGroups > 0)
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
                        createdGroups.Add(nameCandidate);
                        Log.Write("Created group:" + nameCandidate);
                    }
                    catch (Exception ex)
                    {
                        Errors.Log(ex);
                    }
                }                
            }
            _siteSpGroups = web.SiteGroups.ToList();
            _siteSpUsers = web.SiteUsers.Where(x => !x.IsDomainGroup).ToList();
            _siteAdGroupSpUsers = web.SiteUsers.Where(x => x.IsDomainGroup).ToList();

            if (createdGroups.Count > 0)
            {
                var candidates = _siteSpUsers.Union(_siteAdGroupSpUsers).ToList();
                var groups = web.SiteGroups.ToList();
                foreach (var @group in createdGroups)
                {
                    try
                    {
                        candidates.Shuffle();
                        var elementsToTake = Math.Min(candidates.Count, 10);
                        var users = candidates.Take(elementsToTake).ToList();
                        var grp = groups.First(x => x.Name == @group);
                        grp.AddUsers(users);
                        Log.Write(string.Format("added {0} users to group {1}:", users.Count, @group));
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
            progressDetail("Adding permissions to site " + web.Url + "'", 0);
            if (!web.HasUniqueRoleAssignments)
            {
                web.BreakRoleInheritance(true);
            }
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
        }

        private void setListPermissions(SPDGWeb web, string listName)
        {
            progressDetail("Adding permissions to list " + web.Url + "/" + listName, 0);
            var list = web.GetList(listName);
            if (!list.HasUniqueRoleAssignments)
            {
                list.BreakRoleInheritance(true);
            }

            for (int i = 0; i < permissionsPerObject; i++)
            {
                setupNextRoleAssignment(web, list);                
            }

            progressDetail("Unique permissions added to list: " + listName + ", site: " + web.Url);
        }

        private void setFolderPermissions(SPDGWeb web, string folderURL)
        {
            progressDetail("Adding permissions to folder: " + folderURL + " in site: " + web.Url, 0);
            SPDGFolder folder = web.GetFolder(folderURL);
            if (!folder.Item.HasUniqueRoleAssignments)
            {
                folder.Item.BreakRoleInheritance(true);
            }

            for (int i = 0; i < permissionsPerObject; i++)
            {
               setupNextRoleAssignment(web, folder.Item);
            }

            progressDetail("Unique permissions added to folder: " + folderURL + " in site: " + web.Url);            
        }

        private void setItemPermissions(SPDGWeb web, string listName)
        {
            var list = web.GetList(listName);
            foreach (var item in list.Items)
            {                
                if (SampleData.GetRandomNumber(1, 100) < workingDefinition.PermissionsPercentOfListItems)
                {
                    progressDetail("Adding permissions for item/document '" + item.DisplayName + "' in list '" + list.Title, 0);
                    if (!item.HasUniqueRoleAssignments)
                    {
                        item.BreakRoleInheritance(true);
                    }

                    for (int i = 0; i < permissionsPerObject; i++)
                    {
                        setupNextRoleAssignment(web, item);                    
                    }

                    progressDetail("Unique permissions added for item/document '" + item.DisplayName + "' in list '"+list.Title);
                }
                else
                {
                    progressDetail("");
                }
            }
        }


        private void setupNextRoleAssignment(SPDGWeb web, SPDGSecurableObject securableObject)
        {
            SPRoleAssignment roleAssignement = null;
            SPDGPrincipal principal = null;            
            var rnd = SampleData.GetRandomNumber(0, 100);
            if (rnd< WorkingDefinition.PermissionsPercentForUsers)
            {
                principal = _siteSpUsers[SampleData.GetRandomNumber(0, _siteSpUsers.Count - 1)];
            }
            else if (rnd < WorkingDefinition.PermissionsPercentForUsers + WorkingDefinition.PermissionsPercentForSPGroups)
            {
                principal = _siteSpGroups[SampleData.GetRandomNumber(0, _siteSpGroups.Count - 1)];
            }
            else
            {
                principal = _siteAdGroupSpUsers[SampleData.GetRandomNumber(0, _siteAdGroupSpUsers.Count - 1)];
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
