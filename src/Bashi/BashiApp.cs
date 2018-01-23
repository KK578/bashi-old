using System.Collections.Generic;
using System.Linq;
using Bashi.Core.Interface.Connection;
using Bashi.Core.Interface.Log;

namespace Bashi
{
    internal class BashiApp
    {
        private readonly List<IConnectionManager> connectionManagers;

        public BashiApp(IEnumerable<IConnectionManager> connectionManagers,
                        IEnumerable<IEventLogger> eventLoggers)
        {
            this.connectionManagers = connectionManagers.ToList();

            // TODO: Move this somewhere else...
            eventLoggers.ToList().ForEach(l => l.AttachLogger());
        }

        public void Connect()
        {
            connectionManagers.ForEach(manager =>
                                            {
                                                manager.Connect();
                                            });
        }
    }
}
