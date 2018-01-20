using System;
using SlackApi.Core.Data.Message.Rtm.Response.Rtm;
using SlackApi.Core.Events.Rtm;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm
{
    public class SlackRtmEventPublisher : ISlackRtmEventPublisher
    {
        public event EventHandler<RtmMessageEventArgs<BaseRtmResponse>> AllMessages;

        public event EventHandler<RtmMessageEventArgs<HelloResponse>> HelloMessage;
        public event EventHandler<RtmMessageEventArgs<PongResponse>> PongMessage;
        public event EventHandler<RtmMessageEventArgs<UserTypingResponse>> UserTypingMessage;

        public event EventHandler<RtmMessageEventArgs<BotMessageResponse>> BotMessage;
        public event EventHandler<RtmMessageEventArgs<UserMessageResponse>> UserMessage;

        public void Fire(BaseRtmResponse response)
        {
            AllMessages?.Invoke(this, new RtmMessageEventArgs<BaseRtmResponse>(response));
            switch (response.Type)
            {
                case "hello":
                    HelloMessage?.Invoke(this, new RtmMessageEventArgs<HelloResponse>(response));
                    break;
                case "pong":
                    PongMessage?.Invoke(this, new RtmMessageEventArgs<PongResponse>(response));
                    break;
                case "user_typing":
                    UserTypingMessage?.Invoke(this, new RtmMessageEventArgs<UserTypingResponse>(response));
                    break;
                case "message":
                    switch (((MessageResponse)response).Subtype)
                    {
                        case "bot_message":
                            BotMessage?.Invoke(this, new RtmMessageEventArgs<BotMessageResponse>(response));
                            break;
                        default:
                            UserMessage?.Invoke(this, new RtmMessageEventArgs<UserMessageResponse>(response));
                            break;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
