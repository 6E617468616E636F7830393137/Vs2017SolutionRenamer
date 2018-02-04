using System.Collections.Generic;
using System.IO;


namespace VsSolutionRenamer.Business.GatheringFiles
{
    public interface IProcess
    {
        List<FileInfo> Execute(string source);
    }
}
