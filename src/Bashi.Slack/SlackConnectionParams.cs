using Bashi.Core;

namespace Bashi.Slack
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
