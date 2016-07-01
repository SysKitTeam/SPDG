using Acceleratio.SPDG.Generator.Client.GenerationTasks;
using Acceleratio.SPDG.Generator.GenerationTasks;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Client
{
    public partial class ClientDataGenerator : DataGenerator
    {
        private SPDGClientDataHelper _dataHelper;

        public SPDGClientDataHelper DataHelper
        {
            get
            {
                if (_dataHelper == null)
                {
                    _dataHelper = new SPDGClientDataHelper(WorkingDefinition);
                }
                return _dataHelper;
            }
        }

        protected new ClientGeneratorDefinition WorkingDefinition
        {
            get { return (ClientGeneratorDefinition)base.WorkingDefinition; }
        }

        public ClientDataGenerator(ClientGeneratorDefinition definition) : base(definition)
        {
            RegisterTask<CreateUsersAndGroupsGenerationTask>();
            RegisterTask<CreateSiteCollectionsGenerationTask>();
            RegisterTask<SitesDataGenerationTask>();
            RegisterTask<ListsDataGenerationTask>();
            RegisterTask<FoldersDataGenerationTask>();
            RegisterTask<ItemsAndDocumentsDataGenerationTask>();
            RegisterTask<PermissionsDataGenerationTask>();

        }

        protected override SPDGObjectsFactory CreateObjectsFactory()
        {
            return new SPDGClientObjectsFactory(new SharePointOnlineCredentials(WorkingDefinition.Username, Utils.StringToSecureString(WorkingDefinition.Password)));
        }
    }
}