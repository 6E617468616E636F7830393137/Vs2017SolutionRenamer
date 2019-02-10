using System;
using System.Collections;
using System.IO;
using Log = Log4net.Helper.Logging.Logger;

namespace VsSolutionRenamer.Business.ClassUpdater.Transactions
{
    internal class Request
    {
        internal bool ExecuteUpdater(ArrayList filenames, string originalNamespace, string updatedNameSpace)
        {
            foreach (string filename in filenames)
            {
                // Readfiles, Locate Namespace(s), and Output files to file
                var filestream = new System.IO.FileStream(filename,
                                              System.IO.FileMode.Open,
                                              System.IO.FileAccess.Read,
                                              System.IO.FileShare.ReadWrite);
                var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
                var tempFilename = $"{filename}.{Guid.NewGuid().ToString().Substring(0,6)}.temp";
                StreamWriter outputStream = new StreamWriter(tempFilename);
                string lineOfText;
                while ((lineOfText = file.ReadLine()) != null)
                {
                    if (lineOfText.Contains("namespace"))
                    {
                        lineOfText = lineOfText.Replace($"{originalNamespace}", $"{updatedNameSpace}");
                    }
                    // Namespaces contained within class
                    else if (lineOfText.Contains($"{originalNamespace}."))
                    {
                        lineOfText = lineOfText.Replace($"{originalNamespace}.", $"{updatedNameSpace}.");
                    }
                    //Do something with the lineOfText
                    outputStream.WriteLine(lineOfText); 
                }
                file.Close();
                outputStream.Close();
                try
                {
                    // Check if File exists
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                    // Move Temp file to filename
                    File.Move(tempFilename, filename);
                    Log.Info($": : : : : SUCCESS RENAMING CS FILE - {tempFilename} to {filename}");
                }
                catch (Exception ex)
                {
                    Log.Error($": : : : : ERROR RENAMING CS FILE - {tempFilename} to {filename} - {ex.ToString()}");
                }
            }
            return true;
        }
    }
}
