using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator.Objects
{
    public class SPDGField
    {
        public string Title { get; private set; }
        public string InternalName { get; private set; }        
        public SPDGField(string title, string internalName)
        {
            Title = title;
            InternalName = internalName;            
        }
    }
}
