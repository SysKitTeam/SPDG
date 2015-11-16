using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator
{
    public partial class ServerDataGenerator
    {
        protected override void CreateUsersAndGroups()
        {
            if(WorkingDefinition.NumberOfUsersToCreate > 0  )
            {
                try
                {
                    Log.Write("Creating Active Directory users.");
                    AD.createUsers(WorkingDefinition.ADDomainName, WorkingDefinition.ADOrganizationalUnit, WorkingDefinition.NumberOfUsersToCreate);
                }
                catch(Exception ex)
                {
                    Errors.Log(ex);
                }
            }

            if (WorkingDefinition.NumberOfSecurityGroupsToCreate > 0)
            {
                try
                {
                    Log.Write("Creating Active Directory groups.");
                    AD.createGroups(WorkingDefinition.ADDomainName, WorkingDefinition.ADOrganizationalUnit, WorkingDefinition.NumberOfSecurityGroupsToCreate);
                }
                catch (Exception ex)
                {
                    Errors.Log(ex);
                }

            }


        }
    }
}
