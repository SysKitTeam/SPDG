using System.Collections.Generic;

namespace Acceleratio.SPDG.Generator.Model
{
    public interface ISPDGListItemInfo
    {
        object this[string name] { get; set; }
        IEnumerable<string> GetAvailableFields();
    }
    
}
