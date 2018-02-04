using System.Collections.Generic;
using System.IO;

namespace VsSolutionRenamer.Business.GatheringFiles
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
        public List<FileInfo> Execute(string source)
        {
            return _process.Execute(source);
        }
    }
}
