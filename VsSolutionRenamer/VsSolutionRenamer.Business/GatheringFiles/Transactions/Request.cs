using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VsSolutionRenamer.Business.GatheringFiles.Transactions
{
    internal class Request
    {
        internal List<FileInfo> Execute(string source)
        {            
            List<string> getFiles = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories).ToList();
            List<FileInfo> gotFiles = new List<FileInfo>();                        

            foreach (string filename in getFiles)
            {
                if (filename.ToUpper().EndsWith("SLN") /*|| filename.ToUpper().EndsWith("CSPROJ")*/)
                {
                    gotFiles.Add(new FileInfo(filename));
                }
            }

            return gotFiles;
        }
    }
}
