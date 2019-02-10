using System;
using System.IO;
using Log = Log4net.Helper.Logging.Logger;

namespace VsSolutionRenamer.Business.SolutionBuildRemover.Transactions
{
    public class Request
    {
        // Bin and Obj file remover
        internal bool Execute(string source)
        {
            string[] files = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);
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
            return fail;
        }
    }
}
