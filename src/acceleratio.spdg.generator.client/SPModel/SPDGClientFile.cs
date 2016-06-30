using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Client.SPModel
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
