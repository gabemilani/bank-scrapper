using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankScrapper.Utils
{
    public abstract class BaseApi : IDisposable
    {
        private readonly HttpClient _httpClient;        

        public BaseApi(string baseUrl)
        {
            if (baseUrl.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = new TimeSpan(0, 1, 0)
            };
        }

        protected HttpRequestHeaders DefaultRequestHeaders => _httpClient.DefaultRequestHeaders;

        public void Dispose() => _httpClient.Dispose();            

        protected async Task<T> GetAsync<T>(string relativeUrl)
        {
            using (var response = await _httpClient.GetAsync(relativeUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }

            return default(T);
        }

        protected Task PostAsync(string relativeUrl, IDictionary<string, string> values)
        {
            return _httpClient.PostAsync(relativeUrl, new FormUrlEncodedContent(values));
        }

        protected async Task<string> PostWithStringResponseAsync(string relativeUrl, IDictionary<string, string> values)
        {
            using (var response = await _httpClient.PostAsync(relativeUrl, new FormUrlEncodedContent(values)))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }

            return null;
        }

        protected async Task<TResponse> PostWithJsonResponseAsync<TResponse>(string relativeUrl, IDictionary<string, string> values)
        {
            using (var response = await _httpClient.PostAsync(relativeUrl, new FormUrlEncodedContent(values)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }
            }

            return default(TResponse);
        }


        protected async Task<TResponse> PostWithResponseAsync<TResponse>(string relativeUrl, object value)
        {
            using (var response = await _httpClient.PostAsync(relativeUrl, new JsonContent(value)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }
            }

            return default(TResponse);
        }

        private class JsonContent : StringContent
        {
            public JsonContent(object value) : base(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            {
            }
        }
    }
}