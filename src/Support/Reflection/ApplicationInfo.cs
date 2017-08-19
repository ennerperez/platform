﻿using System;
using System.Linq;
using System.Reflection;

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
                public static Version Version
                {
                    get
                    {
#if !PORTABLE
                    return Assembly.GetCallingAssembly().GetName().Version;
#elif !PROFILE_78
                        return new AssemblyName(Assembly.GetCallingAssembly().FullName).Version;
#else
                        return new AssemblyName(typeof(ApplicationInfo).GetTypeInfo().Assembly.FullName).Version;
#endif
                    }
                }

                public static string Title
                {
                    get
                    {
#if !PROFILE_78
                        object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
#else
                        var attributes = typeof(ApplicationInfo).GetTypeInfo().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute)).ToArray();
#endif
                        if (attributes.Length > 0)
                        {
                            AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                            if (titleAttribute.Title.Length > 0) return titleAttribute.Title;
                        }
#if !PORTABLE
                    return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
#elif !PROFILE_78
                        return new AssemblyName(Assembly.GetCallingAssembly().FullName).Name;
#else
                        return new AssemblyName(typeof(ApplicationInfo).GetTypeInfo().Assembly.FullName).Name;
#endif
                    }
                }

                public static string ProductName
                {
                    get
                    {
#if !PROFILE_78
                        object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
#else
                        var attributes = typeof(ApplicationInfo).GetTypeInfo().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute)).ToArray();
#endif
                        return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
                    }
                }

                public static string Description
                {
                    get
                    {
#if !PROFILE_78
                        object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
#else
                        var attributes = typeof(ApplicationInfo).GetTypeInfo().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute)).ToArray();
#endif
                        return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
                    }
                }

                public static string CopyrightHolder
                {
                    get
                    {
#if !PROFILE_78
                        object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
#else
                        var attributes = typeof(ApplicationInfo).GetTypeInfo().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute)).ToArray();
#endif
                        return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
                    }
                }

                public static string CompanyName
                {
                    get
                    {
#if !PROFILE_78
                        object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
#else
                        var attributes = typeof(ApplicationInfo).GetTypeInfo().Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute)).ToArray();
#endif
                        return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
                    }
                }
            }
        }

#if PORTABLE
    }

#endif
}