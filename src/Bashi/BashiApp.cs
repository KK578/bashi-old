using Bashi.Slack;

namespace Bashi
{
    internal class BashiApp
    {
        private readonly BashiSlackConnectionManager bashiSlackConnectionManager;

        public BashiApp(BashiSlackConnectionManager bashiSlackConnectionManager)
        {
            this.bashiSlackConnectionManager = bashiSlackConnectionManager;
        }

        public void Connect(string token)
        {
            bashiSlackConnectionManager.Connect(token);
        }
    }
}
