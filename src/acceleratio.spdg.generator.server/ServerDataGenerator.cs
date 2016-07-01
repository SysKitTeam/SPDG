using Acceleratio.SPDG.Generator.GenerationTasks;
using Acceleratio.SPDG.Generator.Server.GenerationTasks;
using Acceleratio.SPDG.Generator.SPModel;

namespace Acceleratio.SPDG.Generator.Server
{
    public partial class ServerDataGenerator : DataGenerator
    {       

        public ServerDataGenerator(ServerGeneratorDefinition definition) : base(definition)
        {
            RegisterTask<CreateUsersAndGroupsGenerationTask>();
            RegisterTask<CreateWebAppsAndSiteCollectionsTask>();            
            RegisterTask<SitesDataGenerationTask>();
            RegisterTask<ListsDataGenerationTask>();
            RegisterTask<FoldersDataGenerationTask>();
            RegisterTask<ItemsAndDocumentsDataGenerationTask>();
            RegisterTask<CreateContentTypesGenerationTask>();
            RegisterTask<AssociateOotbWorkflowsGenerationTask>();
            RegisterTask<AddDeclarativeAssociateWorkflowsGenerationTask>();            
            RegisterTask<PermissionsDataGenerationTask>();
            
        }

        protected new ServerGeneratorDefinition WorkingDefinition
        {
            get { return (ServerGeneratorDefinition) base.WorkingDefinition; }
        }

        protected override SPDGObjectsFactory CreateObjectsFactory()
        {
            return new SPDGServerObjectsFactory();
        }              

        public override bool startDataGeneration()
        {
            if (!WorkingDefinition.CredentialsOfCurrentUser)
            {
                var parts = WorkingDefinition.Username.Split('\\');

                if (Utils.ImpersonateValidUser(parts[1], parts[0], WorkingDefinition.Password))
                {
                    try
                    {
                        return base.startDataGeneration();
                    }
                    finally
                    {
                        Utils.UndoImpersonation();
                    }   
                }
            }
            else
            {
                return base.startDataGeneration();
            }
            return false;
        }
    }
}