using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGPrincipal
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string LoginName { get; private set; }

        public SPDGPrincipal(int id, string loginName, string name)
        {
            ID = id;
            LoginName = loginName;
            Name = name;
        }
    }
    public abstract class SPDGUser : SPDGPrincipal
    {
        protected SPDGUser(int id, string loginName, string name) : base(id, loginName, name)
        {
        }

        public abstract  bool IsDomainGroup { get; }
    }

    public abstract class SPDGGroup : SPDGPrincipal
    {
        public SPDGGroup(int id, string loginName, string name) : base(id, loginName, name)
        {
        }

        public abstract void AddUsers(IEnumerable<SPDGUser> users);

    }
}
