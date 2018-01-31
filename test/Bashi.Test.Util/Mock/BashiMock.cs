using System.Collections.Generic;
using System.Diagnostics;

namespace Bashi.Test.Util.Mock
{
    public class BashiMock
    {
        private readonly Dictionary<string, int> callCount = new Dictionary<string, int>();

        protected void IncrementCallCount()
        {
            var methodName = new StackFrame(1, true).GetMethod().Name;

            if (callCount.ContainsKey(methodName))
            {
                callCount[methodName]++;
            }
            else
            {
                callCount.Add(methodName, 1);
            }
        }

        public bool WasMethodCalled(string methodName)
        {
            return callCount.ContainsKey(methodName) && callCount[methodName] > 0;
        }
    }
}
