using System.Collections.Generic;

namespace Acceleratio.SPDG.Generator.Model
{
    public abstract class SPDGRoleAssignment
    {
        public SPDGPrincipal Member { get; private set; }
        public IEnumerable<SPDGRoleDefinition> RoleDefinitionBindings { get; private set; }
        public SPDGRoleAssignment(SPDGPrincipal principal, IEnumerable<SPDGRoleDefinition> roleDefinitionBindings)
        {
            Member = principal;
            RoleDefinitionBindings = roleDefinitionBindings;
        }

        public abstract void Update();
    }
}
