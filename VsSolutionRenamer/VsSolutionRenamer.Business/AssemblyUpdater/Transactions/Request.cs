using System.Collections.Generic;
using System.IO;
using VsSolutionRenamer.Entities.Models.Files.Projects;

namespace VsSolutionRenamer.Business.AssemblyUpdater.Transactions
{
    public class Request
    {
        internal bool ExecuteUpdater(ProjectSection projectSection, string originalName, string updatedName)
        {
            string rootDirectoryName = projectSection.currentFolder.Replace(originalName, updatedName);
            // Get Files in Directory
            List<string> filenames = new List<string>();
            filenames.AddRange(Directory.GetFiles($"{new FileInfo($"{rootDirectoryName}\\{projectSection.filename}").DirectoryName}", "*.*", SearchOption.AllDirectories));
            // Create Directory
            string updatedDirectoryName = $"{new FileInfo($"{projectSection.currentFolder}\\{projectSection.filename}").DirectoryName}".Replace(originalName, updatedName);
            
            if (!Directory.Exists(updatedDirectoryName))
            {
                //Directory.Move($"{new FileInfo($"{rootDirectoryName}\\{projectSection.filename}").DirectoryName}", $"{updatedDirectoryName}");
                FileCopyLibrary.Bll.FileCopyLibrary.IFileManager fileManager = new FileCopyLibrary.Bll.FileCopyLibrary.FileManager();
                fileManager.MoveFolder($"{new FileInfo($"{rootDirectoryName}\\{projectSection.filename}").DirectoryName}", $"{updatedDirectoryName}");
            }
            // Copy files to new directory
            
            return true;
        }
        //helper internal method --> Called from Execute method
        internal void Copy(string inputFIlePath, string outputFilePath)
        {
            int i;
            FileStream fin = new FileStream(inputFIlePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            FileStream fout = new FileStream(outputFilePath, FileMode.Create);
            do
            {
                i = fin.ReadByte();
                if (i != -1)
                    fout.WriteByte((byte)i);
            } while (i != -1);

            fin.Close();
            fout.Close();
        }
    }
}
