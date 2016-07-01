using System;
using System.DirectoryServices.AccountManagement;
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
            if (WorkingDefinition.NumberOfUsersToCreate > 0)
            {
                try
                {
                    Log.Write("Creating Active Directory users.");
                    createUsers(WorkingDefinition.ADDomainName, WorkingDefinition.ADOrganizationalUnit, WorkingDefinition.NumberOfUsersToCreate);
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
                    createGroups(WorkingDefinition.ADDomainName, WorkingDefinition.ADOrganizationalUnit, WorkingDefinition.NumberOfSecurityGroupsToCreate);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                }

            }
        }

        public void createUsers(string domain, string ou, int numOfUsers)
        {
            ContextType contextType = ContextType.Domain;
            //must pass null parameter to principalcontext if no ou selected
            if (ou == "")
            {
                ou = null;
            }
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
                    }
                    catch (Exception ex)
                    {
                        Errors.Log(ex);
                    }
                }
            }
        }

        public void createGroups(string domain, string ou, int numOfGroups)
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

                        groupPrincipal.Save();
                        Owner.IncrementCurrentTaskProgress(string.Format("Created {0}/{1} groups.", i, numOfGroups));
                    }
                    catch (Exception ex)
                    {
                        Errors.Log(ex);
                    }
                }
            }
        }
    }
}
