using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceleratio.SPDG.Generator.Objects
{
    public class SPDGWebTemplate
    {
        public string Name { get; private set; }
        public string Title { get; private set; }

        public SPDGWebTemplate(string name, string title)
        {
            Name = name;
            Title = title;
        }
    }
}
