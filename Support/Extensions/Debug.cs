using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Platform.Support
{
    /// <summary>
    /// Class DebugExtensions.
    /// </summary>
    public static class DebugExtensions
    {

        [Conditional("DEBUG")]
        public static void DebugThis(this string str, [CallerMemberName] string callername = "", [CallerFilePath] string filename = "")
        {
            if (!string.IsNullOrEmpty(callername))
                Debug.WriteLine("{1} in {3}:{0}{2}", Environment.NewLine, callername, str, filename);
            else
                Debug.WriteLine(str);
        }

        [Conditional("DEBUG")]
        public static void DebugThis(this Exception ex, [CallerMemberName] string callername = "", [CallerFilePath] string filename = "")
        {
            DebugThis(ex.Message, callername, filename);
        }


    }
}
