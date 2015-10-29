using System.IO;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Utilities
{
    public static class Site
    {
        //source http://stackoverflow.com/a/8443315/15626
        public static bool SiteExists(string url)
        {
            try
            {
                using (SPSite site = new SPSite(url))
                {
                    using (SPWeb web = site.OpenWeb(url, true))
                    {
                        return true;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
    }
}
