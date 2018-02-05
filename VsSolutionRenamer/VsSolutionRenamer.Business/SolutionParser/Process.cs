namespace VsSolutionRenamer.Business.SolutionParser
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
        public Entities.Models.Files.Solution.Solution Execute(string source)
        {
            return _process.Execute(source);
        }
        public Entities.Models.Files.Solution.Solution Execute(Entities.Models.Files.Solution.Solution solution, string updatedName)
        {
            return _process.Execute(solution, updatedName);
        }
    }
}
