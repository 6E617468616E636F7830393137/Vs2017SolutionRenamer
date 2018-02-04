using System.Collections.Generic;
using System.IO;

namespace VsSolutionRenamer.Business.ProjectUpdater.XmlParser
{
    public class Process
    {
        // Declaration
        private readonly IProcess _process;
        // Constructor
        public Process(IProcess concreteImplementation)
        {
            _process = concreteImplementation;
        }
        // Implementation
        public Entities.Models.Files.Projects.Projects Execute(Entities.Models.Files.Projects.Projects data)
        {
            return _process.Execute(data);
        }
        public Entities.Models.Files.Projects.Projects Execute(Entities.Models.Files.Projects.Projects data, string originalNamespace, string updatedNamespaceAssemblyName)
        {
            return _process.Execute(data, originalNamespace, updatedNamespaceAssemblyName);
        }
    }
}
