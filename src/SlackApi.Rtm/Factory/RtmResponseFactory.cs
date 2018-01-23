using System;
using Newtonsoft.Json;
using SlackApi.Core.Base.Rtm;
using SlackApi.Core.Data.Message.Rtm.Response.Rtm;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm.Factory
{
    public class RtmResponseFactory : IRtmResponseFactory
    {
        public BaseRtmResponse CreateResponse(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            string type = jsonObject.type;

            switch (type)
            {
                case "hello":
                    return CreateResponse<HelloResponse>(json);
                case "pong":
                    return CreateResponse<PongResponse>(json);
                case "user_typing":
                    return CreateResponse<UserTypingResponse>(json);
                case "message":
                    return CreateMessageResponse(json);

                default:
                    throw new NotImplementedException($"Message Type '{type}' is not yet supported.");
            }
        }

        private static BaseMessageResponse CreateMessageResponse(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            string subtype = jsonObject.subtype;

            switch (subtype)
            {
                case "bot_message":
                    return CreateResponse<BotMessageResponse>(json);

                default:
                    return CreateResponse<UserMessageResponse>(json);
            }
        }

        private static T CreateResponse<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }
}
