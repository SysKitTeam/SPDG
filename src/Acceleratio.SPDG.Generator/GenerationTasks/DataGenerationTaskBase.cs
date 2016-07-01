namespace Acceleratio.SPDG.Generator.GenerationTasks
{
    public abstract class DataGenerationTaskBase
    {
        public abstract void Execute();
        public abstract int CalculateTotalSteps();
        public abstract string Title { get; }

        public virtual bool IsActive
        {
            get
            {
                return CalculateTotalSteps() > 0;
            }
        }

        private readonly IDataGenerationTaskOwner _owner;

        public DataGenerationTaskBase(IDataGenerationTaskOwner owner)
        {
            _owner = owner;
        }

        protected GeneratorDefinitionBase WorkingDefinition
        {
            get
            {
                return _owner.WorkingDefinition;
            }
        }

        protected IDataGenerationTaskOwner Owner
        {
            get { return _owner; }
        }
    }
}
