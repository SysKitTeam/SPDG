using Acceleratio.SPDG.Generator.Model;
using Acceleratio.SPDG.Generator.Objects.Server;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Server.Model
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
