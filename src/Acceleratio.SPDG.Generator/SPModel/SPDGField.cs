namespace Acceleratio.SPDG.Generator.SPModel
{
    public class SPDGField
    {
        public string Title { get; private set; }
        public string InternalName { get; private set; }        
        public SPDGField(string title, string internalName)
        {
            Title = title;
            InternalName = internalName;            
        }
    }
}
