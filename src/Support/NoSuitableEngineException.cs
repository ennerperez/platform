using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    public class NoSuitableEngineException : InvalidOperationException
    {
        public NoSuitableEngineException(string message) : base(message)
        {
        }
    }

#if PORTABLE
    }
#endif
}