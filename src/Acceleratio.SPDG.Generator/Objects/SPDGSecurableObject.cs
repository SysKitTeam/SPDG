using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGSecurableObject
    {
        public abstract SPDGRoleAssignment GetRoleAssignmentByPrincipal(SPDGPrincipal principal);
        public abstract void AddRoleAssignment(SPDGPrincipal principal, IEnumerable<SPDGRoleDefinition> roledefinitions);
        public abstract void BreakRoleInheritance(bool copyRoleAssignments);
        public abstract bool HasUniqueRoleAssignments { get; }
    }
}
