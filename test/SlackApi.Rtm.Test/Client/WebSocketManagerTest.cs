using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using SlackApi.Core.Interface.Rtm;
using SlackApi.Rtm.Client;

namespace SlackApi.Rtm.Test.Client
{
    public class WebSocketManagerTest
    {
        private class TestableClientWebSocket : IClientWebSocket
        {
            private readonly byte[] byteArray;

            public TestableClientWebSocket(byte[] byteArray)
            {
                this.byteArray = byteArray;
            }

            public WebSocketState State { get; }
            
            public async Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> arraySegment, CancellationToken cancellationToken)
            {
                var array = arraySegment.Array;

                for (int i = 0; i < byteArray.Length; i++)
                {
                    array[i] = byteArray[i];
                }

                await Task.Delay(0);
                
                return null;
            }

            public Task SendAsync(ArraySegment<byte> buffer,
                                  WebSocketMessageType messageType,
                                  bool endOfMessage,
                                  CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
        
        private WebSocketManager subject;

        [Test]
        public async Task ConnectAsync_ConnectsToUnderlyingWebSocket()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IClientWebSocket>()
                    .Setup(x => x.ConnectAsync(It.IsAny<Uri>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.Delay(0));
                subject = mock.Create<WebSocketManager>();

                await subject.ConnectAsync(new Uri("http://localhost"));

                mock.Mock<IClientWebSocket>()
                    .Verify(x => x.ConnectAsync(It.IsAny<Uri>(), It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        [Test]
        [TestCase("Hello", false)]
        [TestCase("Hello", true)]
        public void ReceiveData_ReturnsResultFromBuffer(string message, bool shouldFillArrayWithNullCharacter)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var byteArray = ToByteArray(message, shouldFillArrayWithNullCharacter);
                var clientWebSocket = new TestableClientWebSocket(byteArray); 
                mock.Provide<IClientWebSocket>(clientWebSocket);
                subject = mock.Create<WebSocketManager>();

                var task = subject.ReceiveData();
                task.Wait();
                var result = task.Result;

                Assert.That(result, Is.EqualTo("Hello"));
            }
        }

        private static byte[] ToByteArray(string message, bool shouldFillArrayWithNullCharacter)
        {
            var array = message.ToCharArray().Select(c => (byte)c).ToArray();

            if (shouldFillArrayWithNullCharacter)
            {
                var maxSizeArray = new byte[1024];
                array.CopyTo(maxSizeArray, 0);
                return maxSizeArray;
            }
            else
            {
                return array;
            }
        }
    }
}
