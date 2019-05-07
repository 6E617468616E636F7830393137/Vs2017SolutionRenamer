using Log4net.Helper.Logging;
using System;
using System.IO;
using System.Linq;
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
        // Complete backup of solution
        internal string Execute(string sourceDirectory)
        {                       
            string[] tempDirectory = sourceDirectory.Split('\\');
            string bufferDirectory = $"{Path.GetTempPath()}SolutionRenamer\\{tempDirectory[tempDirectory.Length - 1]}";
            // Creates Unique directory
            Directory.CreateDirectory(bufferDirectory);
            //Now Create all of the directories
            string[] folders = Directory.GetDirectories(sourceDirectory, "*",
                SearchOption.AllDirectories);
            folders = folders.Where(p => !p.Contains(".git")).ToArray();
            folders = folders.Where(p => !p.Contains("TestResults")).ToArray();
            foreach (string dirPath in folders)
            {                
                    Directory.CreateDirectory(dirPath.Replace(sourceDirectory, bufferDirectory));
             
            }
            string[] files = Directory.GetFiles(sourceDirectory, "*.*",
                SearchOption.AllDirectories);
            files = files.Where(p => !p.Contains(".git")).ToArray();
            files = files.Where(p => !p.Contains("TestResults")).ToArray();
            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in files)
                try
                {
                    
                    File.Copy(newPath, newPath.Replace(sourceDirectory, bufferDirectory), true);
                    
                }
                catch (Exception ex)
                {
                    Logger.Error($"ERROR COPYING FILE : {ex}");
                }
            return bufferDirectory;

        }
        // Helper internal method --> Called from Execute method
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
