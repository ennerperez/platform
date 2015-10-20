﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model.SPS.Interfaces
{
    /// <summary>
    /// All plugin-based applications implements this interface.
    /// Applications does not directly implement this interface, but they implement by inheriting PlugInBasedApplication class.
    /// </summary>
    public interface IPlugInBasedApplication : IPluggable
    {
        /// <summary>
        /// Gets the name of this Application.
        /// </summary>
        string Name { get; }
    }
}