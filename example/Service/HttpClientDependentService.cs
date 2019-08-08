using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExampleHttpClientMock.Service
{
    public interface IHttpClientDependentService
    {
        IEnumerable<string> GetValues();
        string GetValue(int id);
    }

    public class HttpClientDependentService : IHttpClientDependentService
    {
        private HttpClient _httpClient;

        public HttpClientDependentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<string> GetValues()
        {
            var rawValues = _httpClient.GetAsync("https://localhost:44386/api/values").Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<string[]>(rawValues);
        }

        public string GetValue(int id)
        {
            var rawValue = _httpClient.GetAsync($"https://localhost:44386/api/values/{id}").Result.Content.ReadAsStringAsync().Result;
            return rawValue;
        }
    }
}
