namespace VsSolutionRenamer.Business.SolutionBuildRemover
{
    public interface IProcess
    {
        bool PurgeBinObjFoldersFiles(string source);
    }
}
