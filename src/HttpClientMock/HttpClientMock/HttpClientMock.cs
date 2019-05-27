using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientMock
{
    public static class HttpClientMock
    {
        #region Mocked Non-Exception
        public static HttpClient GetMockedHttpClient(string mockReturnValue, HttpStatusCode mockStatusCode = HttpStatusCode.OK)
        {
            return GetMockedMessageHandler(mockReturnValue, mockStatusCode).ToHttpClient();
        }

        public static Mock<HttpMessageHandler> GetMockedMessageHandler(string mockReturnValue, HttpStatusCode mockStatusCode = HttpStatusCode.OK)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpResponseMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = mockStatusCode,
                    Content = new StringContent(mockReturnValue)
                })
                .Verifiable();

            return handlerMock;
        }
        #endregion

        #region Mocked Exceptions
        public static HttpClient GetMockedHttpClientError(Exception mockException)
        {
            return GetMockedMessageHandlerError(mockException).ToHttpClient();
        }

        public static Mock<HttpMessageHandler> GetMockedMessageHandlerError(Exception mockException)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpResponseMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(() => { throw mockException; })
                .Verifiable();

            return handlerMock;
        }
        #endregion

        //#region Verifying HttpClient Call Attributes
        //public static void VerifyTimesCalled(this Mock<HttpMessageHandler> mockedHttpClient, int expectedCalls)
        //{
        //    mockedHttpClient
        //        .Protected()
        //        .Verify(
        //            "SendAsync",
        //            Times.Exactly(expectedCalls)
        //        );
        //}
        //#endregion
    }
}
