namespace Acceleratio.SPDG.Generator.SPModel
{
    public class SPDGWebTemplate
    {
        public string Name { get; private set; }
        public string Title { get; private set; }

        public SPDGWebTemplate(string name, string title)
        {
            Name = name;
            Title = title;
        }
    }
}
