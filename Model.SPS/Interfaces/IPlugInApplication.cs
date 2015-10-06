using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model.SPS.Interfaces
{
    /// <summary>
    /// Represents a plugin-based application from perspective of plugins.
    /// </summary>
    /// <typeparam name="TApp">Type of application interface</typeparam>
    public interface IPlugInApplication<out TApp>
    {
        /// <summary>
        /// Proxy object to use application by plugin over application interface.
        /// </summary>
        TApp ApplicationProxy { get; }

        /// <summary>
        /// Name of the application.
        /// </summary>
        string Name { get; }
    }
}
