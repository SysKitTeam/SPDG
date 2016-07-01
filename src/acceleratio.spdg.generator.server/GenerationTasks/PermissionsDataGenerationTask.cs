using System.Collections.Generic;
using Acceleratio.SPDG.Generator.GenerationTasks;

namespace Acceleratio.SPDG.Generator.Server.GenerationTasks
{
    class PermissionsDataGenerationTask : PermissionsDataGenerationTaskBase
    {

        List<string> _allUsers;
        List<string> _allGroups;       

        public PermissionsDataGenerationTask(IDataGenerationTaskOwner owner) : base(owner)
        {
        }

        protected override List<string> GetAvailableUsersInDirectory()
        {
            if (_allUsers == null)
            {
                _allUsers = AD.GetUsersFromAD();
            }
            return _allUsers;
        }

        protected override List<string> GetAvailableGroupsInDirectory()
        {
            if (_allGroups == null)
            {
                _allGroups = AD.GetGroupsFromAD();
            }
            return _allGroups;
        }
    }
}
