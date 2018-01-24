using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace SlackApi.Core.Interface.Rtm
{
    public interface IWebSocketManager
    {
        WebSocketState State { get; }

        Task<string> ReceiveData();
        Task SendData(string data);
        Task ConnectAsync(Uri uri);
    }
}
