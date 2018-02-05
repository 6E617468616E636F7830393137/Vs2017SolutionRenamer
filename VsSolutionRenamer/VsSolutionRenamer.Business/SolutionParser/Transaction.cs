using VsSolutionRenamer.Entities.Models.Files.Solution;

namespace VsSolutionRenamer.Business.SolutionParser
{
    public class Transaction
    {
        public class ParseSolution : IProcess
        {
            public Entities.Models.Files.Solution.Solution Execute(string source)
            {
                return new Transactions.Request().Execute(source);
            }

            public Solution Execute(Solution solution, string updatedName)
            {
                return new Transactions.Request().Execute(solution, updatedName);
            }
        }
    }
}
