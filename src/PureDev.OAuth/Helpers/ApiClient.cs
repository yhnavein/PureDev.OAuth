using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PureDev.OAuth.Helpers
{
    public static class ApiClient
    {
        public static T GetData<T>(string url, IEnumerable<KeyValuePair<string, string>> formData = null, string bearerToken = null)
        {
            if (formData != null)
            {
                var qs = GenerateQueryString(formData);
                url = url + "?" + qs;
            }
            var jsonString = GetData(url, bearerToken);
            var result = jsonString.Result;

            return JsonConvert.DeserializeObject<T>(result);
        }

        public static T PostData<T>(string url, IEnumerable<KeyValuePair<string, string>> formData)
        {
            var jsonString = PostData(url, formData);
            
            return JsonConvert.DeserializeObject<T>(jsonString.Result);
        }

        private static string GenerateQueryString(IEnumerable<KeyValuePair<string, string>> dict)
        {
            return string.Join("&", dict.Select(kvp => string.Concat(Uri.EscapeDataString(kvp.Key), "=", Uri.EscapeDataString(kvp.Value.ToString()))));
        }

        private static async Task<string> GetData(string url, string bearerToken = null)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get,
                };
                if (bearerToken != null)
                    request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "Bearer " + bearerToken);

                request.Headers.Add(HttpRequestHeader.AcceptEncoding.ToString(), "gzip,deflate");

                var response = await client.SendAsync(request);
                //                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        private static async Task<string> PostData(string url, IEnumerable<KeyValuePair<string, string>> formData)
        {
            using (var client = new HttpClient())
            {
                var formContent = new FormUrlEncodedContent(formData);

                var response = await client.PostAsync(new Uri(url), formContent);
                //                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

    }
}
