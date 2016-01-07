using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
    public static partial class Helpers
    {

        public static string ISO8601(DateTime date)
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", date);
        }

    }
}
