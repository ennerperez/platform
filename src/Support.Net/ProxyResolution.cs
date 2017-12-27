using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.Net
{
    internal enum ProxyResolution
    {
        Default,
        DefaultCredentialsOrNoAutoProxy,
        NetworkCredentials,
        DirectAccess,
        Error
    }
}