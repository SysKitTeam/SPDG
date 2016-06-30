using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator.SPModel;

namespace Acceleratio.SPDG.Generator
{
    public abstract class SPDGDataHelper
    {
        public abstract IEnumerable<SPDGWebApplication> GetWebApplications();

        public abstract IEnumerable<string> GetAllSiteCollections(Guid webApplicationId);

        public abstract void ValidateCredentials();
        public static SPDGDataHelper Create(GeneratorDefinitionBase definition)
        {
            string assemblyName = "";
            string typeName = "";
            if (!definition.IsClientObjectModel && DataGenerator.SupportsServer)
            {
                assemblyName = "Acceleratio.SPDG.Generator.Server";
                typeName = "Acceleratio.SPDG.Generator.Server.SPDGServerDataHelper";
            }
            else if (definition.IsClientObjectModel && DataGenerator.SupportsClient)
            {
                assemblyName = "Acceleratio.SPDG.Generator.Client";
                typeName = "Acceleratio.SPDG.Generator.Client.SPDGClientDataHelper";
            }
                        
            if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
            {
                var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                var type = assembly.GetType(typeName);
                return (SPDGDataHelper)Activator.CreateInstance(type, definition);
            }
            throw new InvalidOperationException();

        }
    }
}
