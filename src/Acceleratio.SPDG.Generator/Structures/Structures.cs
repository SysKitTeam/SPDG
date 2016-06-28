using System;
using System.Collections.Generic;
using Acceleratio.SPDG.Generator.SPModel;

namespace Acceleratio.SPDG.Generator.Structures
{
    public class SiteCollInfo
    {
        
        public SiteCollInfo()
        {
            Sites = new List<SiteInfo>();
        }

        public List<SiteInfo> Sites {get;set;}
        public string URL {get;set;}
        
    }

    public class SiteInfo
    {
        
        public SiteInfo()
        {
            Lists = new List<ListInfo>();
        }

        public List<ListInfo> Lists {get;set;}
        public string URL {get;set;}
        public Guid ID { get; set; }
    }

    public class ListInfo
    {

        public ListInfo()
        {
            Folders = new List<FolderInfo>();
        }

        public List<FolderInfo> Folders {get;set;}
        public string Name {get;set;}
        public bool isLib { get; set; }
        public  bool isBigList { get; set; }
        public SPDGListTemplateType TemplateType { get; set; }
        public int ItemCount { get; set; }
    }

    public class FolderInfo
    {        
        public string Name {get;set;}
        public string URL { get; set; }
    }
}
