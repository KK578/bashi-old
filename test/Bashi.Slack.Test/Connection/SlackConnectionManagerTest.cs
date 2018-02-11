using System;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Bashi.Slack.Connection;
using Moq;
using NUnit.Framework;
using SlackApi.Core.Data.Message.Web.Response.Rtm;
using SlackApi.Core.Interface.Rtm;
using SlackApi.Core.Interface.Web;

namespace Bashi.Slack.Test.Connection
{
    public class SlackConnectionManagerTest
    {
        private SlackConnectionManager subject;

        private void SetUp(string url)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var response = new RtmConnectResponse { WebSocketUrl = url };

                mock.Mock<ISlackWebClient>()
                    .Setup(x => x.RtmConnectAsync(It.IsAny<string>()))
                    .Returns(Task.FromResult(response));
                mock.Mock<ISlackRtmClient>()
                    .Setup(x => x.ConnectAsync(It.IsAny<string>()))
                    .Returns(Task.Delay(0));

                subject = mock.Create<SlackConnectionManager>();
            }
        }

        [Test]
        public void Connect_GivenEmptyStringShouldFail()
        {
            SetUp("");
            Assert.ThrowsAsync<Exception>(async () => await subject.ConnectAsync());
        }

        [Test]
        public void Connect_ShouldAttemptAConnection()
        {
            SetUp("http://localhost");
            subject.ConnectAsync();
        }
    }
}
