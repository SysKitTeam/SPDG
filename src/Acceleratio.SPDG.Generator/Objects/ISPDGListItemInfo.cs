using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator.Objects
{
    public interface ISPDGListItemInfo
    {
        object this[string name] { get; set; }
        IEnumerable<string> GetAvailableFields();
    }
    
}
