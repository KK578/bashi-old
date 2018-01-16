using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using Slack.Api.Message.Response.Rtm;

namespace Slack.Api.Rtm.Test
{
    [TestFixture]
    public class SocketDecoderTest
    {
        private SocketDecoder subject;

        [SetUp]
        public void Setup()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRtmResponseFactory>()
                    .Setup(x => x.CreateResponse(It.IsAny<string>()))
                    .Returns(Mock.Of<BaseRtmResponse>(x => x.Type == "hello"));

                subject = mock.Create<SocketDecoder>();
            }
        }

        [Test]
        public void Deserialize_Hello_ShouldStateTypeIsHello()
        {
            var response = subject.Deserialize("{\"type\":\"hello\"}");

            Assert.That(response.Type, Is.EqualTo("hello"));
        }
    }
}
