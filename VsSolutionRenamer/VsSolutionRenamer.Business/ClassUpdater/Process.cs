using System.Collections.Generic;
using System.IO;

namespace VsSolutionRenamer.Business.ClassUpdater
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
        public bool ExecuteClassUpdater(string[] filenames, string originalNamespace, string updatedNameSpace)
        {
            return _process.ExecuteClassUpdater(filenames, originalNamespace, updatedNameSpace);
        }
    }
}
