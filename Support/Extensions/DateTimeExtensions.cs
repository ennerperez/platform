using System;
#if PORTABLE
using Helpers = Platform.Support.Core.DateTimeHelpers;
#else
using Helpers = Platform.Support.DateTimeHelpers;
#endif

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif
        public static class DateTimeExtensions
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
#if PORTABLE
    }
#endif
}
