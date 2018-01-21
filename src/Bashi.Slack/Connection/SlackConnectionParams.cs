using Bashi.Core.Interface.Connection;

namespace Bashi.Slack.Connection
{
    public class SlackConnectionParams : ISlackConnectionParams
    {
        public string BotToken { get; }

        public SlackConnectionParams(string botToken)
        {
            BotToken = botToken;
        }
    }
}
