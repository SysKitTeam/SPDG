using System;
using System.Collections.Generic;
using Acceleratio.SPDG.Generator.GenerationTasks;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator.Client.GenerationTasks
{
    class CreateSiteCollectionsGenerationTask : DataGenerationTaskBase
    {
        public override string Title
        {
            get { return "Creating Site Collections"; }
        }

        public CreateSiteCollectionsGenerationTask(ClientDataGenerator owner) : base(owner)
        {
            _generator = owner;
        }

        private readonly ClientDataGenerator _generator;

        new ClientGeneratorDefinition WorkingDefinition
        {
            get { return (ClientGeneratorDefinition) base.WorkingDefinition; }
        }

        public override int CalculateTotalSteps()
        {
            return WorkingDefinition.CreateNewSiteCollections;
        }

        public override void Execute()
        {
            var helper = _generator.DataHelper;
            HashSet<string> existingSites = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var siteCollectionUrl in helper.GetAllSiteCollections(Guid.Empty))
            {
                existingSites.Add(siteCollectionUrl);
            }
            for (int s = 0; s < WorkingDefinition.CreateNewSiteCollections; s++)
            {
                string siteName = "";
                string siteUrl = "";
                string leafName = "";
                int i = 0;
                string baseName = "";
                do
                {
                    siteName = SampleData.GetRandomName(SampleData.Companies, SampleData.Offices, null, ref i, out baseName);
                    leafName = Utils.GenerateSlug(siteName, 25);
                    siteUrl = string.Format("https://{0}.sharepoint.com/sites/{1}", WorkingDefinition.TenantName, leafName);
                } while (existingSites.Contains(siteUrl));

                existingSites.Add(siteUrl);

                Owner.IncrementCurrentTaskProgress("Creating site collection '" + siteUrl + "'");
                var owner = WorkingDefinition.SiteCollOwnerLogin;
                if (string.IsNullOrEmpty(owner))
                {
                    owner = WorkingDefinition.Username;
                }
                helper.CreateNewSiteCollection(siteName, siteUrl, owner);
                SiteCollInfo siteCollInfo = new SiteCollInfo();
                siteCollInfo.URL = siteUrl;
                Owner.WorkingSiteCollections.Add(siteCollInfo);
            }
        }        
    }
}
