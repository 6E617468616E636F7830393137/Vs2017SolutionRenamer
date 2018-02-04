﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VsSolutionRenamer.Business.SolutionParser.Transactions
{
    internal class Request
    {
        internal Entities.Models.Files.Solution.Solution Execute(string source)
        {
            //Determine working directory
            FileInfo solutionLocation = new FileInfo(source);
            string workingDirectory = solutionLocation.DirectoryName;
            // Initialize Solution component header value - EXAMPLE OF HOW NOT TO CREATE OBJECT LISTS
            Entities.Models.Files.Solution.Header header = new Entities.Models.Files.Solution.Header();
            header.headerData = new List<string>();
            // Initialize other Solution values - CORRECT EXAMPLE OF HOW TO CREATE OBJECT LISTS
            List<Entities.Models.Files.Solution.Project> project = new List<Entities.Models.Files.Solution.Project>();
            List<Entities.Models.Files.Solution.Global> globals = new List<Entities.Models.Files.Solution.Global>();
                        
            // Loop Variables
            bool isHeaderBuilt = false;
            bool isProjctListBuilt = false;
            bool isProjctSectionBuilt = false;
            int counter = 0;
            string line = "";
            // Read the file and display it line by line.  
            using (System.IO.StreamReader file = new System.IO.StreamReader(source))
            {
                while (!isHeaderBuilt && (line = file.ReadLine()) != null)
                {
                    //System.Console.WriteLine(line);
                    // Reading line to identy header
                    if (!line.Contains("(") && !isHeaderBuilt)
                    {
                        header.headerData.Add(line.Trim());
                    }
                    else
                    {
                        isHeaderBuilt = true;
                    }
                    counter++;
                }                                
                do
                {
                    if (!isProjctListBuilt && !line.ToUpper().Trim().Equals("GLOBAL"))
                    {
                        if (!line.ToUpper().Trim().Equals("ENDPROJECT"))
                        {
                            // Subsection of Projects - examples include added files to project
                            if (line.ToUpper().Trim().Split('(')[0].Equals("PROJECTSECTION"))
                            {
                                project[project.Count - 1].projectSection = new List<Entities.Models.Files.Solution.ProjectSection>();

                                do
                                {
                                    // Checking for ENDOFPROJECT section delimiter
                                    if (line.ToUpper().Trim().Equals("ENDPROJECTSECTION"))
                                    {
                                        isProjctSectionBuilt = true;
                                        project[project.Count - 1].projectSection.Add(new Entities.Models.Files.Solution.ProjectSection
                                        {
                                            isEndOfProjectSection = true,                                            
                                            filename = Regex.Replace(line, @"\t|\n|\r", "")
                                        });

                                    }
                                    else
                                    {
                                        isProjctSectionBuilt = false;
                                        var projectSelectionSplit = line.Split('=');
                                        project[project.Count - 1].projectSection.Add(new Entities.Models.Files.Solution.ProjectSection
                                        {
                                            filename = projectSelectionSplit[0].Trim() ?? "",
                                            equalsFilename = projectSelectionSplit[1].Trim() ?? ""
                                        });
                                        Console.WriteLine(line);
                                    }
                                }
                                while (!isProjctSectionBuilt && (line = file.ReadLine()) != null);

                            }
                            else if (line.ToUpper().Trim().Split('(')[0].Equals("PROJECT"))
                            {
                                /* Spliting Example
                                 * Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "00-Entities", "00-Entities", "{B58898D1-8447-4694-BFA7-F621AAD2B9BD}"
                                 * To
                                 * Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}")
                                 * "00-Entities", "00-Entities", "{B58898D1-8447-4694-BFA7-F621AAD2B9BD}"
                                 * 
                                 * DOES NOT CAPTURE CLOSING DELIMETER
                                 */
                                var splitProjectByEqualSign = line.Split('=');
                                var splitPostEqualsByComma = splitProjectByEqualSign[1].Replace("\"", string.Empty).Split(',');
                                if (splitPostEqualsByComma.Length == 3)
                                {
                                    project.Add(new Entities.Models.Files.Solution.Project
                                    {
                                        assignedGuid = splitProjectByEqualSign[0],
                                        projectName = splitPostEqualsByComma[0].Trim(),
                                        projectLocation = splitPostEqualsByComma[1].Trim(),
                                        projectGuid = splitPostEqualsByComma[2].Trim(),
                                        projectData = line,
                                        doesFolderExist = (File.Exists($"{workingDirectory}\\{splitPostEqualsByComma[1].Trim()}")) ? true : false
                                    });
                                }
                                Console.WriteLine(line);
                            }
                        }
                        else
                        {
                            // CAPTURING CLOSING DELIMETER
                            project[project.Count - 1].projectDataEnd = Regex.Replace(line, @"\t|\n|\r", "");
                        }
                    }
                    else
                    {
                        isProjctListBuilt = true;
                        globals.Add(new Entities.Models.Files.Solution.Global { globalSection = line });
                    }
                }
                while ((line = file.ReadLine()) != null);
            }
            Entities.Models.Files.Solution.Solution data = new Entities.Models.Files.Solution.Solution();
            // NOT THE WAY TO DO THE HEADER --> THIS IS AN EXAMPLE OF HOW NOT TO IMPLEMENT OBJECT LISTS
            data.header = new Entities.Models.Files.Solution.Header();
            data.header.headerData = new List<string>();
            data.header.headerData.AddRange(header.headerData);
            // THIS IS CORRECT
            data.project = project;
            data.global = globals;
            
            return (data);
        }
    }
}