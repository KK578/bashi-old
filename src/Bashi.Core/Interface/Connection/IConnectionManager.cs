using System.Threading.Tasks;

namespace Bashi.Core.Interface.Connection
{
    public interface IConnectionManager
    {
        Task ConnectAsync();
    }
}
