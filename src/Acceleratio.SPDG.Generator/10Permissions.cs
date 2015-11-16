using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        List<SPUser> allUsers = null ;
        SPGroupCollection allGroups = null;
        List<SPUser> adGroupUsers = null;

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

            int totalProgress = totalPermissions;
            if (workingDefinition.CreateNewSiteCollections > 0)
            {
                totalProgress = totalProgress * workingDefinition.CreateNewSiteCollections;
            }

            //TODO:rf vratiti ovo nekako
            //if (workingDefinition.CreateNewWebApplications > 0)
            //{
            //    totalProgress = totalProgress * workingDefinition.CreateNewWebApplications;
            //}

            progressOverall("Creating Permissions", totalProgress);

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    countSitesPermissions = 0;
                    countListsPermissions = 0;
                    countFoldersPermissions = 0;
                    countItemsPermissions = 0;
                    countUserPermissions = 0;
                    countGroupPermissions = 0;
                    countADPermissions = 0;

                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            if (countSitesPermissions < numSitesPermissions || countListsPermissions < numListsPermissions || countFoldersPermissions < numFoldersPermissions || countItemsPermissions < numItemsPermissions)
                            {
                                allUsers = new List<SPUser>();
                                adGroupUsers = new List<SPUser>();
                                EnsureUsersAndGroups(web);
                            }

                            if( countSitesPermissions < numSitesPermissions)
                            {
                                setSitePermissions(web);
                                progressDetail("Adding permissions to site " + web.Url + "'");
                            }

                            foreach(ListInfo listInfo in siteInfo.Lists)
                            {
                                if (countListsPermissions < numListsPermissions)
                                {
                                    setListPermissions(web, listInfo.Name);
                                    progressDetail("Adding permissions to list " + web.Url + "/" + listInfo.Name);
                                }

                                foreach(FolderInfo folderInfo in listInfo.Folders)
                                {
                                    if( countFoldersPermissions < numFoldersPermissions)
                                    {
                                        setFolderPermissions(web, folderInfo.URL);
                                        progressDetail("Adding permissions to folder " + web.Url + "/" + folderInfo.URL);
                                    }
                                }

                                if( countItemsPermissions < numItemsPermissions)
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

        private void EnsureUsersAndGroups(SPWeb web)
        {
            

            if (!web.HasUniqueRoleAssignments)
            {
                web.BreakRoleInheritance(true);
            }

            /// ENSURE AD USERS TO SHAREPOINT
            string[] adUsers = AD.GetUsersFromAD().Split(';');
            int numOfUsers = adUsers.Length > 20 ? 20 : adUsers.Length;
            for(int i=0; i<numOfUsers; i++)
            {
                string userName = adUsers[ SampleData.GetRandomNumber(0, adUsers.Length) ];

                if (userName == string.Empty) continue;

                try
                {
                    SPUser user = web.EnsureUser(userName);
                    web.AllUsers.Add(user.LoginName, user.Email, user.Name, null);
                    Log.Write("Ensured user:" + userName  + " for site: " + web.Url);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                    Log.Write("Error adding user:" + userName);
                }
            }

            /// ENSURE AD GROUPS TO SHAREPOINT
            string[] adGrups = AD.GetGroupsFromAD().Split(';');
            numOfUsers = adGrups.Length > 20 ? 20 : adUsers.Length;
            for (int i = 0; i < numOfUsers; i++)
            {
                string groupName = adGrups[SampleData.GetRandomNumber(0, adGrups.Length)];

                if (groupName == string.Empty) continue;

                try
                {
                    SPUser user = web.EnsureUser(groupName);
                    web.AllUsers.Add(groupName, "", "", groupName);
                    //web.AllUsers.Add(user.LoginName, user.Email, user.Name, null);
                    Log.Write("Ensured user:" + groupName + " for site: " + web.Url);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                    Log.Write("Error adding user:" + groupName);
                }
            }

            foreach(SPUser user in web.AllUsers)
            {
                if(user.IsDomainGroup)
                {
                    adGroupUsers.Add(user);
                }
                else
                {
                    allUsers.Add(user);
                }
            }

            /// CREATE SHAREPOINT GROUPS
            if( numGroupPermissions > 0 )
            {
                for(int i=0; i<10; i++)
                {
                    string nameCandidate = SampleData.GetSampleValueRandom(SampleData.Accounts) + " Group";
                    try
                    {
                        web.SiteGroups.Add(
                        nameCandidate,
                        web.Author,
                        web.Author,
                        "SPDG generated group");
                    }
                    catch(Exception ex)
                    {
                        Errors.Log(ex);
                    }
                    

                }

                allGroups = web.SiteGroups;
            }
            
        }

        private void setSitePermissions(SPWeb web)
        {
            for( int i=0; i < permissionsPerObject; i++)
            {
                SPRoleAssignment roleAssignment = getNextRoleAssignment(web);
                web.RoleAssignments.Add(roleAssignment);
                web.Update();
            }

            Log.Write("Unique permissions added to site: " + web.Url);
            countSitesPermissions++;
        }

        private void setListPermissions(SPWeb web, string listName)
        {
            SPList list = web.Lists[listName];
            if (!list.HasUniqueRoleAssignments)
            {
                list.BreakRoleInheritance(true);
            }

            for (int i = 0; i < permissionsPerObject; i++)
            {
                SPRoleAssignment roleAssignment = getNextRoleAssignment(web);
                list.RoleAssignments.Add(roleAssignment);
                list.Update();
            }

            Log.Write("Unique permissions added to list: " + listName + ", site: " + web.Url);

            countListsPermissions++;
        }

        private void setFolderPermissions(SPWeb web, string folderURL)
        {
            SPFolder folder = web.GetFolder(folderURL);
            if (!folder.Item.HasUniqueRoleAssignments)
            {
                folder.Item.BreakRoleInheritance(true);
            }

            for (int i = 0; i < permissionsPerObject; i++)
            {
                SPRoleAssignment roleAssignment = getNextRoleAssignment(web);

                folder.Item.RoleAssignments.Add(roleAssignment);
                folder.Item.Update();
            }

            Log.Write("Unique permissions added to folder: " + folderURL + " in site: " + web.Url);
            countFoldersPermissions++;
        }

        private void setItemPermissions(SPWeb web, string listName)
        {
            SPList list = web.Lists[listName];
            SPListItemCollection items = list.Items;

            foreach(SPListItem item in items)
            {
                countItemsPermissions++;
                if( countItemsPermissions >= numItemsPermissions)
                {
                    break;
                }

                if (!item.HasUniqueRoleAssignments)
                {
                    item.BreakRoleInheritance(true);
                }

                for (int i = 0; i < permissionsPerObject; i++)
                {
                    SPRoleAssignment roleAssignment = getNextRoleAssignment(web);
                    item.RoleAssignments.Add(roleAssignment);
                    item.Update();
                }

                progressDetail("Adding permissions for item/document '" + item.Url + "'");
            }  
        }


        private SPRoleAssignment getNextRoleAssignment(SPWeb web)
        {
            SPRoleAssignment roleAssignement = null;
            
            if( roleAssignemnetType == "user")
            {
                SPUser user = allUsers[ SampleData.GetRandomNumber(0, allUsers.Count-1) ];
                roleAssignement = new SPRoleAssignment(user);
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
                SPGroup spGroup = allGroups[SampleData.GetRandomNumber(0, allGroups.Count - 1)];
                roleAssignement = new SPRoleAssignment(spGroup);
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
                SPUser adUser = adGroupUsers[SampleData.GetRandomNumber(0, adGroupUsers.Count - 1)];
                roleAssignement = new SPRoleAssignment(adUser);

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

            SPRoleDefinition roleDefinition = web.RoleDefinitions.GetByType(SPRoleType.Contributor);
            roleAssignement.RoleDefinitionBindings.Add(roleDefinition);

            return roleAssignement;
            
        }

    }
}
