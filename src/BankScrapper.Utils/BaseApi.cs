using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankScrapper.Utils
{
    public abstract class BaseApi : IDisposable
    {
        private readonly HttpClient _httpClient;

        public BaseApi()
        {
            _httpClient = new HttpClient
            {
                Timeout = new TimeSpan(0, 1, 0)
            };
        }

        public BaseApi(string baseUrl)
        {
            if (baseUrl.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(baseUrl));

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = new TimeSpan(0, 1, 0)
            };
        }

        protected HttpRequestHeaders DefaultRequestHeaders => _httpClient.DefaultRequestHeaders;

        public void Dispose() => 
            _httpClient.Dispose();

        protected async Task<T> GetAsync<T>(string relativeUrl)
        {
            var content = await GetStringAsync(relativeUrl);
            return content.IsNullOrEmpty() ? default(T) : JsonConvert.DeserializeObject<T>(content);
        }

        protected Task<Stream> GetStreamAsync(string relativeUrl) => 
            GetAsync(relativeUrl, c => c.ReadAsStreamAsync());

        protected Task<string> GetStringAsync(string relativeUrl) => 
            GetAsync(relativeUrl, c => c.ReadAsStringAsync());

        protected Task<T> PostJsonAsync<T>(string relativeUrl, object value) =>
            PostWithJsonResponseAsync<T>(relativeUrl, new JsonContent(value));

        protected Task PostUrlEncodedAsync(string relativeUrl, IDictionary<string, string> values) =>
            _httpClient.PostAsync(relativeUrl, new FormUrlEncodedContent(values));

        protected Task<T> PostUrlEncodedAsync<T>(string relativeUrl, IDictionary<string, string> values) =>
            PostWithJsonResponseAsync<T>(relativeUrl, new FormUrlEncodedContent(values));

        protected Task<string> PostUrlEncodedWithStringResponseAsync(string relativeUrl, IDictionary<string, string> values) =>
            PostAsync(relativeUrl, new FormUrlEncodedContent(values), c => c.ReadAsStringAsync());

        private async Task<T> GetAsync<T>(string relativeUrl, Func<HttpContent, Task<T>> sucessCallbackTask)
        {
            using (var response = await _httpClient.GetAsync(relativeUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await sucessCallbackTask(response.Content);
                }
            }

            return default(T);
        }

        private async Task<T> PostAsync<T>(string relativeUrl, HttpContent content, Func<HttpContent, Task<T>> successCallbackTask)
        {
            using (var response = await _httpClient.PostAsync(relativeUrl, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await successCallbackTask(response.Content);
                }
            }

            return default(T);
        }

        private async Task<T> PostWithJsonResponseAsync<T>(string relativeUrl, HttpContent content)
        {
            var responseContent = await PostAsync(relativeUrl, content, c => c.ReadAsStringAsync());
            return responseContent.IsNullOrEmpty() ? default(T) : JsonConvert.DeserializeObject<T>(responseContent);
        }

        private class JsonContent : StringContent
        {
            public JsonContent(object value) 
                : base(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            {
            }
        }
    }
}