using Moq;
using System;
using System.Net.Http;

namespace HttpClientMock
{
    public static class MockHttpMessageHandlerExtension
    {
        internal static HttpClient ToHttpClient(this Mock<HttpMessageHandler> mockClient, string mockUrl = "http://blargh")
        {
            return new HttpClient(mockClient.Object)
            {
                BaseAddress = new Uri(mockUrl)
            };
        }
    }
}
