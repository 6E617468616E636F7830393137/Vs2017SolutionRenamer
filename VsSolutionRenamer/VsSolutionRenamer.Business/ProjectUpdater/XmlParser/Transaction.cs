using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsSolutionRenamer.Entities.Models.Files.Projects;

namespace VsSolutionRenamer.Business.ProjectUpdater.XmlParser
{
    public class Transaction
    {
        public class ParseProject : IProcess
        {
            public Projects Execute(Projects data)
            {
                return new Transactions.Request().ExecuteReadNodes(data);
            }
            public Projects Execute(Projects data, string originalNamespace, string updatedNamespaceAssemblyName)
            {
                return new Transactions.Request().ExecuteUpdateNodes(data, originalNamespace, updatedNamespaceAssemblyName);
            }
        }
    }
}
