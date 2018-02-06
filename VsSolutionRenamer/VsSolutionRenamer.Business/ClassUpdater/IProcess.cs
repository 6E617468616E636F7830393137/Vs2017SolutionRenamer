using System.Collections.Generic;
using System.IO;


namespace VsSolutionRenamer.Business.ClassUpdater
{ 
    public interface IProcess
    {
        bool ExecuteClassUpdater(string [] filenames, string originalNamespace, string updatedNameSpace);
    }
}
