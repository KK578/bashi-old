using System.Collections.Generic;
using System.Linq;
using Bashi.Core;

namespace Bashi
{
    internal class BashiApp
    {
        private readonly List<IBashiConnectionManager> bashiConnectionManagers;
        private readonly BashiConnectionParamsFactory bashiConnectionParamsFactory;

        public BashiApp(IEnumerable<IBashiConnectionManager> bashiConnectionManagers,
                        BashiConnectionParamsFactory bashiConnectionParamsFactory)
        {
            this.bashiConnectionParamsFactory = bashiConnectionParamsFactory;
            this.bashiConnectionManagers = bashiConnectionManagers.ToList();
        }

        public void Connect()
        {
            bashiConnectionManagers.ForEach(manager =>
                                            {
                                                var details = bashiConnectionParamsFactory.GetParams(manager);
                                                manager.Connect(details);
                                            });
        }
    }
}
