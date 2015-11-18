using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Acceleratio.SPDG.Generator.Objects;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.SharePoint.Client;
using Group = Microsoft.Azure.ActiveDirectory.GraphClient.Group;
using User = Microsoft.Azure.ActiveDirectory.GraphClient.User;

namespace Acceleratio.SPDG.Generator
{
    public partial class ClientDataGenerator : DataGenerator
    {

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
            return WorkingDefinition.AzureAdAccessToken;
        }
        private ActiveDirectoryClient GetADClient()
        {
            Uri servicePointUri = new Uri("https://graph.windows.net");
            
            var serviceRoot = new Uri(servicePointUri, string.Format("{0}.onmicrosoft.com", WorkingDefinition.TenantName));            
            
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                async () => GetToken());
            return activeDirectoryClient;
        }

        protected override void CreateUsersAndGroups()
        {
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
                    Log.Write("Creating Active Directory users.");

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
                                batchcounter = 0;
                                client.Context.SaveChangesAsync().Wait();
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
                    Log.Write("Creating Active Directory groups.");
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
                            
                            client.Groups.AddGroupAsync(group,true).Wait();
                            if (batchCounter >= 50)
                            {
                                batchCounter = 0;
                                client.Context.SaveChangesAsync().Wait();
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
            //TODO:rf
            base.workingSiteCollections.Add(new SiteCollInfo() {URL = "https://cloudkit24.sharepoint.com/sites/Dev" });
        }
    }
}