using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoMetrics
{
    internal class Program
    {
        /// <summary>
        /// if true scan the .git folder:
        /// </summary>
        static bool git = false;

        /// <summary>
        /// if true, scan the .VS folder
        /// </summary>
        static bool vs = false;

        static Dictionary<string, long> sizesByExtension;

        private static void ProcessFolder(string folder)
        {
            foreach (var entry in Directory.GetFiles(folder))
            {
                var extension = Path.GetExtension(entry);
                var length = new FileInfo(entry).Length;
                if (sizesByExtension.ContainsKey(extension))
                {
                    sizesByExtension[extension] += length;
                }
                else
                {
                    sizesByExtension[extension] = length;
                }
            }
            foreach (var dir in Directory.GetDirectories(folder))
            {
                if (dir.EndsWith(".git") && !git)
                {
                    continue;
                }
                if (dir.EndsWith(".vs") && !vs)
                {
                    continue;
                }
                ProcessFolder(dir);
            }
        }
        private static void Main(string[] args)
        {
            try
            {
                if (!args.Any())
                {
                    throw new Exception("You must supply at least one argument.");
                }

                foreach (var arg in args)
                {
                    if (arg[0] == '-')
                    {
                        if (arg.Length == 1)
                        {
                            throw new Exception("minus on its own is not permitted as an option.");
                        }
                        if (arg[1] == 'g')
                        {
                            git = true;
                        }
                        else if (arg[1] == 'v')
                        {
                            vs = true;
                        }
                    }
                    else
                    {
                        sizesByExtension = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
                        ProcessFolder(arg);
                        foreach (var entry in sizesByExtension)
                        {
                            var extension = string.IsNullOrEmpty(entry.Key) ? "[No extension]" : entry.Key;
                            Console.WriteLine($"{extension} {entry.Value}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                var progname = Path.GetFileNameWithoutExtension(codeBase);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }
    }
}
