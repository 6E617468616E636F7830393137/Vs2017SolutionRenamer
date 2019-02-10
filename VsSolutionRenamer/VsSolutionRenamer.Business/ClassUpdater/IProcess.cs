using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace VsSolutionRenamer.Business.ClassUpdater
{ 
    public interface IProcess
    {
        bool ExecuteClassUpdater(ArrayList filenames, string originalNamespace, string updatedNameSpace);
    }
}
