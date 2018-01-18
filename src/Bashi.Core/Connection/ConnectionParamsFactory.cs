using Bashi.Interface.Connection;
using Bashi.Slack.Connection;

namespace Bashi.Core.Connection
{
    public class ConnectionParamsFactory
    {
        private readonly SlackConnectionParams slackConnectionParams;

        public ConnectionParamsFactory(SlackConnectionParams slackConnectionParams)
        {
            this.slackConnectionParams = slackConnectionParams;
        }

        public IConnectionParams GetParams(IConnectionManager manager)
        {
            if (manager is SlackConnectionManager)
            {
                return slackConnectionParams;
            }

            return null;
        }
    }
}
