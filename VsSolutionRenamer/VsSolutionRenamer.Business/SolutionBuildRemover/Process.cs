namespace VsSolutionRenamer.Business.SolutionBuildRemover
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
        public bool Execute(string source)
        {
            return _process.PurgeBinObjFoldersFiles(source);
        }
    }
}
