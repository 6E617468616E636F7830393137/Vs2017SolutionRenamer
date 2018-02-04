using System.Collections.Generic;

namespace VsSolutionRenamer.Entities.Models.Files.Solution
{
    public class Solution
    {
        public string folderLocation { get; set; }
        public string updatedFolderLocation { get; set; }
        public string solutionName { get; set; }
        public string updatedSolutionName { get; set; }
        public Header header { get; set; }
        public List<Project> project { get; set; }
        public List<Global> global { get; set; }
    }
}
