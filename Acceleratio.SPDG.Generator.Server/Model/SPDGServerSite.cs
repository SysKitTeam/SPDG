using System;
using Acceleratio.SPDG.Generator.Model;
using Acceleratio.SPDG.Generator.Objects.Server;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Server.Model
{
    class SPDGServerSite : SPDGSite
    {
        private SPSite _spSite;
        private SPDGServerWeb _rootWeb;
        public SPDGServerSite(SPSite sPSite)
        {
            this._spSite = sPSite;
            _rootWeb = new SPDGServerWeb(sPSite.RootWeb);
        }

        public override SPDGWeb OpenWeb(Guid id)
        {
            return new SPDGServerWeb(_spSite.OpenWeb(id));
        }

        public override SPDGWeb RootWeb
        {
            get
            {
                return _rootWeb;
            }
        }
        

        public override void Dispose()
        {
           _spSite.Dispose();
        }
    }
}