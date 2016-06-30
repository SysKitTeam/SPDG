using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;
using Acceleratio.SPDG.Generator.Utilities;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using Microsoft.SharePoint.Client;
using Group = Microsoft.Azure.ActiveDirectory.GraphClient.Group;
using User = Microsoft.Azure.ActiveDirectory.GraphClient.User;

namespace Acceleratio.SPDG.Generator.Client
{
    public partial class ClientDataGenerator : DataGenerator
    {
        List<User> _allUsers = null;
        List<Group> _allGroups = null;
        protected  List<User> GetAvailableUserObjectsInDirectory()
        {
            if (_allUsers == null)
            {
                var adClient = GetADClient();
                _allUsers = new List<User>();

                IPagedCollection<IUser> result = null;
                
                do
                {
                    if (result == null)
                    {
                        result = adClient.Users.ExecuteAsync().Result;
                    }
                    else
                    {
                        result = result.GetNextPageAsync().Result;
                    }
                    foreach (User user in result.CurrentPage)
                    {
                        _allUsers.Add(user);
                    }
                } while (result.MorePagesAvailable);
            }
            return _allUsers;
        }

        protected override List<string> GetAvailableUsersInDirectory()
        {
            return GetAvailableUserObjectsInDirectory().Select(x => x.UserPrincipalName).ToList();
        }

        protected List<Group> GetAvailableGroupObjectsInDirectory()
        {
            if (_allGroups == null)
            {
                var adClient = GetADClient();
                _allGroups = new List<Group>();

                IPagedCollection<IGroup> result = null;
                do
                {
                    if (result == null)
                    {
                        result = adClient.Groups.ExecuteAsync().Result;
                    }
                    else
                    {
                        result = result.GetNextPageAsync().Result;
                    }
                    foreach (Group group in result.CurrentPage)
                    {                 
                        _allGroups.Add(group);
                    }
                } while (result.MorePagesAvailable);
            }
            return _allGroups;
        }

        protected override List<string> GetAvailableGroupsInDirectory()
        {
            return GetAvailableGroupObjectsInDirectory().Select(x => x.DisplayName).ToList();
        }

        protected new ClientGeneratorDefinition WorkingDefinition
        {
            get { return (ClientGeneratorDefinition)base.WorkingDefinition; }
        }

        public ClientDataGenerator(ClientGeneratorDefinition definition) : base(definition)
        {
        }

        protected override SPDGObjectsFactory CreateObjectsFactory()
        {
            return new SPDGClientObjectsFactory(new SharePointOnlineCredentials(WorkingDefinition.Username, Utilities.Common.StringToSecureString(WorkingDefinition.Password)));
        }

        private string GetToken()
        {
            return TokenHelper.GetUserAccessToken(WorkingDefinition.Username, WorkingDefinition.Password);            
        }

        private ActiveDirectoryClient _adClient = null;
        private ActiveDirectoryClient GetADClient()
        {
            if (_adClient == null)
            {
                Uri servicePointUri = new Uri("https://graph.windows.net");

                var serviceRoot = new Uri(servicePointUri, string.Format("{0}.onmicrosoft.com", WorkingDefinition.TenantName));

                _adClient = new ActiveDirectoryClient(serviceRoot,
                    async () => GetToken());
            }
            return _adClient;           
        }

        protected override void CreateUsersAndGroups()
        {
            if (WorkingDefinition.NumberOfUsersToCreate == 0 && WorkingDefinition.NumberOfSecurityGroupsToCreate == 0)
            {
                return;
            }

            var progressTotal = WorkingDefinition.NumberOfUsersToCreate + WorkingDefinition.NumberOfSecurityGroupsToCreate;
            if(WorkingDefinition.MaxNumberOfUsersInCreatedSecurityGroups > 0)
            {
                progressTotal += WorkingDefinition.NumberOfSecurityGroupsToCreate;
            }
            updateProgressOverall("Creating Directory Users And Groups",
                progressTotal);
            var client = GetADClient();
            
            List<ITenantDetail> tenantsList = client.TenantDetails
                    //.Where(tenantDetail => tenantDetail.ObjectId.Equals())
                    .ExecuteAsync().Result.CurrentPage.ToList();
            ITenantDetail tenant = tenantsList.First();

            var defaultDomain = tenant.VerifiedDomains.First(x => x.@default.HasValue && x.@default.Value);
            if (WorkingDefinition.NumberOfUsersToCreate > 0)
            {
                try
                {
                    updateProgressDetail("Creating Active Directory users.",0);

                    int batchcounter = 0;
                    HashSet<Tuple<string,string>> usedNames=new HashSet<Tuple<string, string>>();
                    for (int i = 0; i < WorkingDefinition.NumberOfUsersToCreate; i++)
                    {
                        try
                        {
                            var firstName = SampleData.GetSampleValueRandom(SampleData.FirstNames);
                            var lastName = SampleData.GetSampleValueRandom(SampleData.LastNames);
                            while (usedNames.Contains(new Tuple<string, string>(firstName, lastName)))
                            {
                                firstName = SampleData.GetSampleValueRandom(SampleData.FirstNames);
                                lastName = SampleData.GetSampleValueRandom(SampleData.LastNames);
                            }
                            usedNames.Add(new Tuple<string, string>(firstName, lastName));


                            var userPrincipal = new User();
                            userPrincipal.Surname = lastName;
                            userPrincipal.AccountEnabled = true;
                            
                            userPrincipal.GivenName = firstName;
                            userPrincipal.MailNickname = userPrincipal.GivenName.ToLower() + "." + userPrincipal.Surname.ToLower();
                            userPrincipal.UserPrincipalName = userPrincipal.MailNickname + "@"+ defaultDomain.Name;
                            
                            //userPrincipal.Name = userPrincipal.GivenName + " " + userPrincipal.Surname;                            
                            userPrincipal.DisplayName = userPrincipal.GivenName + " " + userPrincipal.Surname;
                            userPrincipal.UsageLocation = "US";                        
                            userPrincipal.PasswordProfile = new PasswordProfile
                            {
                                Password = "pass@word1",
                                ForceChangePasswordNextLogin = false,
                                
                            };
                            client.Users.AddUserAsync(userPrincipal,true).Wait();
                            batchcounter++;
                            if (batchcounter >= 50)
                            {
                               
                                client.Context.SaveChangesAsync().Wait();
                                updateProgressDetail(string.Format("Created {0}/{1} users", i+1, WorkingDefinition.NumberOfUsersToCreate), batchcounter);
                                batchcounter = 0;
                            }

                        }
                        catch (Exception ex)
                        {
                            Errors.Log(ex);
                        }
                    }
                    if (batchcounter > 0)
                    {
                       client.Context.SaveChangesAsync().Wait();
                       updateProgressDetail(string.Format("Created {0} users", WorkingDefinition.NumberOfUsersToCreate), batchcounter);
                    }                   
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                }
            }            
           
            if (WorkingDefinition.NumberOfSecurityGroupsToCreate > 0)
            {
                try
                {
                    updateProgressDetail("Creating Active Directory groups.",0);
                    HashSet<string> usedGroupNames=new HashSet<string>();
                    int batchCounter = 0;
                    for (int i = 0; i < WorkingDefinition.NumberOfSecurityGroupsToCreate; i++)
                    {
                        try
                        {
                            var displayName = SampleData.GetSampleValueRandom(SampleData.Accounts);
                            while (usedGroupNames.Contains(displayName))
                            {
                                displayName = SampleData.GetSampleValueRandom(SampleData.Accounts);
                            }

                            usedGroupNames.Add(displayName);
                            var mailNickname= Regex.Replace(displayName, @"[^a-z0-9]", "");
                            Group group = new Group();
                            group.DisplayName = displayName;
                            group.MailEnabled = false;
                            group.SecurityEnabled = true;
                            group.MailNickname = mailNickname;
                            batchCounter++;                            
                            client.Groups.AddGroupAsync(group, true).Wait();                           
                            if (batchCounter >= 50)
                            {
                                
                                client.Context.SaveChangesAsync().Wait();
                                updateProgressDetail(string.Format("Created {0}/{1} security groups", i+1, WorkingDefinition.NumberOfSecurityGroupsToCreate), batchCounter);
                                batchCounter = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            Errors.Log(ex);
                        }
                        
                    }
                    if (batchCounter > 0)
                    {
                        client.Context.SaveChangesAsync().Wait();
                        updateProgressDetail(string.Format("Created {0} security groups", WorkingDefinition.NumberOfSecurityGroupsToCreate), batchCounter);
                    }
                    if (WorkingDefinition.NumberOfSecurityGroupsToCreate > 0 && WorkingDefinition.MaxNumberOfUsersInCreatedSecurityGroups > 0)
                    {
                        updateProgressDetail("Retriving groups and users.", 0);
                        var allGroups = GetAvailableGroupObjectsInDirectory();

                        var allUsers = GetAvailableUserObjectsInDirectory();
                        updateProgressDetail("Adding users to groups.", 0);
                        foreach (var @group in allGroups)
                        {
                            if (usedGroupNames.Contains(@group.DisplayName))
                            {
                                var userGroupMaxCount = Math.Min(WorkingDefinition.MaxNumberOfUsersInCreatedSecurityGroups, allUsers.Count);
                                var userGroupCount = SampleData.GetRandomNumber(0, userGroupMaxCount);
                                if (userGroupCount > 0)
                                {
                                    allUsers.Shuffle();
                                    foreach (var user in allUsers.Take(userGroupCount))
                                    {
                                        group.Members.Add(user);
                                       
                                     //   progressDetail(string.Format("Added {0} user to {1} security group", user.DisplayName, @group.DisplayName));
                                    }
                                    group.UpdateAsync().Wait();

                                    updateProgressDetail(string.Format("Added {0} users to {1} security group", userGroupCount, @group.DisplayName));
                                }
                            }
                        }

                      
                    }
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                }

            }
        }

        protected override void ResolveWebAppsAndSiteCollections()
        {
          //  base.workingSiteCollections.Add(new SiteCollInfo() { URL = "https://cloudkit24.sharepoint.com/sites/dev});

            if (WorkingDefinition.CreateNewSiteCollections == 0)
            {
                base.workingSiteCollections.Add(new SiteCollInfo() {URL = WorkingDefinition.SiteCollection});
            }
            else
            {
                
                int totalProgress = WorkingDefinition.CreateNewSiteCollections;                  
                updateProgressOverall("Creating Web Applications / Site Collections", totalProgress);
                
                SPDGClientDataHelper helper=new SPDGClientDataHelper(WorkingDefinition);
                HashSet<string> existingSites=new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var siteCollectionUrl in helper.GetAllSiteCollections(Guid.Empty))
                {
                    existingSites.Add(siteCollectionUrl);
                }
                for (int s = 0; s < WorkingDefinition.CreateNewSiteCollections; s++)
                {
                    string siteName = "";
                    string siteUrl = "";
                    string leafName = "";
                    int i = 0;
                    string baseName = "";
                    do
                    {
                        siteName = SampleData.GetRandomName(SampleData.Companies, SampleData.Offices, null, ref i, out baseName);
                        leafName = Utilities.Path.GenerateSlug(siteName, 25);
                        siteUrl = string.Format("https://{0}.sharepoint.com/sites/{1}", WorkingDefinition.TenantName, leafName);
                    } while (existingSites.Contains(siteUrl));

                    updateProgressDetail("Creating site collection '" + siteUrl + "'");
                    var owner = WorkingDefinition.SiteCollOwnerLogin;
                    if (string.IsNullOrEmpty(owner))
                    {
                        owner = WorkingDefinition.Username;
                    }
                    helper.CreateNewSiteCollection(siteName, leafName, owner);
                    SiteCollInfo siteCollInfo = new SiteCollInfo();
                    siteCollInfo.URL = siteUrl;
                    workingSiteCollections.Add(siteCollInfo);
                }               
            }            
            
        }

        protected override void AssociateWorkflows()
        {
            //nothing
        }

        protected override void AssociateCustomWorkflows()
        {
            //nothing
        }
    }
}