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
            return _generator.DataHelper.GetAvailableGroupObjectsInDirectory().Select(x => x.DisplayName).ToList();
        }
    }
}
