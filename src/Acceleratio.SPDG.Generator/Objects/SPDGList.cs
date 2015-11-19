using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator.Objects
{
    public abstract class SPDGList : SPDGSecurableObject
    {
        public abstract  string Title { get; }
        public abstract  string DefaultViewUrl { get; }
        public abstract SPDGFolder RootFolder { get; }

        public abstract  IEnumerable<SPDGField> Fields { get; } 
        public abstract void AddFields(IEnumerable<SPDGFieldInfo> fields, bool addToDefaultView);
        public abstract void AddItems(IEnumerable<ISPDGListItemInfo> items);
        public abstract  IEnumerable<SPDGListItem> Items { get; } 

    }
}
