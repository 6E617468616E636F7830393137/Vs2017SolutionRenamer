using VsSolutionRenamer.Entities.Models.Files.Projects;

namespace VsSolutionRenamer.Business.AssemblyUpdater
{
    public class Transaction
    {
        public class UpdateAssemblies : IProcess
        {
            public bool ExecuteAssemblyUpdater(ProjectSection projectSection, string originalName, string updatedName)
            {
                return new Transactions.Request().ExecuteUpdater(projectSection, originalName, updatedName);
            }
        }

    }
}
