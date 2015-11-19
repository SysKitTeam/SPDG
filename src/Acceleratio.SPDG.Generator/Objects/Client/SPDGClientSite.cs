using System;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
{
    class SPDGClientSite : SPDGSite
    {
        private readonly ClientContext _context;
        private readonly Site _clientSite;

        public SPDGClientSite(ClientContext context, Site clientSite)
        {
            _context = context;
            _clientSite = clientSite;
            _rootWeb= new SPDGClientWeb(_clientSite.RootWeb, null, _context, this);
        }

        public override SPDGWeb OpenWeb(Guid id)
        {
            var web=_clientSite.OpenWebById(id);
            _context.Load(web, SPDGClientWeb.IncludeExpression);
            _context.ExecuteQuery();
            return new SPDGClientWeb(web, null, _context, this);
        }

        private SPDGWeb _rootWeb = null;
        public override SPDGWeb RootWeb
        {
            get
            {
                return _rootWeb;                
            }
        }
        public override void Dispose()
        {
            _context.Dispose();
        }
    }
}