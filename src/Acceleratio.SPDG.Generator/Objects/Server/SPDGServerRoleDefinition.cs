using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects.Server
{
    public class SPDGServerRoleDefinition : SPDGRoleDefinition
    {
        private SPRoleDefinition _spRoleDefinition;

        public SPDGServerRoleDefinition(SPRoleDefinition spRoleDefinition)
        {
            _spRoleDefinition = spRoleDefinition;
        }

        public override int ID
        {
            get { return _spRoleDefinition.Id; }
        }

        public override string Name
        {
            get { return _spRoleDefinition.Name; }
        }

        public override bool IsGuestRole
        {
            get { return _spRoleDefinition.Type == SPRoleType.Guest; }
        }

        public SPRoleDefinition RoleDefinition
        {
            get { return _spRoleDefinition; }
        }
    }
}
