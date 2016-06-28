using Acceleratio.SPDG.Generator.Model;
using Acceleratio.SPDG.Generator.Server.Model;
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