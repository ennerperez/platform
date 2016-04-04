using Platform.Model.SPS.Attributes;
using Platform.Support.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model.SPS
{
    /// <summary>
    /// This class is used to make possible to create a plugin that implements
    /// IPlugIn interface. A plugin must derive this class to be used by
    /// main application.
    /// </summary>
    /// <typeparam name="TApp">Type of main application interface</typeparam>
    public abstract class PlugIn<TApp> : IPlugIn
    {
        /// <summary>
        /// Gets a reference to main application.
        /// </summary>
        public IPlugInApplication<TApp> Application { get; internal set; }

        /// <summary>
        /// Gets the name of this plugin.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PlugIn()
        {
            Application = new PlugInApplication<TApp>();

            //Get Name from PlugIn attribute.
            var thisPlugInType = GetType();
            var plugInAttribute = Helpers.GetAttribute<PlugInAttribute>(thisPlugInType);
            Name = plugInAttribute == null ? thisPlugInType.Name : plugInAttribute.Name;
        }
    }

}
