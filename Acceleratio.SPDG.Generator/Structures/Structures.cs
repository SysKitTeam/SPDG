using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator
{
    internal class SiteCollInfo
    {
        
        internal SiteCollInfo()
        {
            Sites = new List<SiteInfo>();
        }

        internal List<SiteInfo> Sites {get;set;}
        internal string URL {get;set;}
    }

    internal class SiteInfo
    {
        
        internal SiteInfo()
        {
            Lists = new List<ListInfo>();
        }

        internal List<ListInfo> Lists {get;set;}
        internal string URL {get;set;}
    }

    internal class ListInfo
    {
        
        internal ListInfo()
        {
            Folders = new List<FolderInfo>();
        }

        internal List<FolderInfo> Folders {get;set;}
        internal string Name {get;set;}
    }

    internal class FolderInfo
    {
        
        internal FolderInfo()
        {

        }

        internal string Name {get;set;}
    }
}
