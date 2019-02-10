using System;
using System.IO;
using Log = Log4net.Helper.Logging.Logger;

namespace VsSolutionRenamer.Business.SolutionUpdater.Transactions
{
    internal class Request
    {        
        internal bool SolutionBuilder(Entities.Models.Files.Solution.Solution solution)
        {
            /* Solution Updater File Save */
            using (StreamWriter solutionOutput = new StreamWriter($"{solution.folderLocation}\\{solution.solutionName}.sln"))
            {
                Log.Info($": : : : : Build Header of Solution : {solution.solutionName}.sln  : : : : :");
                foreach (var headerString in solution.header.headerData)
                {
                    solutionOutput.WriteLine(headerString);
                }
                Log.Info($": : : : : Build Project section of Solution : {solution.solutionName}.sln  : : : : :");
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
                        Log.Info($": : : : : Build Nested Project Section of Solution : {solution.solutionName}.sln  : : : : :");
                        solutionOutput.WriteLine($"{projectString.projectData}");
                        foreach (var projectSectionString in projectString.projectSection)
                        {
                            // Checking for end of project section
                            if (!projectSectionString.isEndOfProjectSection)
                            {
                                if (projectSectionString.filename.Contains(solution.solutionName))
                                {
                                    projectSectionString.filename = projectSectionString.filename.Replace(solution.solutionName, solution.updatedSolutionName);
                                }
                                if (projectSectionString.equalsFilename.Contains(solution.solutionName))
                                {
                                    projectSectionString.equalsFilename = projectSectionString.equalsFilename.Replace(solution.solutionName, solution.updatedSolutionName);
                                }
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
            Log.Info($": : : : : Solution Build Completed : {solution.solutionName}.sln  : : : : :");
            return true;

        }
        // Solution File Updater
        internal bool SolutionMover(Entities.Models.Files.Solution.Solution solution)
        {
            // Check if Directory doesn't exist and create if true
            /*if (!Directory.Exists($"{solution.updatedFolderLocation}"))
            {
                Directory.CreateDirectory($"{solution.updatedFolderLocation}");
            }*/
            // Move Project Folder
            try
            {
                Log.Info($": : : : : Moving Directory to Updated location : {solution.solutionName}.sln  : : : : :");
                Directory.Move($"{solution.folderLocation}", $"{solution.updatedFolderLocation}");
                Log.Info($": : : : : Renaming Solution filename : {solution.solutionName}.sln to {solution.updatedSolutionName}.sln : : : : :");
                // Check if File exists and delete if it does
                if (File.Exists($"{solution.updatedFolderLocation}\\{solution.updatedSolutionName}.sln"))
                {
                    File.Delete($"{solution.updatedFolderLocation}\\{solution.updatedSolutionName}.sln");
                }
                // Move file to new location
                File.Move($"{solution.updatedFolderLocation}\\{solution.solutionName}.sln", $"{solution.updatedFolderLocation}\\{solution.updatedSolutionName}.sln");
            }
            catch (Exception ex)
            {
                Log.Error($": : : : : Renaming Solution filename : {solution.solutionName}.sln to {solution.updatedSolutionName}.sln : : : : :");
                Log.Error($": : : : : {ex.ToString()} : : : : :");
                return false;
            }
            return true;
        }
    }
}
