using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace Acceleratio.SPDG.Generator.Server
{
    class SPDGServerDataHelper : SPDGDataHelper
    {
        private readonly GeneratorDefinitionBase _generatorDefinition;

        public SPDGServerDataHelper(GeneratorDefinitionBase definition)
        {
            _generatorDefinition = definition;
        }

        public override IEnumerable<SPDGWebApplication> GetWebApplications()
        {
            SPWebService spWebService = SPWebService.ContentService;
            SPWebApplicationCollection webAppColl = spWebService.WebApplications;
            

            return webAppColl.Select(x => new SPDGWebApplication() {Id = x.Id, Name = x.Name});

        }

        public override IEnumerable<string> GetAllSiteCollections(Guid webApplicationId)
        {
            
            SPWebService spWebService = SPWebService.ContentService;
            SPWebApplication webApp = spWebService.WebApplications.First(a => a.Id == webApplicationId);

            foreach (SPSite siteColl in webApp.Sites)
            {
                yield return siteColl.Url;                
            }
            
        }
    }
}
