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
                    var url = new UriBuilder(iphost);
                    url.Path = "ip";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.ToString()) as HttpWebRequest;
                    request.ContentType = "text/plain";
                    var response = request.GetResponse();

                    using (var reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                    {
                        string responseText = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(responseText))
                            result = IPAddress.Parse(responseText.Trim());
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

        }

#endif

    }

#if PORTABLE
    }
#endif

}