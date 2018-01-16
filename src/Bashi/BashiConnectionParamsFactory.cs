using Bashi.Core;
using Bashi.Slack;

namespace Bashi
{
    public class BashiConnectionParamsFactory
    {
        private readonly SlackConnectionParams slackConnectionParams;

        public BashiConnectionParamsFactory(SlackConnectionParams slackConnectionParams)
        {
            this.slackConnectionParams = slackConnectionParams;
        }

        public IConnectionParams GetParams(IBashiConnectionManager manager)
        {
            if (manager is BashiSlackConnectionManager)
            {
                return slackConnectionParams;
            }

            return null;
        }
    }
}
