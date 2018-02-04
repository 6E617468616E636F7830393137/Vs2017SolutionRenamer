using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VsSolutionRenamer.Entities.Models.FileSelect
{    
    public class FileSelect
    {        
        public FileInfo FileSelector()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Solution file (*.sln)|*.sln";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            DialogResult result = openFileDialog1.ShowDialog();
            return new FileInfo(openFileDialog1.FileName);
        }
    }
}
