using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGList
    {
        public abstract  string Title { get; }
        public abstract  string DefaultViewUrl { get; }
        public abstract SPDGFolder RootFolder { get; }
        
    }
}
