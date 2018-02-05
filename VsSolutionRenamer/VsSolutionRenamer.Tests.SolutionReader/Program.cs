using System;
using System.Collections.Generic;
using System.IO;
using Log = Log4net.Helper.Logging.Logger;

namespace VsSolutionRenamer.Tests.SolutionReader
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(": : : : :  VS Solution Reader  : : : : :");
            Log.Info(": : : : :  VS Solution Reader  : : : : :");
            Entities.Models.Files.Solution.Solution solution = new Entities.Models.Files.Solution.Solution();
            List<Entities.Models.Files.Projects.Projects> projects = new List<Entities.Models.Files.Projects.Projects>();
            // Open file dialog to select solution folder
            var select = new Entities.Models.FileSelect.FileSelect();
            var source = select.FileSelector();
            solution.solutionName = source.Name.Replace(source.Extension,string.Empty);
            solution.folderLocation = source.DirectoryName;

            Console.WriteLine($"Folder selected : {source}");
            Log.Info($"Folder selected : {source}");
            
            string solutionFile = source.FullName;
            
            if (!solutionFile.Equals(""))
            {
                var parsedSolution = new Business.SolutionParser.Process(new Business.SolutionParser.Transaction.ParseSolution()).Execute(solutionFile);
                solution.header = parsedSolution.header;
                solution.project = parsedSolution.project;
                solution.global = parsedSolution.global;                
            }
            // Building projects structure (XML) independent of the Solutions structure (non-XML)
            foreach (var tempProject in solution.project)
            {
                if (File.Exists($"{solution.folderLocation}\\{tempProject.projectLocation}"))
                {
                    projects.Add(new Entities.Models.Files.Projects.Projects
                    {
                        fileName = $"{solution.folderLocation}\\{tempProject.projectLocation}"
                    });
                }
            }
            
            for (int i = 0; i < projects.Count; i++)
            {
                projects[i] = new Business.ProjectUpdater.XmlParser.Process(new Business.ProjectUpdater.XmlParser.Transaction.ParseProject()).Execute(projects[i]);
                Console.WriteLine($"Project Folder : {new FileInfo(projects[i].fileName).DirectoryName}");
            }
            Console.WriteLine($"Current Solution Name : {solution.solutionName}");
            Console.Write($"Enter new name : ");
            var updatedName = Console.ReadLine();
            //Backup Files
            var isBackupSuccess = new Business.SolutionBackup.Process(new Business.SolutionBackup.Transaction.BackupSolution()).Execute(solution);            
            if (isBackupSuccess)
            {
                for (int i = 0; i < projects.Count; i++)
                {
                    projects[i] = new Business.ProjectUpdater.XmlParser.Process(new Business.ProjectUpdater.XmlParser.Transaction.ParseProject()).Execute(projects[i], solution.solutionName, updatedName);
                }
                solution = new Business.SolutionParser.Process(new Business.SolutionParser.Transaction.ParseSolution()).Execute(solution, updatedName);

                /* Solution Updater File Save */
                using (StreamWriter solutionOutput = new StreamWriter($"{solution.folderLocation}\\{solution.solutionName}.sln"))
                {
                    foreach (var headerString in solution.header.headerData)
                    {
                        solutionOutput.WriteLine(headerString);
                    }
                    foreach (var projectString in solution.project)
                    {
                        // Output of Virtual Folder section
                        if (!projectString.doesFolderExist && projectString.projectSection == null)
                        {                           
                            solutionOutput.WriteLine($"{projectString.projectData}\r\n{projectString.projectDataEnd.TrimEnd()}");
                        }
                        // Output of Virtual Folder section with imported files
                        else if (!projectString.doesFolderExist && projectString.projectSection != null)
                        {
                            solutionOutput.WriteLine($"{projectString.projectData}");
                            foreach (var projectSectionString in projectString.projectSection)
                            {
                                // Checking for end of project section
                                if (!projectSectionString.isEndOfProjectSection)
                                {
                                    solutionOutput.WriteLine($"{projectSectionString.filename}={projectSectionString.equalsFilename}");
                                }
                                else
                                {
                                    solutionOutput.WriteLine($"{projectSectionString.filename}");
                                }
                            }
                            solutionOutput.WriteLine($"{projectString.projectDataEnd}");
                        }
                        else if (projectString.doesFolderExist && projectString.projectSection == null)
                        {
                            solutionOutput.WriteLine($"{projectString.updatedProjectData}");
                        }
                    }
                    foreach (var globalString in solution.global)
                    {
                        solutionOutput.WriteLine(globalString.globalSection);
                    }
                }
                // Check if Directory doesn't exist and create if true
                /*if (!Directory.Exists($"{solution.updatedFolderLocation}"))
                {
                    Directory.CreateDirectory($"{solution.updatedFolderLocation}");
                }*/
                // Move Project Folder
                Directory.Move($"{solution.folderLocation}", $"{solution.updatedFolderLocation}");
                // Check if File exists and delete if it does
                if (File.Exists($"{solution.updatedFolderLocation}\\{solution.updatedSolutionName}.sln"))
                {
                    File.Delete($"{solution.updatedFolderLocation}\\{solution.updatedSolutionName}.sln");
                }
                // Move file to new location
                File.Move($"{solution.updatedFolderLocation}\\{solution.solutionName}.sln", $"{solution.updatedFolderLocation}\\{solution.updatedSolutionName}.sln");


            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
