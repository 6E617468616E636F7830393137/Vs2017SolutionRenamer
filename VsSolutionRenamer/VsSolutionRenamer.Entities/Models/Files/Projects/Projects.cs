using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsSolutionRenamer.Entities.Models.Files.Projects
{
    public class Projects
    {
        public string fileName { get; set; }
        public string updateName { get; set; }
        public string rootNamespace { get; set; }
        public string assemblyName { get; set; }
        public string newRootNamespace { get; set; }
        public string newAssemblyName { get; set; }

    }
}
