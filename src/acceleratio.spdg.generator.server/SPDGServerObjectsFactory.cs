using Acceleratio.SPDG.Generator.Server.SPModel;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Server
{
    public class SPDGServerObjectsFactory : SPDGObjectsFactory
    {
        public override SPDGSite GetSite(string url)
        {
            return new SPDGServerSite(new SPSite(url));
        }
    }
}