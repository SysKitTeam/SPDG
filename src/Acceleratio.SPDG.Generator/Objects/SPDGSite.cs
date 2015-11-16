using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGSite : IDisposable
    {
        public abstract SPDGWeb RootWeb { get; }
        public abstract void Dispose();
    }
}
