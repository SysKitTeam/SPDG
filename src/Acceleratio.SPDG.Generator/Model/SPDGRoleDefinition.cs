namespace Acceleratio.SPDG.Generator.Model
{
    public abstract class SPDGRoleDefinition
    {
        public abstract int ID { get; }
        public abstract  string Name { get; }

        public abstract  bool IsGuestRole { get; }

    }
}
