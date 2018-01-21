using Autofac.Extras.Moq;
using NUnit.Framework;
using SlackApi.Core.Data.Message.Rtm.Response.Rtm;
using SlackApi.Rtm.Events;

namespace SlackApi.Rtm.Test.Events
{
    public class SlackRtmEventPublisherTest
    {
        private class TestableSlackRtmEventPublisher
        {
            private readonly SlackRtmEventPublisher publisher;

            public bool AllMessagesWasRaised { get; private set; }
            public bool HelloMessageWasRaised { get; private set; }
            public bool PongMessageWasRaised { get; private set; }
            public bool UserTypingWasRaised { get; private set; }
            public bool BotMessageWasRaised { get; private set; }
            public bool UserMessageWasRaised { get; private set; }

            public TestableSlackRtmEventPublisher()
            {
                using (var mock = AutoMock.GetLoose())
                {
                    publisher = mock.Create<SlackRtmEventPublisher>();
                }
            }

            public void SetUp()
            {
                publisher.AllMessages += (s, e) => AllMessagesWasRaised = true;
                publisher.HelloMessage += (s, e) => HelloMessageWasRaised = true;
                publisher.PongMessage += (s, e) => PongMessageWasRaised = true;
                publisher.UserTypingMessage += (s, e) => UserTypingWasRaised = true;
                publisher.BotMessage += (s, e) => BotMessageWasRaised = true;
                publisher.UserMessage += (s, e) => UserMessageWasRaised = true;
            }

            public void Fire(BaseRtmResponse response) => publisher.Fire(response);
        }

        private const int Timeout = 1000;

        private TestableSlackRtmEventPublisher subject;
        private static readonly BaseRtmResponse[] Responses =
        {
            new HelloResponse(),
            new PongResponse(),
            new UserTypingResponse(),
            new BotMessageResponse(),
            new UserMessageResponse()
        };

        [SetUp]
        public void SetUp()
        {
            subject = new TestableSlackRtmEventPublisher();
            subject.SetUp();
        }

        [Test]
        [TestCaseSource(nameof(Responses))]
        public void Fire_EachResponseTypes_RaisesAllMessagesEvent(BaseRtmResponse response)
        {
            subject.Fire(response);
            Assert.That(subject.AllMessagesWasRaised, Is.True);
        }

        [Test]
        [TestCaseSource(nameof(Responses))]
        public void Fire_EachResponseType_OnlyRaisesCorrespondingMessageEvent(BaseRtmResponse response)
        {
            subject.Fire(response);

            Assert.That(subject.HelloMessageWasRaised, Is.EqualTo(response is HelloResponse));
            Assert.That(subject.PongMessageWasRaised, Is.EqualTo(response is PongResponse));
            Assert.That(subject.UserTypingWasRaised, Is.EqualTo(response is UserTypingResponse));
            Assert.That(subject.BotMessageWasRaised, Is.EqualTo(response is BotMessageResponse));
            Assert.That(subject.UserMessageWasRaised, Is.EqualTo(response is UserMessageResponse));
        }
    }
}
