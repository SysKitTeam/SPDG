using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
{
    public class SPDGClientRoleDefinition :SPDGRoleDefinition
    {
        private RoleDefinition _roleDefinition;

        public SPDGClientRoleDefinition(RoleDefinition roleDefinition)
        {
            _roleDefinition = roleDefinition;
        }

        public override int ID
        {
            get { return _roleDefinition.Id; }
        }

        public override string Name
        {
            get { return _roleDefinition.Name; }
        }

        public override bool IsGuestRole
        {
            get { return _roleDefinition.RoleTypeKind == RoleType.Guest; }
        }

        public RoleDefinition Definition
        {
            get { return _roleDefinition; }            
        }

        
    }
}
