using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Server.SPModel
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
