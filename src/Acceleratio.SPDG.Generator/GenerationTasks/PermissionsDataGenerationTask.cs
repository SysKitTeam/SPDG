using System;
using System.Collections.Generic;
using System.Linq;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator.GenerationTasks
{
    public abstract class PermissionsDataGenerationTaskBase : DataGenerationTaskBase
    {
        int _permissionsPerObject = 1;
        List<SPDGUser> _siteSpUsers = null;
        List<SPDGGroup> _siteSpGroups = null;
        List<SPDGUser> _siteAdGroupSpUsers = null;

        List<SiteInfo> _allSitesToHaveUniquePermissions = new List<SiteInfo>();
        List<ListInfo> _allListsToHavehUniquePermissions = new List<ListInfo>();
        List<FolderInfo> _allFoldersToHaveUniquePermissions = new List<FolderInfo>();

        private bool _doSitePermissions;
        private bool _doListPermissions;
        bool _doListItemPermissions;
        private bool _dofolderPermissions;
        private int _totalSteps;
        public PermissionsDataGenerationTaskBase(IDataGenerationTaskOwner owner) : base(owner)
        {
            Init();
        }

        public override string Title
        {
            get { return "Creating Permissions"; }
        }


        private void Init()
        {
            var allSites = Owner.WorkingSiteCollections.SelectMany(x => x.Sites).ToList();
            var allLists = Owner.WorkingSiteCollections.SelectMany(x => x.Sites.SelectMany(y => y.Lists)).ToList();
            var allFolders = Owner.WorkingSiteCollections.SelectMany(x => x.Sites.SelectMany(y => y.Lists.SelectMany(z => z.Folders))).ToList();
            _permissionsPerObject = Owner.WorkingDefinition.PermissionsPerObject;

            foreach (var site in allSites)
            {
                if (SampleData.GetRandomNumber(1, 100) <= WorkingDefinition.PermissionsPercentOfSites)
                {
                    _allSitesToHaveUniquePermissions.Add(site);
                }
            }

            foreach (var list in allLists)
            {
                if (SampleData.GetRandomNumber(1, 100) <= WorkingDefinition.PermissionsPercentOfLists)
                {
                    _allListsToHavehUniquePermissions.Add(list);
                }
            }

            foreach (var folder in allFolders)
            {
                if (SampleData.GetRandomNumber(1, 100) <= WorkingDefinition.PermissionsPercentOfLists)
                {
                    _allFoldersToHaveUniquePermissions.Add(folder);
                }
            }

            var allItemsCount = allLists.Sum(x => x.ItemCount);

            _doSitePermissions = WorkingDefinition.PermissionsPercentOfSites > 0 && allSites.Count > 0;
            _doListPermissions = WorkingDefinition.PermissionsPercentOfLists > 0 && allLists.Count > 0;
            _doListItemPermissions = WorkingDefinition.PermissionsPercentOfListItems > 0 && allItemsCount > 0;
            _dofolderPermissions = WorkingDefinition.PermissionsPercentOfFolders > 0 && allFolders.Count > 0;
            bool stuffTodo = _doSitePermissions
                             || _doListPermissions
                             || _doListItemPermissions
                             || _dofolderPermissions;
            if (!stuffTodo)
            {
                return;
            }

            _totalSteps =
                //user ensure
                Owner.WorkingSiteCollections.Count
                //enum + eventual permissions change
                + allSites.Count + _allSitesToHaveUniquePermissions.Count;
            if (_doListItemPermissions || _doListPermissions || _dofolderPermissions)
            {
                //enum + eventual permissions change
                _totalSteps += allLists.Count + _allListsToHavehUniquePermissions.Count;
            }
            if (_dofolderPermissions)
            {
                //enum + eventual permissions change
                _totalSteps += allFolders.Count + _allFoldersToHaveUniquePermissions.Count;
            }
            if (_doListItemPermissions)
            {
                var withUnique = (allItemsCount * WorkingDefinition.PermissionsPercentOfListItems) / 100;
                //enum + eventual permissions change
                _totalSteps += allItemsCount + withUnique;
            }
        }

        public override int CalculateTotalSteps()
        {
            if (_totalSteps == 0)
            {
                Init();
            }

            return _totalSteps;
        }


        public override void Execute()
        {
            foreach (SiteCollInfo siteCollInfo in Owner.WorkingSiteCollections)
            {
                using (var siteColl = Owner.ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    _siteSpUsers = new List<SPDGUser>();
                    _siteAdGroupSpUsers = new List<SPDGUser>();
                    _siteSpGroups = new List<SPDGGroup>();

                    Owner.IncrementCurrentTaskProgress("Ensuring users and groups on '" + siteCollInfo.URL + "'");
                    EnsureUsersAndGroups(siteColl.RootWeb);

                    setSitePermissions(siteColl.RootWeb);
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (var web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            Owner.IncrementCurrentTaskProgress("Crawling site", 0);
                            if (_allSitesToHaveUniquePermissions.Contains(siteInfo))
                            {
                                setSitePermissions(web);
                            }

                            if (_doListPermissions || _doListItemPermissions || _dofolderPermissions)
                            {
                                foreach (ListInfo listInfo in siteInfo.Lists)
                                {
                                    Owner.IncrementCurrentTaskProgress("Crawling list " + web.Url + "/" + listInfo.Name, 0);
                                    if (_allListsToHavehUniquePermissions.Contains(listInfo))
                                    {
                                        setListPermissions(web, listInfo.Name);
                                    }

                                    if (_dofolderPermissions)
                                    {
                                        foreach (FolderInfo folderInfo in listInfo.Folders)
                                        {
                                            if (_allFoldersToHaveUniquePermissions.Contains(folderInfo))
                                            {
                                                setFolderPermissions(web, folderInfo.URL);
                                            }
                                            Owner.IncrementCurrentTaskProgress("Crawled folder" + web.Url + "/" + listInfo.Name);
                                        }
                                    }

                                    if (_doListItemPermissions)
                                    {
                                        setItemPermissions(web, listInfo.Name);
                                        Log.Write("Unique permissions added to items in list:" + listInfo.Name + " in site: " + web.Url);
                                    }
                                    Owner.IncrementCurrentTaskProgress("Crawled list " + web.Url + "/" + listInfo.Name);
                                }
                            }

                            Owner.IncrementCurrentTaskProgress("Crawled site " + web.Url);
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

            HashSet<string> createdGroups = new HashSet<string>();
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
            Owner.IncrementCurrentTaskProgress("Adding permissions to site " + web.Url + "'", 0);
            if (!web.HasUniqueRoleAssignments)
            {
                web.BreakRoleInheritance(false);
            }
            for (int i = 0; i < _permissionsPerObject; i++)
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
            Owner.IncrementCurrentTaskProgress("Adding permissions to list " + web.Url + "/" + listName, 0);
            var list = web.GetList(listName);
            if (!list.HasUniqueRoleAssignments)
            {
                list.BreakRoleInheritance(false);
            }

            for (int i = 0; i < _permissionsPerObject; i++)
            {
                setupNextRoleAssignment(web, list);
            }

            Owner.IncrementCurrentTaskProgress("Unique permissions added to list: " + listName + ", site: " + web.Url);
        }

        private void setFolderPermissions(SPDGWeb web, string folderURL)
        {
            Owner.IncrementCurrentTaskProgress("Adding permissions to folder: " + folderURL + " in site: " + web.Url, 0);
            SPDGFolder folder = web.GetFolder(folderURL);
            if (!folder.Item.HasUniqueRoleAssignments)
            {
                folder.Item.BreakRoleInheritance(false);
            }

            for (int i = 0; i < _permissionsPerObject; i++)
            {
                setupNextRoleAssignment(web, folder.Item);
            }

            Owner.IncrementCurrentTaskProgress("Unique permissions added to folder: " + folderURL + " in site: " + web.Url);
        }

        private void setItemPermissions(SPDGWeb web, string listName)
        {
            var list = web.GetList(listName);
            foreach (var item in list.Items)
            {
                if (SampleData.GetRandomNumber(1, 100) < WorkingDefinition.PermissionsPercentOfListItems)
                {
                    Owner.IncrementCurrentTaskProgress("Adding permissions for item/document '" + item.DisplayName + "' in list '" + list.Title, 0);
                    if (!item.HasUniqueRoleAssignments)
                    {
                        item.BreakRoleInheritance(false);
                    }

                    for (int i = 0; i < _permissionsPerObject; i++)
                    {
                        setupNextRoleAssignment(web, item);
                    }

                    Owner.IncrementCurrentTaskProgress("Unique permissions added for item/document '" + item.DisplayName + "' in list '" + list.Title);
                }
                else
                {
                    Owner.IncrementCurrentTaskProgress("");
                }
            }
        }


        private void setupNextRoleAssignment(SPDGWeb web, SPDGSecurableObject securableObject)
        {
            SPDGPrincipal principal = null;
            var rnd = SampleData.GetRandomNumber(0, 100);
            if (rnd < WorkingDefinition.PermissionsPercentForUsers)
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

            var roleAssignment = securableObject.GetRoleAssignmentByPrincipal(principal);

            if (roleAssignment == null || roleAssignment.RoleDefinitionBindings.All(x => x.IsGuestRole))
            {
                var availableRoledefinitions = web.RoleDefinitions.Where(x => !x.IsGuestRole).ToList();

                var selected = availableRoledefinitions[SampleData.GetRandomNumber(0, availableRoledefinitions.Count - 1)];
                securableObject.AddRoleAssignment(principal, new List<SPDGRoleDefinition> { selected });
            }
        }
    }
}