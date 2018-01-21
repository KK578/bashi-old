using Bashi.Core.Interface.Config.Group;

namespace Bashi.Core.Interface.Config
{
    public interface IBashiConfigManager
    {
        ISlackConfigGroup SlackConfigGroup { get; }
    }
}
