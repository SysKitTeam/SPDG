using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGFile
    {
        public abstract  SPDGListItem Item { get; }
    }
}
