using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsSolutionRenamer.Entities.Models.Files.Projects;

namespace VsSolutionRenamer.Business.ClassUpdater
{
    public class Transaction
    {
        public class ParseClasses : IProcess
        {
            public bool ExecuteClassUpdater(ArrayList filenames, string originalNamespace, string updatedNameSpace)
            {
                return new Transactions.Request().ExecuteUpdater(filenames, originalNamespace, updatedNameSpace);
            }
        }
    }
}
