using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsSolutionRenamer.Business.SelectFolderDialog
{
    public class Transaction
    {
        public class Execute : ISelectFolderDialog
        {
            public string SelectFolderDialog(string Location)
            {
                return new Business.SelectFolderDialog.Transactions.SelectFolderDialog().Execute(Location);
            }
        }
    }
}
