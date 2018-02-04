namespace VsSolutionRenamer.Business.SolutionBackup
{
    public class Transaction
    {
        public class BackupSolution : IProcess
        {
            public bool Execute(Entities.Models.Files.Solution.Solution source)
            {
                return new Transactions.Request().Execute(source);
            }
        }
    }
}
