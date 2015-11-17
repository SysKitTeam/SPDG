using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects
{
    public class SPDGFieldInfo
    {
        public SPFieldType FieldType { get; set; }
        public string DisplayName { get; set; }

        public SPDGFieldInfo(string displayName, SPFieldType fieldType)
        {
            DisplayName = displayName;
            FieldType = fieldType;
        }
    }
}
