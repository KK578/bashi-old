using System.Collections.Generic;
using System.Linq;
using Bashi.Core;

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
            bashiConnectionManagers.ForEach(manager => manager.Connect(token));
        }
    }
}
