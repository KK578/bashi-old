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
                    .Returns(new Task(() => { }));
                subject = mock.Create<WebSocketManager>();

                await subject.ConnectAsync(new Uri("http://localhost"));

                mock.Mock<IClientWebSocket>()
                    .Verify(x => x.ConnectAsync(It.IsAny<Uri>(), It.IsAny<CancellationToken>()));
            }
        }
    }
}
