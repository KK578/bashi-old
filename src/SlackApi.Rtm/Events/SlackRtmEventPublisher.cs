using System;
using SlackApi.Core.Data.Message.Rtm.Response.Rtm;
using SlackApi.Core.Events.Rtm;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm.Events
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

            switch (response) {
                case HelloResponse helloResponse:
                    HelloMessage?.Invoke(this, new RtmMessageEventArgs<HelloResponse>(helloResponse));
                    break;
                case PongResponse pongResponse:
                    PongMessage?.Invoke(this, new RtmMessageEventArgs<PongResponse>(pongResponse));
                    break;
                case UserTypingResponse userTypingResponse:
                    UserTypingMessage?.Invoke(this, new RtmMessageEventArgs<UserTypingResponse>(userTypingResponse));
                    break;
                case BaseMessageResponse messageResponse:
                    switch (messageResponse)
                    {
                        case BotMessageResponse botMessageResponse:
                            BotMessage?.Invoke(this, new RtmMessageEventArgs<BotMessageResponse>(botMessageResponse));
                            break;
                        default:
                            UserMessage?.Invoke(this, new RtmMessageEventArgs<UserMessageResponse>(messageResponse));
                            break;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
