using System.IO;

#if PORTABLE

using Helpers = Platform.Support.Core.IO.StreamHelper;

#else

using Helpers = Platform.Support.IO.StreamHelper;

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