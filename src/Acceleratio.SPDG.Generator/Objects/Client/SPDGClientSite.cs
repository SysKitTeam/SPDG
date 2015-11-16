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
        }

        public override SPDGWeb RootWeb
        {
            get
            {
                return new SPDGClientWeb(_clientSite.RootWeb, null, _context, this);
            }
        }
        public override void Dispose()
        {
            _context.Dispose();
        }
    }
}