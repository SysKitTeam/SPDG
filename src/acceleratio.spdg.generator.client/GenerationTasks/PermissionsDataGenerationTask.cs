using System;
using System.Collections.Generic;
using System.Linq;
using Acceleratio.SPDG.Generator.GenerationTasks;

namespace Acceleratio.SPDG.Generator.Client.GenerationTasks
{
    class PermissionsDataGenerationTask: PermissionsDataGenerationTaskBase
    {
        private readonly ClientDataGenerator _generator;

        public PermissionsDataGenerationTask(ClientDataGenerator owner) : base(owner)
        {
            _generator = owner;
        }

        protected override List<string> GetAvailableUsersInDirectory()
        {
            return _generator.DataHelper.GetAvailableUserObjectsInDirectory().Select(x => x.UserPrincipalName).ToList();
        }
        protected override List<string> GetAvailableGroupsInDirectory()
        {
            var allGroups = _generator.DataHelper.GetAvailableGroupObjectsInDirectory();

            List<string> groups = new List<string>();

            try
            {
                // office 365 groups (there is an email)
                groups = allGroups.Where(g => g.Mail != null && g.Mail.Contains("@")).Select(g => g.Mail).ToList();

                // security groups (there is no email, using display name to ensure user)
                groups.AddRange(allGroups.Where(g => g.Mail == null || !g.Mail.Contains("@")).Select(g => g.DisplayName).ToList());
            }
            catch (Exception ex)
            {
                Errors.Log(ex);
            }

            return groups;
        }
    }
}
