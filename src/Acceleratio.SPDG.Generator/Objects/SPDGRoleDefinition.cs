using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGRoleDefinition
    {
        public abstract int ID { get; }
        public abstract  string Name { get; }

        public abstract  bool IsGuestRole { get; }

    }
}
