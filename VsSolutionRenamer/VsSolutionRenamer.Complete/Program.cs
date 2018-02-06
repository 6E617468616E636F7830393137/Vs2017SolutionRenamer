using System;
using System.Collections.Generic;
using System.IO;
using Log = Log4net.Helper.Logging.Logger;
namespace VsSolutionRenamer.Complete
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(": : : : :  VS Rename Complete  : : : : :");
            Log.Info(": : : : :  VS Rename Complete  : : : : :");
            // Initialize objects for Solution Object
            Entities.Models.Files.Solution.Solution solution = new Entities.Models.Files.Solution.Solution();
            List<Entities.Models.Files.Projects.Projects> projects = new List<Entities.Models.Files.Projects.Projects>();
            // Open file dialog to select solution folder
            var select = new Entities.Models.FileSelect.FileSelect();
            var source = select.FileSelector();
            solution.solutionName = source.Name.Replace(source.Extension, string.Empty);
            solution.folderLocation = source.DirectoryName;
            // Output
            Console.WriteLine($"Folder selected : {source}");
            Log.Info($"Folder selected : {source}");
            // Check to make slected file is not empty or null            
            if (!source.FullName.Equals(""))
            {
                // Initialize solution values
                var parsedSolution = new Business.SolutionParser.Process(new Business.SolutionParser.Transaction.ParseSolution()).Execute(source.FullName);
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
            //Build Project array object by parsing files
            for (int i = 0; i < projects.Count; i++)
            {
                projects[i] = new Business.ProjectUpdater.XmlParser.Process(new Business.ProjectUpdater.XmlParser.Transaction.ParseProject()).Execute(projects[i]);
                Console.WriteLine($"Project Folder : {new FileInfo(projects[i].fileName).DirectoryName}");
            }
            // Input new namespace
            Console.WriteLine($"Current Solution Name : {solution.solutionName}");
            Console.Write($"Enter new name : ");
            var updatedName = Console.ReadLine();
            //Backup Files
            // TODO extend to include CS files
            var isBackupSuccess = new Business.SolutionBackup.Process(new Business.SolutionBackup.Transaction.BackupSolution()).Execute(solution);
            if (isBackupSuccess)
            {
                string[] files = Directory.GetFiles(solution.folderLocation, "*.cs", SearchOption.AllDirectories);
                Log.Info($": : : : : Number of CS files : {files.Length} : : : : :");
                Console.WriteLine($"Number of CS files : {files.Length}");
                new Business.ClassUpdater.Process(new Business.ClassUpdater.Transaction.ParseClasses()).ExecuteClassUpdater(files, solution.solutionName, updatedName);

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

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            } // Closes if
        }
    }
}
