using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsSolutionRenamer.Business.GatheringFiles
{
    public class Transaction
    {
        public class GatherFiles : IProcess
        {
            public List<FileInfo> Execute(string source)
            {
                return new Transactions.Request().Execute(source);
            }
        }
    }
}
