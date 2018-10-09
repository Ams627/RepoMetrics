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
                        else if (arg[1] == '')
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
