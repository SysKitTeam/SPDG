using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
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
                    navNodeColl = _context.Web.Navigation.TopNavigationBar;
                    break;                
                case NavigationNodeLocation.QuickLaunchLists:
                    navNodeColl = _context.Web.Navigation.QuickLaunch;
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
    }
}
