using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsSolutionRenamer.Business.SelectFolderDialog
{
    public class SelectFolderDialog
    {
        private readonly ISelectFolderDialog _selectFolderDialog;
        //Constructor
        public SelectFolderDialog(ISelectFolderDialog concreteImplementation)
        {
            _selectFolderDialog = concreteImplementation;
        }
        public string Execute(string Location)
        {
            return _selectFolderDialog.SelectFolderDialog(Location);
        }
    }
}
