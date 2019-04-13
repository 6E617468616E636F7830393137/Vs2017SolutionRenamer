using System;
using System.IO;
using System.Xml;

namespace VsSolutionRenamer.Business.ProjectUpdater.XmlParser.Transactions
{
    /// <summary>
    /// Takes Project object and reads and updates content
    /// </summary>
    internal class Request
    {
        internal Entities.Models.Files.Projects.Projects ExecuteReadNodes(Entities.Models.Files.Projects.Projects data)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(data.fileName);

            XmlNodeList elemList = xmlDoc.GetElementsByTagName("RootNamespace");
            if (elemList != null)
            {
                data.rootNamespace = elemList[0].InnerText;
            }
            elemList = xmlDoc.GetElementsByTagName("AssemblyName");
            if (elemList != null)
            {
                data.assemblyName = elemList[0].InnerText;
            }            
            return data;
        }
        internal Entities.Models.Files.Projects.Projects ExecuteUpdateNodes(Entities.Models.Files.Projects.Projects data, string originalNamespace, string updatedNamespaceAssemblyName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(data.fileName);
            //rename directory 
            var fileInfo = new FileInfo(data.fileName);
            int place = fileInfo.DirectoryName.LastIndexOf(originalNamespace);
            var newFoldername = fileInfo.DirectoryName.Remove(place, originalNamespace.Length).Insert(place, updatedNamespaceAssemblyName);//fileInfo.DirectoryName.Replace(originalNamespace, updatedNamespaceAssemblyName);
            var newFilename = fileInfo.Name.Replace(originalNamespace, updatedNamespaceAssemblyName);
            XmlNodeList elemList = xmlDoc.GetElementsByTagName("RootNamespace"); //xmlDoc.SelectSingleNode("Project/PropertyGroup/RootNamespace");
            if (elemList != null)
            {
                elemList[0].InnerText = elemList[0].InnerText.Replace(originalNamespace, updatedNamespaceAssemblyName);
            }
            elemList = xmlDoc.GetElementsByTagName("AssemblyName"); //xmlDoc.SelectSingleNode("Project/PropertyGroup/AssemblyName");
            if (elemList != null)
            {
                elemList[0].InnerText = elemList[0].InnerText.Replace(originalNamespace, updatedNamespaceAssemblyName);
            }
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("HintPath");
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].InnerXml.Contains(originalNamespace))
                {
                    nodes[i].InnerXml = nodes[i].InnerXml.Replace(originalNamespace, updatedNamespaceAssemblyName);
                }
            }
            nodes = xmlDoc.GetElementsByTagName("ProjectReference");
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Attributes["Include"].Value = nodes[i].Attributes["Include"].Value.Replace(originalNamespace, updatedNamespaceAssemblyName);
                XmlNodeList childNodes = nodes[i].ChildNodes;
                for (int h = 0; h < childNodes.Count; h++)
                {
                    if (childNodes[h].Name.Equals("Name"))
                    {
                        childNodes[h].InnerXml = childNodes[h].InnerXml.Replace(originalNamespace, updatedNamespaceAssemblyName);
                    }
                }            
            }
            nodes = xmlDoc.GetElementsByTagName("None");
            for (int i = 0; i < nodes.Count; i++)
            {
                
                nodes[i].Attributes["Include"].Value = nodes[i].Attributes["Include"].Value.Replace(originalNamespace, updatedNamespaceAssemblyName);
                XmlNodeList childNodes = nodes[i].ChildNodes;
                for (int h = 0; h < childNodes.Count; h++)
                {
                    if (childNodes[h].Name.Equals("Name"))
                    {
                        childNodes[h].InnerXml = childNodes[h].InnerXml.Replace(originalNamespace, updatedNamespaceAssemblyName);
                    }
                }
                
            }
            elemList = xmlDoc.GetElementsByTagName("DocumentationFile");
            if (elemList.Count > 0)
            {
                elemList[0].InnerText = elemList[0].InnerText.Replace(originalNamespace, updatedNamespaceAssemblyName);
            }
            nodes = xmlDoc.GetElementsByTagName("IISUrl");
            for (int i = 0; i < nodes.Count; i++)
            {
                Random generator = new Random();                
                nodes[i].InnerXml = $"http://localhost:{generator.Next(10000, 60000)}";
            }
            xmlDoc.Save(data.fileName);
            if (File.Exists($"{data.fileName}.user"))
            {
                if (File.Exists($"{fileInfo.Directory}\\{newFilename}.user"))
                {
                    File.Delete($"{fileInfo.Directory}\\{newFilename}.user");
                }
                File.Move($"{data.fileName}.user", $"{fileInfo.Directory}\\{newFilename}.user");
            }
            if (File.Exists($"{fileInfo.Directory}\\{newFilename}"))
            {
                File.Delete($"{fileInfo.Directory}\\{newFilename}");
            }
            //File.Move(data.fileName, $"{fileInfo.Directory}\\{newFilename}");
            //Directory.Move(fileInfo.DirectoryName, newFoldername);
            FileCopyLibrary.Bll.FileCopyLibrary.IFileManager fileManager = new FileCopyLibrary.Bll.FileCopyLibrary.FileManager();
            fileManager.Copy(data.fileName, $"{fileInfo.Directory}\\{newFilename}");
            File.Delete(data.fileName);
            fileManager.MoveFolder(fileInfo.DirectoryName, newFoldername);

            return data;
        }
    }
}
