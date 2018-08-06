using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace Reflection
        {
            public static class ApplicationInfo
            {
                public static Assembly Assembly
                {
                    get
                    {
#if !PORTABLE
                    return Assembly.GetCallingAssembly();
#elif !PROFILE_78
                        return System.Reflection.Assembly.GetCallingAssembly();
#else
                        return typeof(ApplicationInfo).GetTypeInfo().Assembly;
#endif
                    }
                }

                public static Version Version => Assembly.GetCustomAttribute<AssemblyVersionAttribute>().Version.As<Version>();
                public static string Title => Assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
                public static string Product => Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                public static string Description => Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;
                public static string Copyright => Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
                public static string Company => Assembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company;

#if !PORTABLE
            public static Guid Guid => Assembly.GetCustomAttribute<GuidAttribute>().Value.As<Guid>();

            public static Dictionary<string, string> GetCommandLine()
            {
                var commandArgs = new Dictionary<string, string>();

                var assembly = string.Format(@"""{0}"" ", Assembly.Location);
                var collection = System.Environment.CommandLine.Replace(assembly, "").Split(' ').Select(a => a.ToLower()).ToList();

                if (collection.Any())
                    foreach (var item in collection.Where(m => m.StartsWith("/") || m.StartsWith("-")))
                        if (collection.Count - 1 > collection.IndexOf(item))
                            commandArgs.Add(item.ToLower().Substring(0), collection[collection.IndexOf(item) + 1].Replace(@"""", @""));
                        else
                            commandArgs.Add(item.ToLower().Substring(0), null);

                return commandArgs;
            }

#endif
            }
        }

#if PORTABLE
    }

#endif
}