using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects.Server
{
    
    class SPDGServerRoleAssignment : SPDGRoleAssignment
    {
        private SPRoleAssignment _roleAssignment;

        public SPDGServerRoleAssignment(SPRoleAssignment roleAssignment, SPDGPrincipal member, IEnumerable<SPDGRoleDefinition> roleDefinitionBindings)
            : base(member, roleDefinitionBindings)
        {
            _roleAssignment = roleAssignment;
        }

        public SPDGServerRoleAssignment(SPDGPrincipal member)
            : base(member, new List<SPDGRoleDefinition>())
        {
            if (member is SPDGServerUser)
            {
                _roleAssignment = new SPRoleAssignment(((SPDGServerUser) member).SPUser);
            }
            else
            {
                _roleAssignment = new SPRoleAssignment(((SPDGServerGroup) member).Group);
            }
        }

        
        public override void Update()
        {
            _roleAssignment.Update();
        }

        public SPRoleAssignment SPRoleAssignment
        {
            get { return _roleAssignment; }
        }      
    }
}
