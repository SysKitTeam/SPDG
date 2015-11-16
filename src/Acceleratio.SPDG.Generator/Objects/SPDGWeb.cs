using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGWeb : IDisposable
    {
        public abstract  string Title { get; }
        public abstract  string ServerRelativeUrl { get; }
        public abstract IEnumerable<SPDGWeb> Webs { get; }
        public abstract string Url { get; }
        public abstract void Dispose();
        public abstract SPDGWeb AddWeb(string url, string siteName, string description, uint lcid, string templateName, bool useUniquePermissions, bool convertIfThere);
        public abstract SPDGWeb ParentWeb { get; }
        public abstract void AddNavigationNode(string title, string url);
        public abstract string Name { get; }
        public abstract Guid ID { get; }

    }
}
