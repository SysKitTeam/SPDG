using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
{
    internal class SPDGClientList : SPDGList
    {
        private readonly SPDGWeb _web;
        private readonly List _list;
        private readonly ClientContext _context;

        public override string Title
        {
            get { return _list.Title; }
        }

        public override string DefaultViewUrl
        {
            get { return _list.DefaultViewUrl; }
        }

        public SPDGClientList(SPDGWeb web, List list, ClientContext context)
        {
            _web = web;
            _list = list;
            _context = context;
        }

        public static Expression<Func<List, object>>[] IncludeExpression
        {
            get
            {
                List<Expression<Func<List, object>>> includeExpression = new List<Expression<Func<List, object>>>();
                includeExpression.Add(web => web.Id);
                includeExpression.Add(web => web.Title);
                includeExpression.Add(web => web.DefaultViewUrl);                
                return includeExpression.ToArray();
            }
        }


    }
}
