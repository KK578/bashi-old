using System.Collections.Generic;
using System.Linq;
using Bashi.Core.Interface.Connection;
using Bashi.Core.Interface.Logger;

namespace Bashi
{
    internal class BashiApp
    {
        private readonly List<IConnectionManager> connectionManagers;
        private readonly IConnectionParamsFactory connectionParamsFactory;

        public BashiApp(IEnumerable<IConnectionManager> connectionManagers,
                        IConnectionParamsFactory connectionParamsFactory,
                        IEnumerable<IEventLogger> eventLoggers)
        {
            this.connectionParamsFactory = connectionParamsFactory;
            this.connectionManagers = connectionManagers.ToList();

            eventLoggers.ToList().ForEach(l => l.AttachLogger());
        }

        public void Connect()
        {
            connectionManagers.ForEach(manager =>
                                            {
                                                var details = connectionParamsFactory.GetParams(manager);
                                                manager.Connect(details);
                                            });
        }
    }
}
