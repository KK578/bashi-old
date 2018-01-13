using System;
using NUnit.Framework;
using Slack.Api.Message.Response.Rtm;

namespace Slack.Api.Rtm.Test
{
    [TestFixture]
    public class SocketDecoderTest
    {
        [Test]
        public void Deserialize_Hello_ShouldStateTypeIsHello()
        {
            var response = SocketDecoder.Deserialize("{\"type\":\"hello\"}");
            
            Assert.That(response.Type, Is.EqualTo("hello"));
        }
        
        [Test]
        public void Deserialize_WithUnknownType_ShouldThrowException()
        {
            var exception = Assert.Throws<NotImplementedException>(TestFunction);
            Assert.That(exception.Message, Contains.Substring("will-never-be-valid"));

            void TestFunction()
            {
                SocketDecoder.Deserialize("{\"type\":\"will-never-be-valid\"}");
            }
        }
        
        [Test]
        [TestCase("hello", typeof(HelloResponse))]
        [TestCase("pong", typeof(PongResponse))]
        [TestCase("user_typing", typeof(UserTypingResponse))]
        public void Deserialize_ShouldCreateRelatedResponseType(string type, Type expectedType)
        {
            var response = SocketDecoder.Deserialize($"{{\"type\":\"{type}\"}}");
            
            Assert.That(response.Response, Is.TypeOf(expectedType));
        }

        [Test]
        [TestCase("message", null, typeof(MessageResponse))]
        [TestCase("message", "bot_message", typeof(BotMessageResponse))]
        public void Deserialize_Message_ShouldCreateRelatedMessageResponseType(string type, string subtype, Type expectedType)
        {
            var innerSubtype = string.IsNullOrEmpty(subtype) ? "" : $",\"subtype\":\"{subtype}\"";
            var message = ($"{{\"type\":\"{type}\"{innerSubtype}}}");

            var response = SocketDecoder.Deserialize(message);
            
            Assert.That(response.Response, Is.TypeOf(expectedType));
        }
    }
}
