using System;
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
        public async Task ReceiveData_ReturnsResultFromBuffer()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IClientWebSocket>()
                    .Setup(x => x.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>()))
                    .Callback<ArraySegment<byte>, CancellationToken>((buffer, token) =>
                                                                     {
                                                                         buffer.Array[0] = (byte)'H';
                                                                         buffer.Array[1] = (byte)'e';
                                                                         buffer.Array[2] = (byte)'l';
                                                                         buffer.Array[3] = (byte)'l';
                                                                         buffer.Array[4] = (byte)'o';

                                                                         for (var i = 5; i < 1024; i++)
                                                                         {
                                                                             buffer.Array[i] = (byte)' ';
                                                                         }
                                                                     });
                subject = mock.Create<WebSocketManager>();

                var result = await subject.ReceiveData();

                Assert.That(result, Is.EqualTo("Hello"));

                mock.Mock<IClientWebSocket>()
                    .Verify(x => x.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>()),
                            Times.Once);
            }
        }
    }
}
