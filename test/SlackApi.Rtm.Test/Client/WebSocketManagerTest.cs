using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Bashi.Test.Util.Mock.SlackApi;
using NUnit.Framework;
using SlackApi.Core.Interface.Rtm;
using SlackApi.Rtm.Client;

namespace SlackApi.Rtm.Test.Client
{
    public class WebSocketManagerTest
    {
        private MockClientWebSocket clientWebSocket;
        private WebSocketManager subject;

        [SetUp]
        public void Setup()
        {
            using (var mock = AutoMock.GetLoose())
            {
                clientWebSocket = new MockClientWebSocket();
                mock.Provide<IClientWebSocket>(clientWebSocket);
                subject = mock.Create<WebSocketManager>();
            }
        }

        [Test]
        public async Task ConnectAsync_ConnectsToUnderlyingWebSocket()
        {
            await subject.ConnectAsync(new Uri("http://localhost"));

            Assert.That(clientWebSocket.WasMethodCalled(nameof(clientWebSocket.ConnectAsync)), Is.True);
        }

        [Test]
        public async Task ReceiveData_ReceivesFromUnderlyingWebSocket()
        {
            await subject.ReceiveData();

            Assert.That(clientWebSocket.WasMethodCalled(nameof(clientWebSocket.ReceiveAsync)), Is.True);
        }

        [Test]
        [TestCase("Hello", false)]
        [TestCase("Hello", true)]
        public async Task ReceiveData_ReturnsResultFromBuffer(string message, bool shouldFillArrayWithNullCharacter)
        {
            clientWebSocket.TestByteArray = ToByteArray(message, shouldFillArrayWithNullCharacter);

            var result = await subject.ReceiveData();

            Assert.That(result, Is.EqualTo("Hello"));
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
