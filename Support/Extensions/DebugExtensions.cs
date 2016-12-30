using System;
using System.Diagnostics;

namespace Platform.Support
{

#if PORTABLE
    namespace Core
    {
#endif

        /// <summary>
        /// Class DebugExtensions.
        /// </summary>
        public static class DebugExtensions
        {

            [Conditional("DEBUG")]
#if NETFX_45 && !PORTABLE
        public static void DebugThis(this string str, [CallerMemberName] string callername = "", [CallerFilePath] string filename = "")
#else
            public static void DebugThis(this string str, string callername = "", string filename = "")
#endif
            {
                if (!string.IsNullOrEmpty(callername))
                    Debug.WriteLine("{1} in {3}:{0}{2}", Environment.NewLine, callername, str, filename);
                else
                    Debug.WriteLine(str);
            }

            [Conditional("DEBUG")]
#if NETFX_45 && !PORTABLE
        public static void DebugThis(this Exception ex, [CallerMemberName] string callername = "", [CallerFilePath] string filename = "")
#else
            public static void DebugThis(this Exception ex, string callername = "", string filename = "")
#endif
            {
                DebugThis(ex.Message, callername, filename);
            }


        }

#if PORTABLE
    }
#endif

}
