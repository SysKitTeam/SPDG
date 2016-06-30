using Acceleratio.SPDG.Generator.Client.SPModel;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Client
{
    public class SPDGClientObjectsFactory: SPDGObjectsFactory
    {
        private readonly SharePointOnlineCredentials _credentials;

        public SPDGClientObjectsFactory(SharePointOnlineCredentials credentials)
        {
            _credentials = credentials;
        }

        public override SPDGSite GetSite(string url)
        {
            ClientContext context = new ClientContext(url);
            context.RequestTimeout = 10 * 60 * 1000;
            context.Credentials = _credentials;
            context.Load(context.Site, x=>x.Id, x=>x.Url, x=>x.ServerRelativeUrl);
            context.Load(context.Site.RootWeb, SPDGClientWeb.IncludeExpression);

            context.ExecuteQuery();
            return new SPDGClientSite(context, context.Site);
        }
    }
}