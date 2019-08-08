using NUnit.Framework;
using HttpClientMock;
using ExampleHttpClientMock.Service;
using System.Net.Http;
using Newtonsoft.Json;

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

        //[Test]
        //public void GetValues_CatchesHttpCallError()
        //{
        //    var mockedHttpClient = HttpClientMock.HttpClientMock.GetMockedHttpClientError(new System.Exception("HttpClient error!"));
        //    var httpClientDependentService = new HttpClientDependentService(mockedHttpClient);

        //    Assert.Throws<System.Exception>(
        //        () => httpClientDependentService.GetValues()
        //    );
        //}

        //[TestCase(@"[")]
        //[TestCase(@"[single value]")]
        //[TestCase(@"[""some value,""another value""]")]
        //public void GetValues_InvalidHttpCallResponse(string mockedReturnValue)
        //{
        //    var mockedHttpClient = HttpClientMock.HttpClientMock.GetMockedHttpClient(mockedReturnValue, System.Net.HttpStatusCode.OK);
        //    var httpClientDependentService = new HttpClientDependentService(mockedHttpClient);
        //    var serviceCallResult = httpClientDependentService.GetValues();

        //    Assert.Throws<System.Exception>(
        //        () => httpClientDependentService.GetValues()
        //    );
        //}

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
    }
}