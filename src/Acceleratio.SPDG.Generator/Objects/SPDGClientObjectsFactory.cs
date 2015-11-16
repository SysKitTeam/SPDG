using System;
using Acceleratio.SPDG.Generator.Objects.Client;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects
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
            context.Credentials = _credentials;
            context.Load(context.Site, x=>x.Id, x=>x.Url, x=>x.ServerRelativeUrl);
            context.Load(context.Site.RootWeb, x=>x.Id, x=>x.Title, x=>x.Url, x=>x.ServerRelativeUrl);

            context.ExecuteQuery();
            return new SPDGClientSite(context, context.Site);
        }
    }
}