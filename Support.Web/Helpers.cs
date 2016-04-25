using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

#if NETFX_45 && !PORTABLE

namespace Platform.Support.Web
{

    public static class Helpers
    {

        internal const string iphost = "http://ipinfo.io/";

#if !PORTABLE
        public static async Task<IPAddress> GetExternalIPAsync()
#else
        public static async Task<string> GetExternalIPAsync()
#endif
        {
#if !PORTABLE
            IPAddress result = IPAddress.Parse("127.0.0.1");

#else
            string result = "127.0.0.1";
#endif
            try
            {
                var request = WebRequest.Create(iphost + "ip") as HttpWebRequest;
                var response = await request.GetResponseAsync();

                using (var reader = new StreamReader(response.GetResponseStream(), true))
                {
                    string responseText = reader.ReadToEnd();
#if !PORTABLE
                    result = IPAddress.Parse(responseText);
#else
                    result = responseText;
#endif
                }

            }
            catch
            {
            }
            return result;
        }

        public static async Task<double[]> GetExternalLocationAsync()
        {
            try
            {

                var request = WebRequest.Create(iphost + "loc") as HttpWebRequest;
                var response = await request.GetResponseAsync();

                using (var reader = new StreamReader(response.GetResponseStream(), true))
                {
                    string responseText = reader.ReadToEnd();
                    var locs = responseText.Split(',');
                    return new double[] { double.Parse(locs[0]), double.Parse(locs[1]) };
                }

            }
            catch
            {
                return null;
            }
        }

    }
    
}

#endif