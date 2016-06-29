namespace Acceleratio.SPDG.Generator
{
    public class ServerGeneratorDefinition : GeneratorDefinitionBase
    {       
        public override bool IsClientObjectModel { get { return false; } }
        public string SharePointURL { get; set; }              
        public string WebAppOwnerLogin { get; set; }
        public string WebAppOwnerPassword { get; set; }
        public string WebAppOwnerEmail { get; set; }
        public string DatabaseServer { get; set; }
        public string ADDomainName { get; set; }
        public string ADOrganizationalUnit { get; set; }
        public int CreateNewWebApplications { get; set; }
        public string UseExistingWebApplication { get; set; }
        public string UseExistingWebApplicationName { get; set; }
        public bool CreateOutOfTheBoxWorkflowsToList { get; set; }
        public bool AttachCustomWorkflowToList { get; set; }
    }
}