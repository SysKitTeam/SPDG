using System;
using System.Collections.Generic;

namespace Acceleratio.SPDG.Generator.Model
{
    public abstract class SPDGWeb : SPDGSecurableObject, IDisposable
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
        public abstract SPDGList GetList(string title);
        public abstract SPDGList TryGetList(string title);        
        public abstract SPDGUser EnsureUser(string loginName);
        public abstract void AddSiteGroup(string name, SPDGUser owner, SPDGUser defaultUser, string description);
        public abstract SPDGUser CurrentUser { get; }
        public abstract IEnumerable<SPDGGroup> SiteGroups { get; }
        public abstract IEnumerable<SPDGUser> SiteUsers { get; } 
        public abstract IEnumerable<SPDGRoleDefinition> RoleDefinitions { get; }
        public abstract SPDGFolder GetFolder(string folderUrl);
        public abstract IEnumerable<SPDGWebTemplate> GetWebTemplates(uint lcid);
        public abstract  uint Language { get; }
    }

    public enum NavigationNodeLocation
    {
        TopNavigationBar = 1,
        QuickLaunch,
        QuickLaunchLists

    }
}
