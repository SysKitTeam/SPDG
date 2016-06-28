
namespace Acceleratio.SPDG.Generator.Model
{
    public class SPDGFieldInfo
    {
        public SPDGFieldType FieldType { get; set; }
        public string DisplayName { get; set; }

        public SPDGFieldInfo(string displayName, SPDGFieldType fieldType)
        {
            DisplayName = displayName;
            FieldType = fieldType;
        }
    }

    public enum SPDGFieldType
    {
        Text,
        DateTime,
        Integer
    }
}
