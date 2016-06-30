using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Client.SPModel
{
    class SPDGClientWeb : SPDGWeb
    {
        public static Expression<Func<Web, object>>[] IncludeExpression
        {
            get
            {
                List<Expression<Func<Web, object>>> includeExpression = new List<Expression<Func<Web, object>>>();
                includeExpression.Add(web => web.Id);
                includeExpression.Add(web => web.Title);
                includeExpression.Add(web => web.Url);
                includeExpression.Add(web => web.ServerRelativeUrl);
                includeExpression.Add(web => web.RootFolder);
                includeExpression.Add(web => web.CurrentUser);
                includeExpression.Add(web => web.Language);
                includeExpression.Add(web => web.HasUniqueRoleAssignments);


                return includeExpression.ToArray();
            }
        }
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
                _context.Load(_web.Webs, coll=> coll.Include(IncludeExpression));
                _context.ExecuteQuery();
                foreach (var spWeb in _web.Webs)
                {
                    yield return new SPDGClientWeb(spWeb, this, _context, _site);
                }
            }
        }

        public override IEnumerable<SPDGList> Lists
        {
            get
            {
                _context.Load(_web.Lists, collection => collection.Include(SPDGClientList.IncludeExpression));
                _context.ExecuteQuery();
                foreach (var list in _web.Lists)
                {
                    yield return  new SPDGClientList(this, list, _context);
                }
            }
        }

        public override Guid AddList(string title, string description, int templateId)
        {
            ListCreationInformation createInfo=new ListCreationInformation();
            createInfo.Description = description;
            createInfo.Title = title;
            createInfo.TemplateType = templateId;
            var list=_web.Lists.Add(createInfo);
            _context.Load(list);
            _context.ExecuteQuery();
            return list.Id;
        }

        public override SPDGList GetList(Guid id)
        {
            var list = _web.Lists.GetById(id);
            _context.Load(list, SPDGClientList.IncludeExpression);
            _context.ExecuteQuery();
            return new SPDGClientList(this,list, _context);
        }

        public override SPDGList GetList(string title)
        {
            var list = TryGetList(title);
            if (list == null)
            {
                throw new InvalidOperationException();
            }

            return list;
        }

        public override SPDGList TryGetList(string title)
        {
            var query = _web.Lists.Where(x => x.Title == title).Include(SPDGClientList.IncludeExpression);
            
          //  var list = _web.Lists.GetByTitle(title);
            var results=_context.LoadQuery(query);
            _context.ExecuteQuery();
            if (results.Any())
            {
                return new SPDGClientList(this, results.First(), _context);
            }
            else
            {
                return null;
            }
        }

        public override bool HasUniqueRoleAssignments
        {
            get { return _web.HasUniqueRoleAssignments; }
        }

        public override SPDGRoleAssignment GetRoleAssignmentByPrincipal(SPDGPrincipal principal)
        {
            return ClientRoleAssignmentHelper.GetRoleAssignmentByPrincipal(_web, _context, principal);
        }

        public override void AddRoleAssignment(SPDGPrincipal principal, IEnumerable<SPDGRoleDefinition> roledefinitions)
        {
            ClientRoleAssignmentHelper.AddRoleAssignment(_web, _context, principal, roledefinitions);
        }

        public override void BreakRoleInheritance(bool copyRoleAssignments)
        {
            if (_web.Id == _site.RootWeb.ID)
            {
                return;
            }
            _web.BreakRoleInheritance(copyRoleAssignments,false);
            _context.Load(_web,x=>x.HasUniqueRoleAssignments);
            _context.ExecuteQuery();
        }

        public override SPDGUser EnsureUser(string loginName)
        {
            invalidateSiteUsers();
            var user=_web.EnsureUser(loginName);
            _context.Load(user);
            _context.ExecuteQuery();
            return new SPDGClientUser(user);
        }

        public override string Url
        {
            get { return _web.Url; }
        }
        public override void Dispose()
        {
            //no dispose            
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
            Web newWeb = _web.Webs.Add(creation);
            _context.Load(newWeb);
            _context.ExecuteQuery();
            return new SPDGClientWeb(newWeb, this, _context, _site);
        }

        public override SPDGWeb ParentWeb
        {
            get { return _parentWeb; }
        }

      

        public override void AddNavigationNode(string title, string url, NavigationNodeLocation location)
        {
            NavigationNodeCreationInformation newNavNode = new NavigationNodeCreationInformation();
            newNavNode.Title = title;
            newNavNode.Url = url;

            NavigationNodeCollection navNodeColl = null;
            switch (location)
            {
                case NavigationNodeLocation.TopNavigationBar:
                    navNodeColl = _web.Navigation.TopNavigationBar;
                    break;                
                case NavigationNodeLocation.QuickLaunchLists:
                    navNodeColl = _web.Navigation.QuickLaunch;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }


            try
            {
                navNodeColl.Add(newNavNode);
                _context.ExecuteQuery();
            }
            catch (Exception)
            {
                //ignore, vec postoji
            }
            
        }

        public override string Name
        {
            get
            {
                return _web.RootFolder.Name;
                //var url= this.Url.TrimEnd('/');
                //var name = url.Substring(url.LastIndexOf("/") + 1);
                //return name;
            }
        }

        public override Guid ID
        {
            get { return _web.Id; }
        }

        public override SPDGUser CurrentUser
        {
            get
            {
                return new SPDGClientUser(_web.CurrentUser);
            }
        }

        private void invalidateSiteGroups()
        {
            _siteGroups = null;
        }

        public override void AddSiteGroup(string name, SPDGUser owner, SPDGUser defaultUser, string description)
        {
            if (_site.RootWeb.ID != this.ID)
            {
                _site.RootWeb.AddSiteGroup(name, owner, defaultUser, description);
            }
            invalidateSiteGroups();
            GroupCreationInformation groupCreateInformation=new GroupCreationInformation();
            groupCreateInformation.Title = name;
            groupCreateInformation.Description = description;

            var group= _web.SiteGroups.Add(groupCreateInformation);
            group.Owner = ((SPDGClientUser) owner).User;
            group.Update();
            _context.ExecuteQuery();
        }

        private List<SPDGGroup> _siteGroups;
        public override IEnumerable<SPDGGroup> SiteGroups
        {
            get
            {
                if (_site.RootWeb.ID != this.ID)
                {
                    return _site.RootWeb.SiteGroups;
                }
                if (_siteGroups == null)
                {
                    _siteGroups=new List<SPDGGroup>();
                    _context.Load(_web.SiteGroups);
                    _context.ExecuteQuery();
                    foreach (var @group in _web.SiteGroups)
                    {
                        _siteGroups.Add(new SPDGClientGroup(@group, _context));
                    }
                }
                
                return _siteGroups;
            }
        }

        private void invalidateSiteUsers()
        {
            _siteUsers = null;                     
        }
        private List<SPDGUser> _siteUsers;

        public override IEnumerable<SPDGUser> SiteUsers
        {
            get
            {
                if (_site.RootWeb.ID != this.ID)
                {
                    return _site.RootWeb.SiteUsers;
                }
                if (_siteUsers == null)
                {
                    _siteUsers = new List<SPDGUser>();
                    _context.Load(_web.SiteUsers);
                    _context.ExecuteQuery();
                    foreach (var user in _web.SiteUsers)
                    {
                        _siteUsers.Add(new SPDGClientUser(user));
                    }
                }
                return _siteUsers;
            }
        }

        private List<SPDGClientRoleDefinition> _roleDefinitions;

        public override IEnumerable<SPDGRoleDefinition> RoleDefinitions
        {
            get
            {
                if (_site.RootWeb.ID != this.ID)
                {
                    return _site.RootWeb.RoleDefinitions;
                }
                if (_roleDefinitions == null)
                {
                    _roleDefinitions=new List<SPDGClientRoleDefinition>();
                    _context.Load(_web.RoleDefinitions);
                    _context.ExecuteQuery();
                    foreach (var roleDefinition in _web.RoleDefinitions)
                    {
                        _roleDefinitions.Add(new SPDGClientRoleDefinition(roleDefinition));
                    }
                }
                return _roleDefinitions;
            }
        }

        public override SPDGFolder GetFolder(string folderUrl)
        {
            var folder = _web.GetFolderByServerRelativeUrl(folderUrl);
            _context.Load(folder);
            _context.Load(folder.ListItemAllFields);
            return new SPDGClientFolder(folder, _context);
        }

        private List<SPDGWebTemplate> _webTemplates;
        public override IEnumerable<SPDGWebTemplate> GetWebTemplates(uint lcid)
        {
            if (_webTemplates == null)
            {
                _webTemplates=new List<SPDGWebTemplate>();
                var templates = _web.GetAvailableWebTemplates(lcid, true);
                _context.Load(templates);
                _context.ExecuteQuery();
                foreach (var template in templates)
                {
                    _webTemplates.Add(new SPDGWebTemplate(template.Name, template.Title));
                }
            }
            return _webTemplates;

        }

        public override uint Language
        {
            get { return _web.Language; }
        }
    }
}
