using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using Platform.Support.Reflection;

namespace Platform.Support
{
#if PORTABLE
    name Core
    {
#endif
    namespace Web
    {

        public static class Methods
        {

            [DefaultValue(DecompressionMethods.GZip)]
            public static DecompressionMethods AutomaticDecompression { get; set; } = DecompressionMethods.GZip;

            [DefaultValue("text/plain")]
            public static string Accept { get; set; } = "text/plain";

            internal static HttpClient getHttpClient(string token = "")
            {
                var handler = new HttpClientHandler();
                handler.AutomaticDecompression = AutomaticDecompression;

                HttpClient client = new HttpClient(handler);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Accept));

                switch (handler.AutomaticDecompression)
                {
                    case DecompressionMethods.GZip:
                        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                        break;
                    case DecompressionMethods.Deflate:
                        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return client;
            }
            internal static async Task<R> processHttpResponseMessage<R>(HttpResponseMessage response)
            {
                object result = null;

                if (typeof(R) == typeof(string))
                    result = await response.Content.ReadAsStringAsync();

                if (typeof(R) == typeof(Stream))
                    result = await response.Content.ReadAsStreamAsync();

                if (typeof(R) == typeof(byte[]))
                    result = await response.Content.ReadAsByteArrayAsync();

                if (result == null)
                {
                    var path = Path.Combine(Assembly.GetEntryAssembly().DirectoryPath(), "Newtonsoft.Json.dll");
                    if (File.Exists(path))
                    {
                        var newtonsoftJson = Assembly.LoadFile(path);
                        var jsonConvert = newtonsoftJson.GetType("Newtonsoft.Json.JsonConvert");

                        var data = await response.Content.ReadAsStringAsync();
                        var methods = from item in jsonConvert.GetMethods()
                                      where item.Name == "DeserializeObject" && item.IsGenericMethodDefinition
                                      select item;
                        var generic = methods.FirstOrDefault().MakeGenericMethod(typeof(R));
                        result = (R)generic.Invoke(null, new object[] { data });
                    }
                }

                if (result != null)
                    return (R)result;
                else
                    return default(R);
            }

            public static async Task<R> GetAsync<T, R>(string url, string token = "")
            {
                try
                {
                    var client = getHttpClient(token);
                    var response = await client.GetAsync(url);
                    return await processHttpResponseMessage<R>(response);
                }
                catch (Exception ex)
                {
                    ex.Message.DebugThis();
                    return default(R);
                }
            }
            public async static Task<T> GetAsync<T>(string url, string token = "")
            {
                return await GetAsync<T, T>(url, token);
            }

            public async static Task<R> PostAsync<T, R>(string url, T model = default(T), HttpContent content = null, string token = "")
            {
                try
                {
                    var client = getHttpClient(token);
                    var response = await client.PostAsync(url, content);
                    return await processHttpResponseMessage<R>(response);
                }
                catch (Exception ex)
                {
                    ex.Message.DebugThis();
                    return default(R);
                }
            }
            public async static Task<T> PostAsync<T>(string url, T model = default(T), HttpContent content = null, string token = "")
            {
                return await PostAsync<T, T>(url, model, content, token);
            }

            public async static Task<R> PutAsync<T, R>(string url, T model = default(T), HttpContent content = null, string token = "")
            {
                try
                {

                    var client = getHttpClient(token);
                    var response = await client.PutAsync(url, content);
                    return await processHttpResponseMessage<R>(response);

                }
                catch (Exception ex)
                {
                    ex.Message.DebugThis();
                    return default(R);
                }
            }
            public async static Task<T> PutAsync<T>(string url, T model = default(T), HttpContent content = null, string token = "")
            {
                return await PutAsync<T, T>(url, model, content, token);
            }

            public static async Task<R> DeleteAsync<T, R>(string url, string token = "")
            {
                try
                {
                    var client = getHttpClient(token);
                    var response = await client.DeleteAsync(url);
                    return await processHttpResponseMessage<R>(response);
                }
                catch (Exception ex)
                {
                    ex.Message.DebugThis();
                    return default(R);
                }
            }
            public async static Task<T> DeleteAsync<T>(string url, string token = "")
            {
                return await DeleteAsync<T, T>(url, token);
            }

        }

    }
#if PORTABLE
    }
#endif
}
