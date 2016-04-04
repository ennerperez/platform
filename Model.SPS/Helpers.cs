﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Platform.Model.SPS
{
    public static class Helpers
    {
        /// <summary>
        /// Gets an attribute from a MemberInfo object.
        /// </summary>
        /// <typeparam name="T">Type of searched Attribute</typeparam>
        /// <param name="memberInfo">MemberInfo object</param>
        /// <returns>Specified Attribute, if found, else null</returns>
        public static T GetAttribute<T>(MemberInfo memberInfo) where T : class
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(T), true);
            if (attributes.Length <= 0)
            {
                return null;
            }

            return attributes[0] as T;
        }

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
                return (new FileInfo(Assembly.GetEntryAssembly().Location)).Directory.FullName;
            }
            catch (Exception)
            {
                return Directory.GetCurrentDirectory();
            }
        }
    }

}
