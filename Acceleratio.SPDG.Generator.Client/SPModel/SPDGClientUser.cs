using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;

namespace Acceleratio.SPDG.Generator.Client.SPModel
{
    class SPDGClientUser: SPDGUser
    {
        private readonly User _user;

        public SPDGClientUser(User user) : base(user.Id, user.LoginName, user.Title)
        {
            _user = user;
        }

        public User User
        {
            get { return _user; }
        }

        public override bool IsDomainGroup
        {
            get { return _user.PrincipalType == PrincipalType.SecurityGroup; }
        }
    }
}
