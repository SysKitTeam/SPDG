using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Acceleratio.SPDG.Generator.Objects
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
