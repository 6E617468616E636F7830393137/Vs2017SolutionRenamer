using VsSolutionRenamer.Entities.Models.Files.Projects;

namespace VsSolutionRenamer.Business.AssemblyUpdater
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
        public bool ExecuteAssemblyUpdater(ProjectSection projectSection, string originalName, string updatedName)
        {
            return _process.ExecuteAssemblyUpdater(projectSection, originalName, updatedName);
        }
    }
}
