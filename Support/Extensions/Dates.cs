using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
    public static partial class Extensions
    {

        public static string ISO8601(this System.DateTime date)
        {
            return Helpers.ISO8601(date);
        }

        public static bool IsMin(this System.DateTime date)
        {
            return date.Date == DateTime.MinValue.Date;
        }

        public static bool IsNow(this System.DateTime date)
        {
            return date.Date == DateTime.Now.Date;
        }

    }
}
