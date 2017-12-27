using Platform.Support.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace Platform.Support.Net.Download
{
    public class EngineException : LocalizedException
    {
        public EngineException()
        {
        }

        public EngineException(ResourceManager resourceManager, string resourceId, params object[] args) : base(resourceManager, resourceId, args)
        {
        }

        public EngineException(string message) : base(message)
        {
        }

        public EngineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public bool ShouldLog { get; set; }
    }
}