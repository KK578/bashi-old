using Bashi.Interface.Connection;

namespace Bashi.Slack.Connection
{
    public class SlackConnectionParams : IConnectionParams
    {
        public string BotToken { get; }

        public SlackConnectionParams(string botToken)
        {
            BotToken = botToken;
        }
    }
}
