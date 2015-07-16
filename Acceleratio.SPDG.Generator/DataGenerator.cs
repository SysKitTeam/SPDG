using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        GeneratorDefinition workingDefinition;
        List<SiteCollInfo> workingSiteCollections = new List<SiteCollInfo>();

        public DataGenerator(GeneratorDefinition definition)
        {
            workingDefinition = definition;
        }

        public bool startDataGeneration()
        {

            //Creates or sets Web applications and Site Collections
            ResolveWebAppsAndSiteCollections();

            //Create sites in previously defined Site Collections
            CreateSites();

            //Create lists and libraries
            CreateLists();

            //Create folders with nested folder levels
            CreateFolders();

            return true;
        }
    }
}
