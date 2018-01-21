using Bashi.Core.Interface.Config;

namespace Bashi.Config.Groups
{
    public class SlackConfigGroup : ISlackConfigGroup
    {
        public string BotToken { get; set; }
    }
}
