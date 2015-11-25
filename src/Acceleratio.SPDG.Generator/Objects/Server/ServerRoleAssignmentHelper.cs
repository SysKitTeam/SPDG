using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceleratio.SPDG.Generator.Objects.Client;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Server
{
    class ServerRoleAssignmentHelper
    {
        public static void AddRoleAssignment(SPSecurableObject securableObject, SPDGPrincipal principal, IEnumerable<SPDGRoleDefinition> roleDefinitions)
        {
            SPPrincipal spPrincipal;
            if (principal is SPDGServerUser)
            {
                spPrincipal = ((SPDGServerUser)principal).SPUser;
            }
            else
            {
                spPrincipal = ((SPDGServerGroup)principal).Group;
            }
            SPRoleAssignment roleAss = new SPRoleAssignment(spPrincipal);
            foreach (var spdgRoleDefinition in roleDefinitions)
            {
                var spRoleDef = ((SPDGServerRoleDefinition)spdgRoleDefinition).RoleDefinition;
                roleAss.RoleDefinitionBindings.Add(spRoleDef);
            }
            securableObject.RoleAssignments.Add(roleAss);            
        }

        public static SPDGRoleAssignment GetRoleAssignmentByPrincipal(SPSecurableObject securableObject, SPDGPrincipal principal)
        {
            SPPrincipal spPrincipal;
            if (principal is SPDGServerUser)
            {
                spPrincipal = ((SPDGServerUser)principal).SPUser;
            }
            else
            {
                spPrincipal = ((SPDGServerGroup)principal).Group;
            }
            try
            {
                var spRoleAss = securableObject.RoleAssignments.GetAssignmentByPrincipal(spPrincipal);
                return new SPDGServerRoleAssignment(spRoleAss, principal, spRoleAss.RoleDefinitionBindings.Cast<SPRoleDefinition>().Select(x => new SPDGServerRoleDefinition(x)));
            }
            catch (Exception)
            {
                //ugly but will do
                return null;
            }
        }
    }
}
