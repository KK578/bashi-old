using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Bashi.Config;
using Bashi.Core.Interface.Config;
using Moq;
using NUnit.Framework;

namespace Bashi.Core.Test
{
    public class BashiConfigManagerTest
    {
        private BashiConfigManager subject;

        private void SetUp(IEnumerable<KeyValuePair<string, string>> results)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IEnvConfigParser>()
                    .Setup(p => p.Parse(It.IsAny<IEnumerable<string>>()))
                    .Returns(results);
                subject = mock.Create<BashiConfigManager>();
            }
        }

        [TearDown]
        public void TearDown()
        {
            subject = null;
        }

        [Test]
        public void Constructor_DefaultValues()
        {
            SetUp(Enumerable.Empty<KeyValuePair<string, string>>());

            Assert.That(subject.SlackConfigGroup.BotToken, Is.EqualTo(null));
            Assert.That(subject.SlackConfigGroup.PingTimeout, Is.EqualTo(5000));
        }

        [Test]
        public void Constructor_SlackBotToken_LoadedCorrectly()
        {
            SetUp(new List<KeyValuePair<string, string>>
                  {
                      new KeyValuePair<string, string>("SLACK_BOT_TOKEN", "Banana")
                  });

            Assert.That(subject.SlackConfigGroup.BotToken, Is.EqualTo("Banana"));
        }

        [Test]
        public void Constructor_SlackPingTimeout_LoadedCorrectly()
        {
            SetUp(new List<KeyValuePair<string, string>>
                  {
                      new KeyValuePair<string, string>("SLACK_PING_TIMEOUT",
                                                       "1234")
                  });

            Assert.That(subject.SlackConfigGroup.PingTimeout, Is.EqualTo(1234));
        }
    }
}
