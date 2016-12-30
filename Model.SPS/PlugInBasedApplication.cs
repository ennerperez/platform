﻿using Platform.Model.SPS.Attributes;
using Platform.Support.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Platform.Model.SPS
{
    /// <summary>
    /// This class must be inherited by applications that uses plugins.
    /// </summary>
    /// <typeparam name="TPlugIn">Type of PlugIn interface that is used by application</typeparam>
    public class PlugInBasedApplication<TPlugIn> : IPlugInBasedApplication
    {
        /// <summary>
        /// Gets the name of this Application.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// All plugins that are loaded in this application.
        /// </summary>
        public List<IApplicationPlugIn<TPlugIn>> PlugIns { get; private set; }

        /// <summary>
        /// Gets/Sets the plugin folder for this application.
        /// Default: Root directory of application.
        /// </summary>
        public string PlugInFolder { get; set; }

        /// <summary>
        /// A boolean value indicates whether plugins are loaded or not.
        /// </summary>
        public bool PlugInsLoaded { get; private set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public PlugInBasedApplication()
        {
            Initialize();
            PlugInFolder = Helper.GetCurrentDirectory();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="plugInFolder">plugin folder for this application</param>
        public PlugInBasedApplication(string plugInFolder)
        {
            Initialize();
            PlugInFolder = plugInFolder;
        }

        /// <summary>
        /// Loads all PlugIns in PlugInFolder directory.
        /// </summary>
        public void LoadPlugIns()
        {
            if (string.IsNullOrEmpty(PlugInFolder) || !Directory.Exists(PlugInFolder))
            {
                throw new ApplicationException("PlugInFoler must be a valid folder path");
            }

            var assemblyFiles = Helper.FindAssemblyFiles(PlugInFolder);
            var plugInType = typeof(TPlugIn);
            foreach (var assemblyFile in assemblyFiles)
            {
                var allTypes = Assembly.LoadFrom(assemblyFile).GetTypes();
                foreach (var type in allTypes)
                {
                    if (plugInType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                    {
                        PlugIns.Add(new ApplicationPlugIn<TPlugIn>(this, type));
                    }
                }
            }

            PlugInsLoaded = true;
        }

        /// <summary>
        /// Initializes this class.
        /// </summary>
        private void Initialize()
        {
            var plugInApplicationAttribute = Helper.GetAttribute<PlugInApplicationAttribute>(GetType());
            Name = plugInApplicationAttribute == null ? GetType().Name : plugInApplicationAttribute.Name;
            PlugIns = new List<IApplicationPlugIn<TPlugIn>>();
        }
    }

}
