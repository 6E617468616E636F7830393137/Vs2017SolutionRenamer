using System.Collections.Generic;
using System.IO;

namespace VsSolutionRenamer.Business.SolutionParser
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
        public Entities.Models.Files.Solution.Solution Execute(string source)
        {
            return _process.Execute(source);
        }
    }
}
