using Bashi.Core;
using Bashi.Slack;

namespace Bashi
{
    internal class BashiApp
    {
        private readonly IBashiConnectionManager bashiConnectionManager;

        public BashiApp(IBashiConnectionManager bashiConnectionManager)
        {
            this.bashiConnectionManager = bashiConnectionManager;
        }

        public void Connect(string token)
        {
            bashiConnectionManager.Connect(token);
        }
    }
}
