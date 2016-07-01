using System.Collections.Generic;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator.GenerationTasks
{
    public interface IDataGenerationTaskOwner
    {
        GeneratorDefinitionBase WorkingDefinition { get; }
        SPDGObjectsFactory ObjectsFactory { get; }
        List<SiteCollInfo> WorkingSiteCollections { get; }
        void IncrementCurrentTaskProgress(string message, int incrementInProgress = 1);
    }
}