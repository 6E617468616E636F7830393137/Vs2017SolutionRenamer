using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsSolutionRenamer.Entities.Models.Files.Projects
{
    public class ProjectSection
    {
        public string filename { get; set; }
        public string filenameEquals { get; set; }
        public string currentFolder { get; set; }
        public string updatedFolder { get; set; }
    }
}
