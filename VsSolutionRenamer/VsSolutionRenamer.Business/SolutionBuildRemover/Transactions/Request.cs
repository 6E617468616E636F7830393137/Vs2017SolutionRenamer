using System;
using System.IO;
using Log = Log4net.Helper.Logging.Logger;

namespace VsSolutionRenamer.Business.SolutionBuildRemover.Transactions
{
    /// <summary>
    /// Removes files and folders in within a given directory and the source folder
    /// </summary>
    public class Request
    {
        // Bin and Obj file remover
        internal bool Execute(string source)
        {
            string[] files = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);
            string[] folders = Directory.GetDirectories(source, "*.*", SearchOption.AllDirectories); 
            bool fail = false;
            foreach (string file in files)
            {
                try
                {
                    // Check if File exists
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleteing file {file} : : : {ex}");
                    Log.Error($"Error deleteing file {file} : : : {ex}");
                    fail = true;
                }
            }
            for (int i = folders.Length-1; i >-1; i--)
            {
                try
                {
                    // Check if folder exists
                    if (Directory.Exists(folders[i]))
                    {
                        Directory.Delete(folders[i]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleteing folder {folders[i]} : : : {ex}");
                    Log.Error($"Error deleteing folder {folders[i]} : : : {ex}");
                    fail = true;
                }
            }
            try
            {
                // Check if source folder exists
                if (Directory.Exists(source))
                {
                    Directory.Delete(source);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleteing folder {source} : : : {ex}");
                Log.Error($"Error deleteing folder {source} : : : {ex}");
                fail = true;
            }
            return fail;
        }
    }
}
