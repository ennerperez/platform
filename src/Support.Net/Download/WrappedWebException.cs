using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Platform.Support.Net.Download
{
    public class WrappedWebException : WebException
    {
        public WrappedWebException(int errorCode, string message) : base(message)
        {
            base.HResult = errorCode;
        }
    }
}