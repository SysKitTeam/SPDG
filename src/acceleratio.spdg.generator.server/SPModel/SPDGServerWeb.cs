using System;
using System.Collections.Generic;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;

namespace Acceleratio.SPDG.Generator.Server.SPModel
{
    class SPDGServerWeb : SPDGWeb
    {
        private readonly SPWeb _spWeb;

        public SPDGServerWeb(SPWeb spWeb)
        {
            _spWeb = spWeb;
        }

        public override string Url
        {
            get { return _spWeb.Url; }
        }

        public override bool HasUniqueRoleAssignments
        {
            get { return _spWeb.HasUniqueRoleAssignments; }
        }

       
        public override void Dispose()
        {
            _spWeb.Dispose();
        }

        public override SPDGWeb AddWeb(string url, string siteName, string description, uint lcid, string templateName, bool useUniquePermissions, bool convertIfThere)
        {
            var spWeb=_spWeb.Webs.Add(url, siteName, description, lcid, templateName, useUniquePermissions, convertIfThere);
            return new SPDGServerWeb(spWeb);
        }

        public override string Title
        {
            get { return _spWeb.Title; }
        }
        public override string ServerRelativeUrl {
            get { return _spWeb.ServerRelativeUrl; }

        }

        public override IEnumerable<SPDGWeb> Webs
        {
            get
            {
                foreach (SPWeb spWeb in _spWeb.Webs)
                {
                    yield return new SPDGServerWeb(spWeb);
                }                
            }
        }

        public override SPDGWeb ParentWeb
        {
            get
            {
                if (_spWeb.ParentWeb != null)
                {
                    return new SPDGServerWeb(_spWeb.ParentWeb);
                }
                else
                {
                    return null;
                }
            }
        }

        public override void AddNavigationNode(string title, string url, NavigationNodeLocation location)
        {
            SPNavigationNodeCollection topnav = null;
            SPNavigationNode node = new SPNavigationNode(title, url);
            if (_spWeb.Navigation.GetNodeByUrl(node.Url) != null)
            {
                return;
            }
            switch (location)
            {
                case NavigationNodeLocation.TopNavigationBar:
                    topnav = _spWeb.Navigation.TopNavigationBar;
                    topnav.AddAsLast(node);
                    break;                              
                case NavigationNodeLocation.QuickLaunchLists:                                        
                    _spWeb.Navigation.AddToQuickLaunch(node, SPQuickLaunchHeading.Lists);                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }   
        }

        public override string Name
        {
            get { return _spWeb.Name; }
        }

        public override Guid ID
        {
            get { return _spWeb.ID; }
        }

        public override IEnumerable<SPDGList> Lists
        {
            get
            {
                foreach (SPList spList in _spWeb.Lists)
                {
                    yield return new SPDGServerList(spList);
                }
            }
        }

        public override Guid AddList(string title, string description, int templateId)
        {
            return _spWeb.Lists.Add(title, description, (SPListTemplateType) templateId);
        }

        public override SPDGList GetList(Guid id)
        {
            return new SPDGServerList(_spWeb.Lists[id]);
        }
        public override SPDGList GetList(string listName)
        {
            return new SPDGServerList(_spWeb.Lists[listName]);
        }
        public override SPDGList TryGetList(string title)
        {
           var lst=_spWeb.Lists.TryGetList(title);
            if (lst == null)
            {
                return null;
            }
            else
            {
                return new SPDGServerList(lst);
            }
        }

        public override SPDGRoleAssignment GetRoleAssignmentByPrincipal(SPDGPrincipal principal)
        {
            return ServerRoleAssignmentHelper.GetRoleAssignmentByPrincipal(_spWeb, principal);

        }

        public override void AddRoleAssignment(SPDGPrincipal principal, IEnumerable<SPDGRoleDefinition> roledefinitions)
        {
           ServerRoleAssignmentHelper.AddRoleAssignment(_spWeb, principal, roledefinitions);
        }

        public override void BreakRoleInheritance(bool copyRoleAssignments)
        {            
            _spWeb.BreakRoleInheritance(copyRoleAssignments);
        }

        public override SPDGUser EnsureUser(string loginName)
        {
            var spUser = _spWeb.EnsureUser(loginName);
            
            return new SPDGServerUser(spUser);
        }

        public override void AddSiteGroup(string name, SPDGUser owner, SPDGUser defaultUser, string description)
        {
            var spOwner =((SPDGServerUser)owner).SPUser;
            var spDefaultUser = ((SPDGServerUser)defaultUser).SPUser;
            
            _spWeb.SiteGroups.Add(name, spOwner, spDefaultUser, description);
        }


        public override SPDGUser CurrentUser
        {
            get
            {
                return new SPDGServerUser(_spWeb.CurrentUser);
            }
        }

        public override IEnumerable<SPDGGroup> SiteGroups
        {
            get
            {
                
                foreach (SPGroup spGroup in _spWeb.SiteGroups)
                {
                    yield return  new SPDGServerGroup(spGroup);
                }
            }
        }

        public override IEnumerable<SPDGUser> SiteUsers
        {
            get
            {
                foreach (SPUser siteUser in _spWeb.SiteUsers)
                {
                    yield return new SPDGServerUser(siteUser);
                }
            }
        }

        public override IEnumerable<SPDGRoleDefinition> RoleDefinitions
        {
            get
            {
                foreach (SPRoleDefinition roleDefinition in _spWeb.RoleDefinitions)
                {
                    yield return new SPDGServerRoleDefinition(roleDefinition);
                }
            }
        }

        public override SPDGFolder GetFolder(string folderUrl)
        {
            return new SPDGServerFolder(_spWeb.GetFolder(folderUrl));
        }

        public override IEnumerable<SPDGWebTemplate> GetWebTemplates(uint lcid)
        {
            foreach (SPWebTemplate template in _spWeb.GetAvailableWebTemplates(lcid))
            {
                yield return new SPDGWebTemplate(template.Name, template.Title);
            }
        }

        public override uint Language
        {
            get { return _spWeb.Language; }
        }
    }
}
