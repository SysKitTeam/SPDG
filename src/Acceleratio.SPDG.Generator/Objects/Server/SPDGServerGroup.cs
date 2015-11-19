using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects.Server
{
    class SPDGServerGroup : SPDGGroup
    {
        private readonly SPGroup _group;

        public SPDGServerGroup(SPGroup group) : base(group.ID, group.LoginName, group.Name)
        {
            _group = @group;
        }

        public SPGroup Group
        {
            get { return _group; }
        }
    }
}
