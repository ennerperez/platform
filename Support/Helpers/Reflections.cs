﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
    public static partial class Helpers
    {

#if (!PORTABLE)
        internal static System.Reflection.Assembly m_Assembly = System.Reflection.Assembly.GetEntryAssembly();
#else
        internal static System.Reflection.Assembly m_Assembly = System.Reflection.Assembly.GetCallingAssembly();
#endif

        public static T GetAttribute<T>(System.Reflection.Assembly assembly = null) where T : System.Attribute
        {
            return (T)GetAttribute(typeof(T), assembly);
        }
        public static T[] GetAttributes<T>(System.Reflection.Assembly assembly = null) where T : System.Attribute
        {
            return (T[])GetAttributes(typeof(T), assembly);
        }

        public static object GetAttribute(Type AttributeType, System.Reflection.Assembly assembly = null)
        {
            return GetAttributes(AttributeType, assembly)[0];
        }
        public static object[] GetAttributes(Type AttributeType, System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }

            object[] customAttributes = assembly.GetCustomAttributes(AttributeType, true);
            if (customAttributes.Length == 0)
            {
                return null;
            }
            return customAttributes;
        }

        public static String Title(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyTitleAttribute>(assembly).Title;
        }
        public static String Description(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyDescriptionAttribute>(assembly).Description;
        }
        public static String Company(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyCompanyAttribute>(assembly).Company;
        }
        public static String Product(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyProductAttribute>(assembly).Product;
        }
        public static String Copyright(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyCopyrightAttribute>(assembly).Copyright;
        }
        public static String Trademark(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyTrademarkAttribute>(assembly).Trademark;
        }
        public static Version Version(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return new Version(GetAttribute<System.Reflection.AssemblyVersionAttribute>(assembly).Version);
        }
        public static Version FileVersion(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return new Version(GetAttribute<System.Reflection.AssemblyFileVersionAttribute>(assembly).Version);
        }

#if (!PORTABLE)
        public static Guid GUID(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return new Guid(GetAttribute<System.Runtime.InteropServices.GuidAttribute>(assembly).Value);
        }

        public static String DirectoryPath(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return new System.IO.FileInfo(assembly.Location).Directory.FullName;
        }

        public static String ExecutablePath(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return assembly.Location;
        }


#endif

    }
}