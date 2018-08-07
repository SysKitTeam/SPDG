using System.Collections.Generic;

namespace Acceleratio.SPDG.Generator.SPModel
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
        public abstract bool IsDocumentLibrary { get; }

        public abstract void AddView(string viewName, IEnumerable<string> viewFields, string strQuery, uint rowLimit, bool paged, bool makeDefault);

    }
}
