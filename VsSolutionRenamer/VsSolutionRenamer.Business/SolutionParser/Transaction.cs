using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
