namespace Bashi.Core.Interface.Connection
{
    public interface IConnectionParamsFactory
    {
        IConnectionParams GetParams(IConnectionManager manager);
    }
}
