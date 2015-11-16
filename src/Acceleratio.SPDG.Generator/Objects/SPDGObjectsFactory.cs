using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator.Objects.Server;
using  Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGObjectsFactory
    {
        public abstract SPDGSite GetSite(string url);

    }

    public class SPDGServerObjectsFactory : SPDGObjectsFactory
    {
        public override SPDGSite GetSite(string url)
        {
            return new SPDGServerSite(new SPSite(url));
        }
    }
}
