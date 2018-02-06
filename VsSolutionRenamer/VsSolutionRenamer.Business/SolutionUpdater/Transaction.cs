using VsSolutionRenamer.Entities.Models.Files.Solution;

namespace VsSolutionRenamer.Business.SolutionUpdater
{
    public class Transaction
    {
        public class Execute : IProcess
        {            
            public bool SolutionBuilder(Solution solution)
            {
                return new Transactions.Request().SolutionBuilder(solution);
            }

            public bool SolutionMover(Solution solution)
            {
                return new Transactions.Request().SolutionMover(solution);
            }
        }
    }
}
