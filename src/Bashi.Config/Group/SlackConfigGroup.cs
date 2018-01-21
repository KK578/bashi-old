using Bashi.Core.Interface.Config.Group;

namespace Bashi.Config.Group
{
    public class SlackConfigGroup : ISlackConfigGroup
    {
        public string BotToken { get; set; }
        public int PingTimeout { get; set; } = 5000;
    }
}
