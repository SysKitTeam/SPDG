using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects.Server
{
    class SPDGServerUser : SPDGUser
    {
        private readonly SPUser _user;

        public SPDGServerUser(SPUser user) : base(user.ID, user.LoginName, user.Name)
        {
            _user = user;
        }

        public SPUser SPUser
        {
            get { return _user; }
        }

        public override bool IsDomainGroup
        {
            get { return _user.IsDomainGroup; }
        }
    }
}
