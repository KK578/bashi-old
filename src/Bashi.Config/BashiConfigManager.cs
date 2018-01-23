using Bashi.Config.Group;
using Bashi.Core.Interface.Config;
using Bashi.Core.Interface.Config.Group;
using Bashi.Core.Interface.Log;

namespace Bashi.Config
{
    internal class BashiConfigManager : IBashiConfigManager
    {
        private readonly IBashiLogger log;
        private readonly SlackConfigGroup slackConfigGroup;

        public ISlackConfigGroup SlackConfigGroup => slackConfigGroup;

        public BashiConfigManager(IEnvConfigParser parser, IBashiConfigFile file, IBashiLogger log)
        {
            this.log = log;
            slackConfigGroup = new SlackConfigGroup();

            ParseConfig(parser, file);
        }

        private void ParseConfig(IEnvConfigParser parser, IBashiConfigFile file)
        {
            var keyValuePairs = parser.Parse(file.ReadLines());

            foreach (var keyValuePair in keyValuePairs)
            {
                switch (keyValuePair.Key)
                {
                    case "SLACK_BOT_TOKEN":
                        slackConfigGroup.BotToken = keyValuePair.Value;
                        break;

                    case "SLACK_PING_TIMEOUT":
                        slackConfigGroup.PingTimeout = int.Parse(keyValuePair.Value);
                        break;

                    default:
                        log.Error($"Unknown ConfigFile key '{keyValuePair.Key}'");
                        break;
                }
            }
        }
    }
}
