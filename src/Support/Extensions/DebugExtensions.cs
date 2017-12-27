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
    public static partial class Extensions
    {
        [Conditional("DEBUG")]
        public static void DebugThis(this string str, [CallerMemberName] string callername = "", [CallerFilePath] string filename = "", [CallerLineNumber] int linenumber = 0)
        {
            if (!string.IsNullOrEmpty(callername))
                Debug.WriteLine($"{callername} in {filename}:{Environment.NewLine}{str} line {linenumber}");
            else
                Debug.WriteLine(str);
        }

        [Conditional("DEBUG")]
        public static void DebugThis(this Exception ex, [CallerMemberName] string callername = "", [CallerFilePath] string filename = "", [CallerLineNumber] int linenumber = 0)
        {
            DebugThis(ex.Message, callername, filename, linenumber);
        }
    }

#if PORTABLE
    }

#endif
}