namespace VsSolutionRenamer.Business.SolutionUpdater
{
    public interface IProcess
    {
        bool SolutionBuilder(Entities.Models.Files.Solution.Solution solution);
        bool SolutionMover(Entities.Models.Files.Solution.Solution solution);
    }
}
