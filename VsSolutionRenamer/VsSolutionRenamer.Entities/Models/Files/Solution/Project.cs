using System.Collections.Generic;

namespace VsSolutionRenamer.Entities.Models.Files.Solution
{
    public class Project
    {
        // Assigned Project Id
        public string assignedGuid { get; set; }
        // Project Id
        public string projectGuid { get; set; }
        // Virtual Folder
        public string projectName { get; set; }
        // Updated Virtual Folder
        public string updatedProjectName { get; set; }
        // Project Folder
        public string projectLocation { get; set; }
        // Updated Project Folder
        public string updatedProjectLocation { get; set; }
        // Boolean for identifying file versus virtual folder
        public bool doesFolderExist { get; set; }
        // Original Project configuration line
        public string projectData { get; set; }
        // Updated Project configuration line
        public string updatedProjectData { get; set; }
        // End tag for Project Entry
        public string projectDataEnd { get; set; }
        // Section for imported files for project
        public List<ProjectSection> projectSection { get; set; }
    }
}
