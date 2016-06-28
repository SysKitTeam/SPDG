using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceleratio.SPDG.Generator.Model;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
{
    public class SPDGClientFile: SPDGFile
    {
        private readonly File _file;
        private readonly ClientContext _context;

        public SPDGClientFile(File file, ClientContext context)
        {
            _file = file;
            _context = context;
        }

        public override SPDGListItem Item
        {
            get
            {
                return new SPDGClientListItem(_file.ListItemAllFields, _context);
            }
        }
    }
}
