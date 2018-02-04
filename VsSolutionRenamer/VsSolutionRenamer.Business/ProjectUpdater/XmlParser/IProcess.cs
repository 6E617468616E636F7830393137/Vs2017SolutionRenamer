using System.Collections.Generic;
using System.IO;


namespace VsSolutionRenamer.Business.ProjectUpdater.XmlParser
{
    public interface IProcess
    {
        Entities.Models.Files.Projects.Projects Execute(Entities.Models.Files.Projects.Projects data);
        Entities.Models.Files.Projects.Projects Execute(Entities.Models.Files.Projects.Projects data, string originalNamespace, string updatedNamespaceAssemblyName);
    }
}
