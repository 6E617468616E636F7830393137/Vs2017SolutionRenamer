namespace VsSolutionRenamer.Business.SolutionUpdater
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
        public bool ExecuteUpdater(Entities.Models.Files.Solution.Solution solution)
        {
            return _process.SolutionBuilder(solution);
        }
        public bool ExecuteMove(Entities.Models.Files.Solution.Solution solution)
        {
            return _process.SolutionMover(solution);
        }
    }
}
