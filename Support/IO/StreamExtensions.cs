﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if PORTABLE
using Helpers = Platform.Support.Core.IO.StreamHelpers;
#else
using Helpers = Platform.Support.IO.StreamHelpers;
#endif

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif
        namespace IO
        {
            public static class StreamExtensions
            {

                public static void CopyTo(this Stream from, ref Stream destination)
                {
                    Helpers.CopyStream(from, destination);
                }

            }
        }
#if PORTABLE
    }
#endif
}
