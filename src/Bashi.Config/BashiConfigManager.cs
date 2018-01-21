using System;
using Bashi.Config.Group;
using Bashi.Core.Interface.Config;
using Bashi.Core.Interface.Config.Group;

namespace Bashi.Config
{
    public class BashiConfigManager : IBashiConfigManager
    {
        private readonly SlackConfigGroup slackConfigGroup;

        public ISlackConfigGroup SlackConfigGroup => slackConfigGroup;

        public BashiConfigManager(IEnvConfigParser parser, IBashiConfigFile file)
        {
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

                    default:
                        Console.WriteLine($"Unknown ConfigFile key '{keyValuePair.Key}'");
                        break;
                }
            }
        }
    }
}
