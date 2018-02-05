namespace VsSolutionRenamer.Business.SolutionParser
{
    public interface IProcess
    {
        Entities.Models.Files.Solution.Solution Execute(string source);
        Entities.Models.Files.Solution.Solution Execute(Entities.Models.Files.Solution.Solution solution, string updatedName);
    }
}
