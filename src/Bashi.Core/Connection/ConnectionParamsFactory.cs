using System.Collections.Generic;
using System.Linq;
using Bashi.Core.Interface.Connection;

namespace Bashi.Core.Connection
{
    public class ConnectionParamsFactory : IConnectionParamsFactory
    {
        private readonly List<IConnectionParams> connectionParams;

        public ConnectionParamsFactory(IEnumerable<IConnectionParams> connectionParams)
        {
            this.connectionParams = connectionParams.ToList();
        }

        public IConnectionParams GetParams(IConnectionManager manager)
        {
            if (manager is ISlackConnectionManager)
            {
                return connectionParams.First(p => p is ISlackConnectionParams);
            }

            return null;
        }
    }
}
