using NUnit.Framework;
using HttpClientMock;
using ExampleWebApi.Service;
using System.Net.Http;
using Newtonsoft.Json;
using System;

namespace Tests
{
    [TestFixture]
    public class HttpClientDependentServiceTest
    {
        [TestCase(@"[]")]
        [TestCase(@"[""single value""]")]
        [TestCase(@"[""some value"",""another value""]")]
        public void GetValues_ReturnsValuesFromSuccessfulHttpCall(string mockedReturnValue)
        {
            var mockedHttpClient = HttpClientMock.HttpClientMock.GetMockedHttpClient(mockedReturnValue, System.Net.HttpStatusCode.OK);
            var httpClientDependentService = new HttpClientDependentService(mockedHttpClient);
            var serviceCallResult = httpClientDependentService.GetValues();

            Assert.AreEqual(mockedReturnValue, JsonConvert.SerializeObject(serviceCallResult));
        }

        [Test]
        public void GetValues_CatchesHttpCallError()
        {
            var mockedHttpClient = HttpClientMock.HttpClientMock.GetMockedHttpClientError(new Exception("HttpClient error!"));
            var httpClientDependentService = new HttpClientDependentService(mockedHttpClient);

            Assert.Throws<System.Exception>(
                () => httpClientDependentService.GetValues()
            );
        }

        [TestCase(@"[")]
        [TestCase(@"[single value]")]
        [TestCase(@"[""some value,""another value""]")]
        public void GetValues_InvalidHttpCallResponse(string mockedReturnValue)
        {
            var mockedHttpClient = HttpClientMock.HttpClientMock.GetMockedHttpClient(mockedReturnValue, System.Net.HttpStatusCode.OK);
            var httpClientDependentService = new HttpClientDependentService(mockedHttpClient);

            Assert.Throws<Exception>(
                () => httpClientDependentService.GetValues()
            );
        }

        [TestCase(1, "some value")]
        [TestCase(2, "another value")]
        [TestCase(3, "")]
        [TestCase(3, "!/test some symb@l$")]
        public void GetValue_ReturnsValuesFromSuccessfulHttpCall(int testId, string mockedReturnValue)
        {
            var mockedHttpClient = HttpClientMock.HttpClientMock.GetMockedHttpClient(mockedReturnValue, System.Net.HttpStatusCode.OK);
            var httpClientDependentService = new HttpClientDependentService(mockedHttpClient);
            var serviceCallResult = httpClientDependentService.GetValue(testId);

            Assert.AreEqual(mockedReturnValue, serviceCallResult);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(100)]
        public void GetValue_CatchesHttpCallError(int testId)
        {
            var mockedHttpClient = HttpClientMock.HttpClientMock.GetMockedHttpClientError(new Exception("HttpClient error!"));
            var httpClientDependentService = new HttpClientDependentService(mockedHttpClient);

            string errorMessage = string.Empty;
            try
            {
                httpClientDependentService.GetValue(testId);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            Assert.AreEqual($"Caught exception while calling api/values/{testId}.", errorMessage);
        }
    }
}