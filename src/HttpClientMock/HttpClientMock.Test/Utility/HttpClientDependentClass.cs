using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HttpClientMock.Test.Utility
{
    public class HttpClientDependentClass
    {
        private HttpClient _httpClient;

        public HttpClientDependentClass(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpResponseMessage GetResponseMessage()
        {
            return _httpClient.GetAsync("http://bad-url-location").Result;
        }

        public string GetAsync()
        {
            return _httpClient.GetAsync("http://bad-url-location").Result.Content.ReadAsStringAsync().Result;
        }

        public string GetStringAsync()
        {
            return _httpClient.GetStringAsync("http://bad-url-location").Result;
        }
    }
}
