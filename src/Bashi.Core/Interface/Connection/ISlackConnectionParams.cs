namespace Bashi.Core.Interface.Connection
{
    public interface ISlackConnectionParams : IConnectionParams
    {
        string BotToken { get; }
    }
}
