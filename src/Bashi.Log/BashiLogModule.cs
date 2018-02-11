using System.Linq;
using Autofac;
using Autofac.Core;
using Bashi.Core.Interface.Log;
using Bashi.Log.Log;
using log4net;

namespace Bashi.Log
{
    public class BashiLogModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry,
                                                              IComponentRegistration registration)
        {
            // Handle constructor parameters.
            registration.Preparing += OnComponentPreparing;
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            var resolvedParameter = new ResolvedParameter((p, i) => p.ParameterType == typeof(IBashiLogger),
                                                          (p, i) => new BashiLogger(LogManager.GetLogger(p.Member.DeclaringType)));

            e.Parameters = e.Parameters.Union(new[] { resolvedParameter });
        }
    }
}
