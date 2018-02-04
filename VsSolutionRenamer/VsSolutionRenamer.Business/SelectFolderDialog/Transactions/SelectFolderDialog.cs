using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsSolutionRenamer.Entities.Models.FolderSelect;
using System.Windows.Forms;

namespace VsSolutionRenamer.Business.SelectFolderDialog.Transactions
{
    internal class SelectFolderDialog
    {
        internal string Execute(string Location)
        {
            FolderSelectDialog fsd = new FolderSelectDialog();
            fsd.Title = "Select Wallpaper Folder to Gather Images";
            if (Location.Equals(""))
            {
                fsd.InitialDirectory = System.Environment.GetEnvironmentVariable("USERPROFILE") + @"\Desktop";
            }
            else
            {
                fsd.InitialDirectory = Location;
            }
            fsd.ShowDialog();

            return fsd.FileName;

        }
    }
}
