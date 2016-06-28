using System.Collections.Generic;

namespace Acceleratio.SPDG.Generator.SPModel
{
    public abstract class SPDGFolder
    {
        public abstract string Name { get; }
        public abstract string Url { get; }
        public abstract IEnumerable<SPDGFolder> SubFolders { get; }
        public abstract SPDGListItem Item { get; }
        public abstract SPDGFolder AddFolder(string name);
        public abstract void Update();
        public abstract SPDGFile AddFile(string url, byte[] content, bool overWrite);

    }
}
