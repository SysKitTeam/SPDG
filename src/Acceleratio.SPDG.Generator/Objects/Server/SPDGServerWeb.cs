using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;

namespace Acceleratio.SPDG.Generator.Objects.Server
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
           return new SPDGServerList(_spWeb.Lists.TryGetList(title));
        }
    }
}
