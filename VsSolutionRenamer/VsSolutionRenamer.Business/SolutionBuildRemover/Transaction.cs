namespace VsSolutionRenamer.Business.SolutionBuildRemover
{
    public class Transaction
    {
        public class PreviousBuildPurge : IProcess
        {            
            public bool PurgeBinObjFoldersFiles(string source)
            {
                return new Transactions.Request().Execute(source);
            }
        }
    }
}
