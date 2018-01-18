using System;
using Autofac.Extras.Moq;
using NUnit.Framework;
using SlackApi.Core.Data.Message.Rtm.Response.Rtm;

namespace SlackApi.Rtm.Test
{
    [TestFixture]
    public class RtmResponseFactoryTest
    {
        private RtmResponseFactory subject;

        [SetUp]
        public void Setup()
        {
            using (var mock = AutoMock.GetLoose())
            {
                subject = mock.Create<RtmResponseFactory>();
            }
        }

        [Test]
        public void Deserialize_WithUnknownType_ShouldThrowException()
        {
            var exception = Assert.Throws<NotImplementedException>(TestFunction);
            Assert.That(exception.Message, Contains.Substring("will-never-be-valid"));

            void TestFunction()
            {
                subject.CreateResponse("{\"type\":\"will-never-be-valid\"}");
            }
        }

        [Test]
        [TestCase("hello", typeof(HelloResponse))]
        [TestCase("pong", typeof(PongResponse))]
        [TestCase("user_typing", typeof(UserTypingResponse))]
        public void Deserialize_ShouldCreateRelatedResponseType(string type, Type expectedType)
        {
            var response = subject.CreateResponse($"{{\"type\":\"{type}\"}}");

            Assert.That(response, Is.TypeOf(expectedType));
        }

        [Test]
        [TestCase("message", null, typeof(MessageResponse))]
        [TestCase("message", "bot_message", typeof(BotMessageResponse))]
        public void Deserialize_Message_ShouldCreateRelatedMessageResponseType(
            string type,
            string subtype,
            Type expectedType)
        {
            var innerSubtype = string.IsNullOrEmpty(subtype) ? "" : $",\"subtype\":\"{subtype}\"";
            var message = ($"{{\"type\":\"{type}\"{innerSubtype}}}");

            var response = subject.CreateResponse(message);

            Assert.That(response, Is.TypeOf(expectedType));
        }
    }
}
