using System.Collections.Generic;
using Bashi.Core.Interface.Config;

namespace Bashi.Config
{
    public class EnvConfigParser : IEnvConfigParser
    {
        public IEnumerable<KeyValuePair<string, string>> Parse(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var index = line.IndexOf('=');

                if (index >= 0 || index == line.Length - 1)
                {
                    var key = line.Substring(0, index);
                    var value = line.Substring(index + 1);

                    yield return new KeyValuePair<string, string>(key, value);
                }
            }
        }
    }
}
