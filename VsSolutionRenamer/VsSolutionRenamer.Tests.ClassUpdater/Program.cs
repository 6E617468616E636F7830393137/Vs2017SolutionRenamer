using System;
using System.Collections.Generic;
using System.IO;
using Log = Log4net.Helper.Logging.Logger;

namespace VsSolutionRenamer.Tests.ClassUpdater
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(": : : : :  VS Class Updater  : : : : :");
            Log.Info(": : : : : VS Class Updater : : : : :");
            Entities.Models.Files.Solution.Solution solution = new Entities.Models.Files.Solution.Solution();
            List<Entities.Models.Files.Projects.Projects> projects = new List<Entities.Models.Files.Projects.Projects>();
            // Open file dialog to select solution folder
            var select = new Entities.Models.FileSelect.FileSelect();
            var source = select.FileSelector();
            solution.solutionName = source.Name.Replace(source.Extension, string.Empty);
            solution.folderLocation = source.DirectoryName;            

            string solutionFile = source.FullName;

            if (!solutionFile.Equals(""))
            {
                var parsedSolution = new Business.SolutionParser.Process(new Business.SolutionParser.Transaction.ParseSolution()).Execute(solutionFile);
                solution.header = parsedSolution.header;
                solution.project = parsedSolution.project;
                solution.global = parsedSolution.global;
            }

            string[] files = Directory.GetFiles(solution.folderLocation, "*.cs", SearchOption.AllDirectories);
            Log.Info($": : : : : Number of CS files : {files.Length} : : : : :");
            Console.WriteLine($"Number of CS files : {files.Length}");
            Console.Write($"Enter new namespace : ");
            var updatedName = Console.ReadLine();
            new Business.ClassUpdater.Process(new Business.ClassUpdater.Transaction.ParseClasses()).ExecuteClassUpdater(files, solution.solutionName, updatedName);
        }
    }
}
