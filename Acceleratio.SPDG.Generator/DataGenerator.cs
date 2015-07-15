using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        GeneratorDefinition workingDefinition;
        List<String> workingSiteCollections = new List<string>();

        public DataGenerator(GeneratorDefinition definition)
        {
            workingDefinition = definition;
        }

        public bool startDataGeneration()
        {
            ResolveWebAppsAndSiteCollections();

            return true;
        }
    }
}
