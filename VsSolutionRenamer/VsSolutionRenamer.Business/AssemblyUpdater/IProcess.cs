using VsSolutionRenamer.Entities.Models.Files.Projects;

namespace VsSolutionRenamer.Business.AssemblyUpdater
{
    public interface IProcess
    {
        bool ExecuteAssemblyUpdater(ProjectSection projectSection, string originalName, string updatedName);
    }
}
