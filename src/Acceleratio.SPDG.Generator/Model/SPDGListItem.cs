using System.Collections.Generic;

namespace Acceleratio.SPDG.Generator.Model
{
    public abstract class SPDGListItem : SPDGSecurableObject, ISPDGListItemInfo
    {
        public abstract object this[string fieldName] { get; set; }
        public abstract IEnumerable<string> GetAvailableFields();

        public abstract int Id { get; }
        public abstract  string DisplayName { get; }
        public abstract void Update();

    }
}
