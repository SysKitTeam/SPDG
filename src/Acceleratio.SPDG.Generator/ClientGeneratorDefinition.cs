namespace Acceleratio.SPDG.Generator
{
    public class ClientGeneratorDefinition : GeneratorDefinitionBase
    {
        public string TenantName { get; set; }
        
        public override bool IsClientObjectModel { get { return true; } }
        
    }
}