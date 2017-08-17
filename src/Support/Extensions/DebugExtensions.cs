using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

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
#if !PORTABLE
        public static void DebugThis(this string str, [CallerMemberName] string callername = "", [CallerFilePath] string filename = "", [CallerLineNumber] int linenumber = 0)
#else
        public static void DebugThis(this string str, string callername = "", string filename = "", int linenumber = 0)
#endif
        {
            if (!string.IsNullOrEmpty(callername))
                    Debug.WriteLine($"{callername} in {filename}:{Environment.NewLine}{str} line {linenumber}");
                else
                    Debug.WriteLine(str);
            }

            [Conditional("DEBUG")]
#if !PORTABLE
        public static void DebugThis(this Exception ex, [CallerMemberName] string callername = "", [CallerFilePath] string filename = "", [CallerLineNumber] int linenumber = 0)
#else
        public static void DebugThis(this Exception ex, string callername = "", string filename = "", int linenumber = 0)
#endif
        {
            DebugThis(ex.Message, callername, filename, linenumber);
            }
        }

#if PORTABLE
    }

#endif
}