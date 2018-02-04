using System;
using System.IO;
using System.Text;

namespace VsSolutionRenamer.Business.SolutionBackup.Transactions
{
    internal class Request
    {
        internal bool Execute(Entities.Models.Files.Solution.Solution solution)
        {
            string datetimestamp = DateTime.Now.ToString("yyyyMMddhhmmssffff");
            var backupFileName = $"{solution.folderLocation}\\_backup\\{datetimestamp}_restore.bat";
            var backupFolderName = $"{solution.folderLocation}\\_backup\\";
            if (!Directory.Exists(backupFolderName))
                Directory.CreateDirectory(backupFolderName);
            /*using (StreamWriter writer = new StreamWriter(backupFileName))
            {
                writer.Write("@ECHO OFF");
            }*/
            Copy($"{solution.folderLocation}\\{solution.solutionName}.sln", $"{backupFolderName}{solution.solutionName}.sln");
            foreach (var projectFileName in solution.project)
            {

                if (File.Exists($"{solution.folderLocation}\\{projectFileName.projectLocation}"))
                {
                    if (!Directory.Exists(new FileInfo($"{ backupFolderName }{ projectFileName.projectLocation}").DirectoryName))
                    {
                        Directory.CreateDirectory(new FileInfo($"{ backupFolderName }{ projectFileName.projectLocation}").DirectoryName);
                    }
                    Copy($"{solution.folderLocation}\\{projectFileName.projectLocation}", $"{backupFolderName}{projectFileName.projectLocation}");
                }
            }
            //solution.project[0].projectLocation;
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
