using HttpClientMock.Test.Utility;
using NUnit.Framework;
using System.Net.Http;

namespace Tests
{
    public class ValueCodeResponseTest
    {
        private HttpClientDependentClass _httpClientDependentClass;
        private HttpClient _mockedHttpClient;

        [SetUp]
        public void Setup()
        {
            _mockedHttpClient = HttpClientMock.HttpClientMock.GetMockedHttpClient("Hello Mr. Walrus", System.Net.HttpStatusCode.OK);
            _httpClientDependentClass = new HttpClientDependentClass(_mockedHttpClient);
        }

        [Test]
        public void GetStringAsyncReturns()
        {
            var returnValue = _httpClientDependentClass.GetStringAsync();
            Assert.AreEqual("Hello Mr. Walrus", returnValue);
        }

        [Test]
        public void GetAsyncReturns()
        {
            var returnValue = _httpClientDependentClass.GetAsync();
            Assert.AreEqual("Hello Mr. Walrus", returnValue);
        }


        [Test]
        public void GetReponseReturnsOk()
        {
            var returnValue = _httpClientDependentClass.GetResponseMessage();
            Assert.IsTrue(returnValue.IsSuccessStatusCode);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, returnValue.StatusCode);
            Assert.AreEqual("Hello Mr. Walrus", returnValue.Content.ReadAsStringAsync().Result);
        }

        [TestCase(System.Net.HttpStatusCode.Ambiguous)]
        [TestCase(System.Net.HttpStatusCode.BadGateway)]
        [TestCase(System.Net.HttpStatusCode.BadRequest)]
        [TestCase(System.Net.HttpStatusCode.Conflict)]
        [TestCase(System.Net.HttpStatusCode.ExpectationFailed)]
        [TestCase(System.Net.HttpStatusCode.Forbidden)]
        [TestCase(System.Net.HttpStatusCode.GatewayTimeout)]
        [TestCase(System.Net.HttpStatusCode.Gone)]
        [TestCase(System.Net.HttpStatusCode.HttpVersionNotSupported)]
        [TestCase(System.Net.HttpStatusCode.InternalServerError)]
        [TestCase(System.Net.HttpStatusCode.MethodNotAllowed)]
        [TestCase(System.Net.HttpStatusCode.MovedPermanently)]
        [TestCase(System.Net.HttpStatusCode.NotFound)]
        [TestCase(System.Net.HttpStatusCode.Unauthorized)]
        public void GetReponseReturnsBadStatus(System.Net.HttpStatusCode statusCodeIn)
        {
            _mockedHttpClient = HttpClientMock.HttpClientMock.GetMockedHttpClient("Bad Requst", statusCodeIn);
            _httpClientDependentClass = new HttpClientDependentClass(_mockedHttpClient);
            var returnValue = _httpClientDependentClass.GetResponseMessage();
            Assert.IsTrue(!returnValue.IsSuccessStatusCode);
            Assert.AreEqual(statusCodeIn, returnValue.StatusCode);
            Assert.AreEqual("Bad Requst", returnValue.Content.ReadAsStringAsync().Result);
        }
    }
}