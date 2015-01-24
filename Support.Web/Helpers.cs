using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Support.Web
{
    public static class Helpers
    {

        public static System.Net.IPAddress GetExternalIP()
        {
            try
            {
                string ExternalIP;
                ExternalIP = new WebClient().DownloadString("http://ipinfo.io/ip");
                return System.Net.IPAddress.Parse(ExternalIP);             
            }
            catch 
            {
                return System.Net.IPAddress.Parse("127.0.0.1");
            }
        }

        public static Double[] GetExternalLocation()
        {
            try
            {
                string ExternalLocation;
                ExternalLocation = new WebClient().DownloadString("http://ipinfo.io/loc");
                string[] locs = ExternalLocation.Split(',');

                return new Double[] {double.Parse(locs[0]), double.Parse(locs[1])};
            }
            catch
            {
                return null;
            }
        }

    }
}
