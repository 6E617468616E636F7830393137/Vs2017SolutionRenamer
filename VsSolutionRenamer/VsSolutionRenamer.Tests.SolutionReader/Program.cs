﻿using System;
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

                if (new Business.SolutionUpdater.Process(new Business.SolutionUpdater.Transaction.Execute()).ExecuteUpdater(solution))
                {
                    Console.WriteLine($": : : : : Solution Updated Successfully : : : : :");
                    if (new Business.SolutionUpdater.Process(new Business.SolutionUpdater.Transaction.Execute()).ExecuteMove(solution))
                    {
                        Console.WriteLine($": : : : : Solution Moved Successfully : : : : :");
                    }
                    else
                    {
                        Console.WriteLine($": : : : : ERROR OCCURED DURING MOVE : : : : :");
                    }
                }
                else
                {
                    Console.WriteLine($": : : : : ERROR OCCURED UPDATE : : : : :");
                }
                

            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
