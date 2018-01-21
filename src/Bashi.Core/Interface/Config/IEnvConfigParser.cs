using System.Collections.Generic;

namespace Bashi.Core.Interface.Config
{
    public interface IEnvConfigParser
    {
        IEnumerable<KeyValuePair<string, string>> Parse(IEnumerable<string> lines);
    }
}
