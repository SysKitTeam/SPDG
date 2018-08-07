using System.Collections.Generic;

namespace Acceleratio.SPDG.Generator.SPModel
{
    public abstract class SPDGPrincipal
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string LoginName { get; private set; }
        public string Email { get; protected set; }
        
        public SPDGPrincipal(int id, string loginName, string name, string email)
        {
            ID = id;
            LoginName = loginName;
            Name = name;
            Email = email;
        }
    }
    public abstract class SPDGUser : SPDGPrincipal
    {
        protected SPDGUser(int id, string loginName, string name, string email) : base(id, loginName, name, email)
        {            
        }

        public abstract  bool IsDomainGroup { get; }
        public abstract bool IsGuestUser { get; }

    }

    public abstract class SPDGGroup : SPDGPrincipal
    {
        public SPDGGroup(int id, string loginName, string name) : base(id, loginName, name, "")
        {
        }

        public abstract void AddUsers(IEnumerable<SPDGUser> users);

    }
}
