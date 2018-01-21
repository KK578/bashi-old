namespace Bashi.Core.Interface.Config
{
    public interface IConfigManager
    {
        ISlackConfigGroup SlackConfigGroup { get; }
    }
}
