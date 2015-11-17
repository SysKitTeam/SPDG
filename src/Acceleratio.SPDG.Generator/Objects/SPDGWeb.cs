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
        public abstract void AddNavigationNode(string title, string url, NavigationNodeLocation location);
        public abstract string Name { get; }
        public abstract Guid ID { get; }

        public abstract IEnumerable<SPDGList> Lists { get; }
        public abstract Guid AddList(string title, string description, int templateId);
        public abstract SPDGList GetList(Guid id);
        public abstract SPDGList TryGetList(string title);

    }

    public enum NavigationNodeLocation
    {
        TopNavigationBar = 1,
        QuickLaunch,
        QuickLaunchLists

    }
}
