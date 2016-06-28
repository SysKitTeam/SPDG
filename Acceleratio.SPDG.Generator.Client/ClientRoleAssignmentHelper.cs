using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceleratio.SPDG.Generator.Model;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
{
    static class ClientRoleAssignmentHelper
    {

        public static void AddRoleAssignment(SecurableObject securableObject, ClientContext context, SPDGPrincipal principal, IEnumerable<SPDGRoleDefinition> roleDefinitions)
        {
            Principal p;
            if (principal is SPDGClientUser)
            {
                p = ((SPDGClientUser)principal).User;
            }
            else
            {
                p = ((SPDGClientGroup)principal).Group;
            }
            var roleDefinitionBindingCollection = new RoleDefinitionBindingCollection(context);
            foreach (var spdgRoleDefinition in roleDefinitions)
            {
                roleDefinitionBindingCollection.Add(((SPDGClientRoleDefinition)spdgRoleDefinition).Definition);
            }
            securableObject.RoleAssignments.Add(p, roleDefinitionBindingCollection);
            context.ExecuteQuery();
        }

        public static SPDGRoleAssignment GetRoleAssignmentByPrincipal(SecurableObject securableObject, ClientContext context, SPDGPrincipal principal)
        {
            var principalId = principal.ID;
            var roleAss = securableObject.RoleAssignments.Where(x => x.PrincipalId == principalId).Include(x => x.RoleDefinitionBindings, x => x.Member, x => x.PrincipalId);
            var results = context.LoadQuery(roleAss);
            context.ExecuteQuery();
            if (results.Any())
            {
                return new SPDGClientRoleAssignment(results.First(), principal, results.First().RoleDefinitionBindings.Select(x => new SPDGClientRoleDefinition(x)));
            }
            else
            {
                return null;
            }
        }
    }
}
