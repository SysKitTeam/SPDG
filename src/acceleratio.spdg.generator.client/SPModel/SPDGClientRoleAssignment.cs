using System.Collections.Generic;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Client.SPModel
{
    public class SPDGClientRoleAssignment : SPDGRoleAssignment
    {

        private RoleAssignment _roleAssignment;

        public SPDGClientRoleAssignment(RoleAssignment roleAssignment, SPDGPrincipal member, IEnumerable<SPDGRoleDefinition> roleDefinitionBindings)
            : base(member, roleDefinitionBindings)
        {
            _roleAssignment = roleAssignment;
        }

        public SPDGClientRoleAssignment(SPDGPrincipal member)
            : base(member, new List<SPDGRoleDefinition>())
        {
        }
        
        public override void Update()
        {
            _roleAssignment.Update();
            _roleAssignment.Context.ExecuteQuery();
        }
    }
}
