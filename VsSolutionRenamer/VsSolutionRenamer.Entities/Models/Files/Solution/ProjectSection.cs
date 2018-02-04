namespace VsSolutionRenamer.Entities.Models.Files.Solution
{
    // Used for projects that have imported files into project, ie README.TXT or CERTIFICATES
    public class ProjectSection
    {        
        public string filename { get; set; }        
        public string equalsFilename { get; set; }
        public bool isEndOfProjectSection { get; set; }
    }
}
