using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGFolder
    {
        public abstract string Name { get; }
        public abstract string Url { get; }
        public abstract IEnumerable<SPDGFolder> SubFolders { get; }
        public abstract SPDGFolder AddFolder(string name);
        public abstract void Update();
    }
}
