using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Acceleratio.SPDG.Generator.GenerationTasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Group = Microsoft.Azure.ActiveDirectory.GraphClient.Group;

namespace Acceleratio.SPDG.Generator.Client.GenerationTasks
{
    class CreateUsersAndGroupsGenerationTask : DataGenerationTaskBase
    {
        private readonly ClientDataGenerator _generator;

        public override string Title
        {
            get { return "Creating Directory Users And Groups."; }
        }

        public CreateUsersAndGroupsGenerationTask(ClientDataGenerator owner) : base(owner)
        {
            _generator = owner;
        }
      
        public override int CalculateTotalSteps()
        {
            var totalSteps = WorkingDefinition.NumberOfUsersToCreate + WorkingDefinition.NumberOfSecurityGroupsToCreate;
            if (WorkingDefinition.MaxNumberOfUsersInCreatedSecurityGroups > 0)
            {
                totalSteps += WorkingDefinition.NumberOfSecurityGroupsToCreate;
            }
            return totalSteps;           
        }

        public override void Execute()
        {           
            var client = _generator.DataHelper.GetADClient();

            List<ITenantDetail> tenantsList = client.TenantDetails                    
                    .ExecuteAsync().Result.CurrentPage.ToList();
            ITenantDetail tenant = tenantsList.First();

            var defaultDomain = tenant.VerifiedDomains.First(x => x.@default.HasValue && x.@default.Value);
            if (WorkingDefinition.NumberOfUsersToCreate > 0)
            {
                try
                {                  
                    int batchcounter = 0;
                    HashSet<Tuple<string, string>> usedNames = new HashSet<Tuple<string, string>>();
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
                            userPrincipal.UserPrincipalName = userPrincipal.MailNickname + "@" + defaultDomain.Name;                                                     
                            userPrincipal.DisplayName = userPrincipal.GivenName + " " + userPrincipal.Surname;
                            userPrincipal.UsageLocation = "US";
                            userPrincipal.PasswordProfile = new PasswordProfile
                            {
                                Password = "pass@word1",
                                ForceChangePasswordNextLogin = false,

                            };
                            client.Users.AddUserAsync(userPrincipal, true).Wait();
                            batchcounter++;
                            if (batchcounter >= 50)
                            {

                                client.Context.SaveChangesAsync().Wait();
                                Owner.IncrementCurrentTaskProgress(string.Format("Created {0}/{1} users", i + 1, WorkingDefinition.NumberOfUsersToCreate), batchcounter);
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
                        Owner.IncrementCurrentTaskProgress(string.Format("Created {0} users", WorkingDefinition.NumberOfUsersToCreate), batchcounter);
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
                    HashSet<string> usedGroupNames = new HashSet<string>();
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
                            var mailNickname = Regex.Replace(displayName, @"[^a-z0-9]", "");
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
                                Owner.IncrementCurrentTaskProgress(string.Format("Created {0}/{1} security groups", i + 1, WorkingDefinition.NumberOfSecurityGroupsToCreate), batchCounter);
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
                        Owner.IncrementCurrentTaskProgress(string.Format("Created {0} security groups", WorkingDefinition.NumberOfSecurityGroupsToCreate), batchCounter);
                    }
                    if (WorkingDefinition.NumberOfSecurityGroupsToCreate > 0 && WorkingDefinition.MaxNumberOfUsersInCreatedSecurityGroups > 0)
                    {
                        Owner.IncrementCurrentTaskProgress("Retriving groups and users.", 0);
                        var allGroups = _generator.DataHelper.GetAvailableGroupObjectsInDirectory();

                        var allUsers = _generator.DataHelper.GetAvailableUserObjectsInDirectory();
                        Owner.IncrementCurrentTaskProgress("Adding users to groups.", 0);
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

                                    Owner.IncrementCurrentTaskProgress(string.Format("Added {0} users to {1} security group", userGroupCount, @group.DisplayName));
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
    }
}
