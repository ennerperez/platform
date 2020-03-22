#if PORTABLE
using Platform.Support.Core;
#else
#endif

using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif

    namespace Net
    {
#if !PORTABLE

        public static class InternetInformation
        {
#if !PORTABLE

            private static IPAddress externalIP;

            public static IPAddress ExternalIP
            {
                get
                {
                    IPAddress.TryParse(GetExternalIP(), out externalIP);
                    return externalIP;
                }
            }

#endif

            internal const string pingHost = "http://google.com/";
            internal const string ipHost = "http://ipinfo.io/";

            public static bool PingHost(string hostname)
            {
                bool pingable = false;
                Ping pinger = new Ping();
                try
                {
                    PingReply reply = pinger.Send(hostname);
                    pingable = reply.Status == IPStatus.Success;
                }
                catch (PingException)
                {
                    // Discard PingExceptions and return false;
                }
                return pingable;
            }

            public static async Task<bool> PingHostAsync(string hostname)
            {
                bool pingable = false;
                Ping pinger = new Ping();
                try
                {
#if NETFX_45 || NETCORE
                    PingReply reply = await pinger.SendPingAsync(hostname);
#else
                    PingReply reply = await pinger.SendTaskAsync(hostname);
#endif
                    pingable = reply.Status == IPStatus.Success;
                }
                catch (PingException)
                {
                    // Discard PingExceptions and return false;
                }
                return pingable;
            }

            public static bool CheckConnection()
            {
                var isConnected = PingHost(pingHost);
                return isConnected;
            }

            public static async Task<bool> CheckConnectionAsync()
            {
                var isConnected = await PingHostAsync(pingHost);
                return isConnected;
            }

            public static string GetExternalIP()
            {
                try
                {
                    var url = new UriBuilder(ipHost);
                    url.Path = "ip";
                    var request = (HttpWebRequest)WebRequest.Create(url.ToString());
                    request.ContentType = "text/plain";
                    var response = request.GetResponse();

                    using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        string responseText = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(responseText))
                            return responseText.Trim();
                    }
                }
                catch (Exception ex)
                {
                    ex.DebugThis();
                }

                return string.Empty;
            }

            public static async Task<string> GetExternalIPAsync()
            {
                try
                {
                    var url = new UriBuilder(ipHost);
                    url.Path = "ip";
                    var request = (HttpWebRequest)WebRequest.Create(url.ToString());
                    request.ContentType = "text/plain";
                    var response = await request.GetResponseAsync();

                    using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        string responseText = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(responseText))
                            return responseText.Trim();
                    }
                }
                catch (Exception ex)
                {
                    ex.DebugThis();
                }

                return string.Empty;
            }
        }

#endif
    }

#if PORTABLE
    }
#endif
}