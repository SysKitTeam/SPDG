using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
{
    class SPDGClientGroup : SPDGGroup
    {
        private readonly Group _group;

        public SPDGClientGroup(Group group) : base(group.Id, group.LoginName, group.LoginName)
        {
            _group = @group;
        }

        public Group Group
        {
            get { return _group; }
        }
    }
}
