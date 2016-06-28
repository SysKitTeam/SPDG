using System.Xml.Serialization;
using Microsoft.Azure.ActiveDirectory.GraphClient;

namespace Acceleratio.SPDG.Generator.Client
{
    public class ClientGeneratorDefinition : GeneratorDefinitionBase
    {
        public string TenantName { get; set; }
        
        public override bool IsClientObjectModel { get { return true; } }       
    }
}