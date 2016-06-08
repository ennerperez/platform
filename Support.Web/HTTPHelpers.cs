//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace Platform.Support
//{
//#if PORTABLE
//    name Core
//    {
//#endif
//    namespace Web
//    {
//        public static class HTTPHelpers
//        {

//            public async static Task<R> PostAsync<T, R>(string url, T model = default(T), HttpContext content = null, string token = "")
//            {
//                try
//                {

//                    serializerSettings();

//                    var handler = new HttpClientHandler();
//                    handler.AutomaticDecompression = DecompressionMethods.GZip;

//                    HttpClient client = new HttpClient(handler);
//                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
//                    if (!string.IsNullOrEmpty(token))
//                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//                    if (model != null)
//                    {
//                        var data = JsonConvert.SerializeObject(model);
//                        if (content == null)
//                            content = new StringContent(data, Encoding.UTF8, "application/json");
//                    }

//                    var response = await client.PostAsync(url, content);
//                    var str = await response.Content.ReadAsStringAsync();

//                    if (response.IsSuccessStatusCode)
//                    {
//                        if (!string.IsNullOrEmpty(str))
//                            return JsonConvert.DeserializeObject<R>(str);
//                    }
//#if DEBUG
//                    else
//                    {
//                        if (!string.IsNullOrEmpty(str))
//                        {
//                            var result = JsonConvert.DeserializeObject(str);
//                        }
//                    }
//#endif

//                    return default(R);

//                }
//                catch (Exception ex)
//                {
//                    ex.Message.DebugThis();
//                    return default(R);
//                }
//            }
//            public async static Task<T> PostAsync<T>(string url, T model = default(T), HttpContent content = null, string token = "")
//            {
//                return await PostAsync<T, T>(url, model, content, token);
//            }

//            public async static Task<R> GetAsync<T, R>(string url, string token = "")
//            {
//                try
//                {

//                    serializerSettings();

//                    var handler = new HttpClientHandler();
//                    handler.AutomaticDecompression = DecompressionMethods.GZip;

//                    HttpClient client = new HttpClient(handler);
//                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

//                    if (!string.IsNullOrEmpty(token))
//                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//                    var response = await client.GetAsync(url);
//                    var str = await response.Content.ReadAsStringAsync();

//                    if (response.IsSuccessStatusCode)
//                    {
//                        if (!string.IsNullOrEmpty(str))
//                            return JsonConvert.DeserializeObject<R>(str);
//                    }
//#if DEBUG
//                    else
//                    {
//                        if (!string.IsNullOrEmpty(str))
//                        {
//                            var result = JsonConvert.DeserializeObject(str);
//                        }
//                    }
//#endif

//                    return default(R);


//                }
//                catch (Exception ex)
//                {
//                    ex.Message.DebugThis();
//                    return default(R);
//                }
//            }
//            public async static Task<T> GetAsync<T>(string url, string token = "")
//            {
//                return await GetAsync<T, T>(url, token);
//            }

//            public async static Task<T> PutAsync<T>(string url, T model = default(T), HttpContent content = null, string token = "")
//            {
//                return await PutAsync<T, T>(url, model, content, token);
//            }
//            public async static Task<R> PutAsync<T, R>(string url, T model = default(T), HttpContent content = null, string token = "")
//            {
//                try
//                {

//                    serializerSettings();

//                    var handler = new HttpClientHandler();
//                    handler.AutomaticDecompression = DecompressionMethods.GZip;

//                    HttpClient client = new HttpClient(handler);
//                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
//                    if (!string.IsNullOrEmpty(token))
//                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//                    if (model != null)
//                    {
//                        var data = JsonConvert.SerializeObject(model);
//                        if (content == null)
//                            content = new StringContent(data, Encoding.UTF8, "application/json");
//                    }

//                    var response = await client.PutAsync(url, content);
//                    var str = await response.Content.ReadAsStringAsync();

//                    if (response.IsSuccessStatusCode)
//                    {
//                        if (!string.IsNullOrEmpty(str))
//                            return JsonConvert.DeserializeObject<R>(str);
//                    }

//                    return default(R);

//                }
//                catch (Exception ex)
//                {
//                    ex.Message.DebugThis();
//                    return default(R);
//                }
//            }


//        }
//    }
//#if PORTABLE
//    }
//#endif
//}
