using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects.Server
{
    class SPDGServerList :SPDGList
    {
        private readonly SPList _spList;

        public override string Title
        {
            get { return _spList.Title; }
        }

        public override string DefaultViewUrl
        {
            get { return _spList.DefaultViewUrl; }
        }

        public override SPDGFolder RootFolder
        {
            get
            {
                return  new SPDGServerFolder(_spList.RootFolder);
            }
        }

        public SPDGServerList(SPList spList)
        {
            _spList = spList;
        }

        
    }
}
