namespace VsSolutionRenamer.Business.SolutionBackup
{
    public interface IProcess
    {
        bool Execute(Entities.Models.Files.Solution.Solution source);
    }
}
