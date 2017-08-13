using System;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        public static class DateTimeHelper
        {
            public static string ISO8601(DateTime date)
            {
                return string.Format("{0:yyyy-MM-dd HH:mm:ss}", date);
            }

            public static DateTime FromUnixTime(this long unixTime)
            {
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return dateTime.AddSeconds((double)(unixTime + 18000L));
            }

            public static string ElapsedTime(this System.DateTime date)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(date);
                int num = (int)timeSpan.TotalDays;
                if (num > 1)
                {
                    if (num / 7 > 0)
                    {
                        int num2 = num / 7;
                        return num2.ToString() + " week" + ((num2 > 1) ? "s" : "") + " ago";
                    }
                    return num.ToString() + " day" + ((num > 1) ? "s" : "") + " ago";
                }
                else
                {
                    int num3 = (int)timeSpan.TotalHours;
                    if (num3 > 0)
                    {
                        return num3.ToString() + " hour" + ((num3 > 1) ? "s" : "") + " ago";
                    }
                    int num4 = (int)timeSpan.TotalMinutes;
                    if (num4 > 0)
                    {
                        return num4.ToString() + " minute" + ((num4 > 1) ? "s" : "") + " ago";
                    }
                    return "few seconds ago";
                }
            }
        }

#if PORTABLE
    }

#endif
}