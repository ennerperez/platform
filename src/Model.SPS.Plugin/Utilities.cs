using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace SPS
    {
        public static partial class Utilities
        {
#if !PORTABLE

            /// <summary>
            /// Searches a directory and all subdirectories and returns a list of assembly files.
            /// </summary>
            /// <param name="plugInFolder">Directory to search assemblies</param>
            /// <returns>List of found assemblies</returns>
            public static List<string> FindAssemblyFiles(string plugInFolder)
            {
                var assemblyFilePaths = new List<string>();
                assemblyFilePaths.AddRange(Directory.GetFiles(plugInFolder, "*.exe", SearchOption.AllDirectories));
                assemblyFilePaths.AddRange(Directory.GetFiles(plugInFolder, "*.dll", SearchOption.AllDirectories));
                return assemblyFilePaths;
            }

            /// <summary>
            /// Gets the current directory of executing assembly.
            /// </summary>
            /// <returns>Directory path</returns>
            public static string GetCurrentDirectory()
            {
                try
                {
                    return (new FileInfo(Assembly.GetExecutingAssembly().Location)).Directory.FullName;
                }
                catch (Exception)
                {
                    return Directory.GetCurrentDirectory();
                }
            }

#endif
        }
    }

#if PORTABLE
    }

#endif
}