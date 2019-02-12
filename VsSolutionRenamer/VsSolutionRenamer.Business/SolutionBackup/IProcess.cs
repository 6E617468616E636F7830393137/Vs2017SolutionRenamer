namespace VsSolutionRenamer.Business.SolutionBackup
{
    public interface IProcess
    {
        bool Execute(Entities.Models.Files.Solution.Solution source);
        // Creates Temp Folder and copies solution to temp folder
        string Execute(string sourceDirectory);
    }
}
