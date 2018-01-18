using System.Collections.Generic;
using System.Linq;
using Bashi.Core.Connection;
using Bashi.Interface.Connection;

namespace Bashi
{
    internal class BashiApp
    {
        private readonly List<IConnectionManager> connectionManagers;
        private readonly ConnectionParamsFactory connectionParamsFactory;

        public BashiApp(IEnumerable<IConnectionManager> connectionManagers,
                        ConnectionParamsFactory connectionParamsFactory)
        {
            this.connectionParamsFactory = connectionParamsFactory;
            this.connectionManagers = connectionManagers.ToList();
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
