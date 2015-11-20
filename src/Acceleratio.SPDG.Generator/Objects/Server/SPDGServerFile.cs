using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects.Server
{
    class SPDGServerFile : SPDGFile
    {
        private readonly SPFile _file;

        public SPDGServerFile(SPFile file)
        {
            _file = file;
        }

        public override SPDGListItem Item
        {
            get { return new SPDGServerListItem(_file.Item); }            
        }
    }
}
