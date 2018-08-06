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

    namespace Net
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

#if PORTABLE
    }
#endif
}