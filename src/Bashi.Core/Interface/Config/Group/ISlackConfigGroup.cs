namespace Bashi.Core.Interface.Config.Group
{
    public interface ISlackConfigGroup
    {
        string BotToken { get; }
        int PingTimeout { get; }
    }
}
