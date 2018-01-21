using Bashi.Core.Interface.Config;

namespace Bashi.Config
{
    public class ConfigManager : IConfigManager
    {
        public ISlackConfigGroup SlackConfigGroup { get; set; }

        public ConfigManager(string configFilePath)
        {
        }
    }
}
