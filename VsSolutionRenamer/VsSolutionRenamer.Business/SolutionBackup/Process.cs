namespace VsSolutionRenamer.Business.SolutionBackup
{
    public class Process
    {
        // Declaration
        private readonly IProcess _process;
        // Constructor
        public Process(IProcess concreteImplementation)
        {
            _process = concreteImplementation;
        }
        // Implementation
        public bool Execute(Entities.Models.Files.Solution.Solution source)
        {
            return _process.Execute(source);
        }
    }
}
