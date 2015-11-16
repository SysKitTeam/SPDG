using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
{
    class SPDGClientWeb : SPDGWeb
    {
        private readonly Web _web;
        private readonly SPDGWeb _parentWeb;
        private readonly ClientContext _context;
        private readonly SPDGClientSite _site;

        public SPDGClientWeb(Web web, SPDGWeb parentWeb, ClientContext context, SPDGClientSite site)
        {
            
            _web = web;
            _parentWeb = parentWeb;
            _context = context;
            _site = site;
            
        }

        public override string Title
        {
            get { return _web.Title; }
            
        }
        public override string ServerRelativeUrl { get { return _web.ServerRelativeUrl; } }

        public override IEnumerable<SPDGWeb> Webs
        {
            get
            {
                _context.Load(_web.Webs);
                _context.ExecuteQuery();
                foreach (var spWeb in _web.Webs)
                {
                    yield return new SPDGClientWeb(spWeb, this, _context, _site);
                }
            }
        }

        public override string Url
        {
            get { return _web.Url; }
        }
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override SPDGWeb AddWeb(string url, string siteName, string description, uint lcid, string templateName, bool useUniquePermissions, bool convertIfThere)
        {
            WebCreationInformation creation = new WebCreationInformation();
            creation.Url = url;
            creation.Title = siteName;
            creation.Description = description;
            creation.Language = (int) lcid;            
            creation.UseSamePermissionsAsParentSite = !useUniquePermissions;
            creation.WebTemplate = templateName;
            Web newWeb = _context.Web.Webs.Add(creation);
            _context.Load(newWeb);
            _context.ExecuteQuery();
            return new SPDGClientWeb(newWeb, this, _context, _site);
        }

        public override SPDGWeb ParentWeb
        {
            get { return _parentWeb; }
        }

        public override void AddNavigationNode(string title, string url)
        {
            NavigationNodeCollection qlNavNodeColl = _context.Web.Navigation.QuickLaunch;
            //Create a new node
            NavigationNodeCreationInformation newNavNode = new NavigationNodeCreationInformation();
            newNavNode.Title = title;
            newNavNode.Url = url;
            qlNavNodeColl.Add(newNavNode);
            _context.ExecuteQuery();
        }

        public override string Name
        {
            get
            {
                var url= this.Url.TrimEnd('/');
                var name = url.Substring(url.LastIndexOf("/") + 1);
                return name;
            }
        }

        public override Guid ID { get { return _web.Id; } }
    }
}
