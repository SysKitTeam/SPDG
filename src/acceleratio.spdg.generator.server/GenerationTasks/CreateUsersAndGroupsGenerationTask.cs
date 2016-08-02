using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using Acceleratio.SPDG.Generator.GenerationTasks;

namespace Acceleratio.SPDG.Generator.Server.GenerationTasks
{
    class CreateUsersAndGroupsGenerationTask : DataGenerationTaskBase
    {
        public override string Title
        {
            get { return "Creating Active Directory users and groups."; }
        }

        new ServerGeneratorDefinition WorkingDefinition
        {
            get { return (ServerGeneratorDefinition)base.WorkingDefinition; }
        }
       
        public CreateUsersAndGroupsGenerationTask(IDataGenerationTaskOwner owner) : base(owner)
        {

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
            var users = new List<Principal>();
            if (WorkingDefinition.NumberOfUsersToCreate > 0)
            {
                try
                {
                    Log.Write("Creating Active Directory users.");
                    users = createUsers(WorkingDefinition.ADDomainName, WorkingDefinition.ADOrganizationalUnit, WorkingDefinition.NumberOfUsersToCreate);
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
                    createGroups(WorkingDefinition.ADDomainName, WorkingDefinition.ADOrganizationalUnit, WorkingDefinition.NumberOfSecurityGroupsToCreate, users);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                }

            }
        }

        public List<Principal> createUsers(string domain, string ou, int numOfUsers)
        {
            ContextType contextType = ContextType.Domain;
            //must pass null parameter to principalcontext if no ou selected
            if (ou == "")
            {
                ou = null;
            }
            var createdPrincipals = new List<Principal>();
            using (PrincipalContext ctx = new PrincipalContext(contextType, domain, ou))
            {
                for (int i = 0; i < numOfUsers; i++)
                {
                    try
                    {
                        UserPrincipal userPrincipal = new UserPrincipal(ctx);
                        userPrincipal.Surname = SampleData.GetSampleValueRandom(SampleData.LastNames);
                        userPrincipal.GivenName = SampleData.GetSampleValueRandom(SampleData.FirstNames); ;
                        userPrincipal.SamAccountName = userPrincipal.GivenName.ToLower() + "." + userPrincipal.Surname.ToLower();
                        userPrincipal.Name = userPrincipal.GivenName + " " + userPrincipal.Surname;
                        userPrincipal.DisplayName = userPrincipal.GivenName + " " + userPrincipal.Surname;

                        string pwdOfNewlyCreatedUser = "Acce1234!";

                        userPrincipal.SetPassword(pwdOfNewlyCreatedUser);
                        userPrincipal.Enabled = true;
                        userPrincipal.PasswordNeverExpires = true;
                        userPrincipal.Save();
                        Owner.IncrementCurrentTaskProgress(string.Format("Created {0}/{1} users.", i, numOfUsers));
                        createdPrincipals.Add(userPrincipal);
                    }
                    catch (Exception ex)
                    {
                        Errors.Log(ex);
                    }
                }
            }

            return createdPrincipals;
        }

        public void createGroups(string domain, string ou, int numOfGroups, List<Principal> principalList)
        {
            ContextType contextType = ContextType.Domain;
            //must pass null parameter to principalcontext if no ou selected
            if (ou == "")
            {
                ou = null;
            }
            using (PrincipalContext ctx = new PrincipalContext(contextType, domain, ou))
            {
                for (int i = 0; i < numOfGroups; i++)
                {
                    try
                    {
                        GroupPrincipal groupPrincipal = new GroupPrincipal(ctx);
                        groupPrincipal.Name = SampleData.GetSampleValueRandom(SampleData.Accounts);
                        groupPrincipal.DisplayName = groupPrincipal.Name;
                        addPrincipalsToGroup(groupPrincipal, principalList);

                        groupPrincipal.Save();
                        Owner.IncrementCurrentTaskProgress(string.Format("Created {0}/{1} groups.", i, numOfGroups));
                        principalList.Add(groupPrincipal);
                    }
                    catch (Exception ex)
                    {
                        Errors.Log(ex);
                    }
                }
            }
        }

        private void addPrincipalsToGroup(GroupPrincipal group, List<Principal> availablePrincipals)
        {
            var userCount = Math.Min(WorkingDefinition.MaxNumberOfUsersInCreatedSecurityGroups, availablePrincipals.Count);

            if (userCount > 0)
            {
                availablePrincipals.Shuffle();
                var randomPrincipals = availablePrincipals.Take(userCount);

                foreach (var randomPrincipal in randomPrincipals)
                {
                    group.Members.Add(group.Context, IdentityType.Sid, randomPrincipal.Sid.Value);
                }
            }
        }
    }
}
