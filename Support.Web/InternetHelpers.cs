#if PORTABLE
using Platform.Support.Core;
#else
using Platform.Support;
#endif
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif

    namespace Web
    {

#if !PORTABLE

        public static class InternetHelpers
        {

            internal const string iphost = "http://ipinfo.io/";

            public static IPAddress GetExternalIP()
            {
                IPAddress result = IPAddress.Parse("127.0.0.1");
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(iphost + "ip") as HttpWebRequest;
                    var response = request.GetResponse();

                    using (var reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                    {

                        string responseText = reader.ReadToEnd();
                        responseText = responseText.Replace("\n", "");
                        result = IPAddress.Parse(responseText);

                    }

                }
                catch (Exception ex)
                {
                    ex.DebugThis();
                }

                return result;

            }


            internal static bool hasInternetConnection;

            public static bool HasInternetConnection()
            {
                hasInternetConnection = GetExternalIP().Equals(IPAddress.Parse("127.0.0.1"));
                return hasInternetConnection;
            }

#if NETFX_45 && !PORTABLE

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


#endif
        }

#endif

    }

#if PORTABLE
    }
#endif

}