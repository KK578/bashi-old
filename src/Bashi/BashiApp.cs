using System.Collections.Generic;
using System.Linq;
using Bashi.Core;
using Bashi.Slack;

namespace Bashi
{
    internal class BashiApp
    {
        private readonly List<IBashiConnectionManager> bashiConnectionManagers;

        public BashiApp(IEnumerable<IBashiConnectionManager> bashiConnectionManagers)
        {
            this.bashiConnectionManagers = bashiConnectionManagers.ToList();
        }

        public void Connect(string token)
        {
            bashiConnectionManagers.ForEach(manager => manager.Connect(new SlackConnectionParams(token)));
        }
    }
}
