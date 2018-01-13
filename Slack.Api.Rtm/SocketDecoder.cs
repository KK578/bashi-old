using System;
using Newtonsoft.Json;
using Slack.Api.Message.Response.Rtm;

namespace Slack.Api.Rtm
{
    public static class SocketDecoder
    {
        public static WebSocketResponse Deserialize(string json)
        {
            var response = RtmResponseFactory.CreateResponse(json);

            return new WebSocketResponse(response.Type, response);
        } 
    }

    public static class RtmResponseFactory
    {
        public static BaseRtmResponse CreateResponse(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            string type = jsonObject.type;
            
            switch (type)
            {
                case "hello":
                    return CreateHelloResponse(json);
                case "pong":
                    return CreatePongResponse(json);
                case "user_typing":
                    return CreateUserTypingResponse(json);
                case "message":
                    return CreateMessageResponse(json);

                default:
                    throw new NotImplementedException($"Message Type '{type}' is not yet supported.");
            }
        }

        private static HelloResponse CreateHelloResponse(string json)
        {
            return JsonConvert.DeserializeObject<HelloResponse>(json);
        }

        private static PongResponse CreatePongResponse(string json)
        {
            return JsonConvert.DeserializeObject<PongResponse>(json);
        }

        private static BaseRtmResponse CreateUserTypingResponse(string json)
        {
            return JsonConvert.DeserializeObject<UserTypingResponse>(json);
        }

        private static MessageResponse CreateMessageResponse(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            string subtype = jsonObject.subtype;

            switch (subtype)
            {
                case "bot_message":
                    return CreateBotMessageResponse(json);

                default:
                    return JsonConvert.DeserializeObject<MessageResponse>(json);
            }
        }

        private static BotMessageResponse CreateBotMessageResponse(string json)
        {
            return JsonConvert.DeserializeObject<BotMessageResponse>(json);
        }
    }
}
