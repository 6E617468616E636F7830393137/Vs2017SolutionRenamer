using System.Collections.Generic;
using System.IO;


namespace VsSolutionRenamer.Business.SolutionParser
{
    public interface IProcess
    {
        Entities.Models.Files.Solution.Solution Execute(string source);
    }
}
