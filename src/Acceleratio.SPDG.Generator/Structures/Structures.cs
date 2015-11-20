using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator
{
    public class SiteCollInfo
    {
        
        internal SiteCollInfo()
        {
            Sites = new List<SiteInfo>();
        }

        internal List<SiteInfo> Sites {get;set;}
        internal string URL {get;set;}
        
    }

    public class SiteInfo
    {
        
        internal SiteInfo()
        {
            Lists = new List<ListInfo>();
        }

        internal List<ListInfo> Lists {get;set;}
        internal string URL {get;set;}
        internal Guid ID { get; set; }
    }

    public class ListInfo
    {
        
        internal ListInfo()
        {
            Folders = new List<FolderInfo>();
        }

        internal List<FolderInfo> Folders {get;set;}
        internal string Name {get;set;}
        internal bool isLib { get; set; }
        internal  bool isBigList { get; set; }
        internal SPListTemplateType TemplateType { get; set; }
        public int ItemCount { get; set; }
    }

    public class FolderInfo
    {
        
        internal FolderInfo()
        {

        }

        internal string Name {get;set;}
        internal string URL { get; set; }
    }
}
