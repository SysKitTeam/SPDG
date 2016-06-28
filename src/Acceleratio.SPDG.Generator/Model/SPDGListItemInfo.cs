using System.Collections.Generic;
using System.Linq;

namespace Acceleratio.SPDG.Generator.Model
{
    class SPDGListItemInfo : ISPDGListItemInfo
    {
        readonly Dictionary<string, object> _fieldValues=new Dictionary<string, object>();

        public object this[string fieldName]
        {
            get { return _fieldValues[fieldName]; }
            set { _fieldValues[fieldName] = value; }
        }

        public IEnumerable<string> GetAvailableFields()
        {
            return _fieldValues.Keys.ToList();
        }
    }
}
