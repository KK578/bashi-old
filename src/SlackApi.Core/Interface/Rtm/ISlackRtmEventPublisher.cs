using System;
using SlackApi.Core.Data.Message.Rtm.Response.Rtm;
using SlackApi.Core.Events.Rtm;

namespace SlackApi.Core.Interface.Rtm
{
    public interface ISlackRtmEventPublisher
    {
        event EventHandler<RtmMessageEventArgs<BaseRtmResponse>> AllMessages;

        event EventHandler<RtmMessageEventArgs<HelloResponse>> HelloMessage;
        event EventHandler<RtmMessageEventArgs<PongResponse>> PongMessage;
        event EventHandler<RtmMessageEventArgs<UserTypingResponse>> UserTypingMessage;
        event EventHandler<RtmMessageEventArgs<BotMessageResponse>> BotMessage;
        event EventHandler<RtmMessageEventArgs<UserMessageResponse>> UserMessage;

        void Fire(BaseRtmResponse response);
    }
}
