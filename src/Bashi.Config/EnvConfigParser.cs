using System.Collections.Generic;
using Bashi.Core.Interface.Config;

namespace Bashi.Config
{
    internal class EnvConfigParser : IEnvConfigParser
    {
        public IEnumerable<KeyValuePair<string, string>> Parse(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var trimmed = line.Trim();

                if (IsComment(trimmed))
                {
                    continue;
                }

                var index = trimmed.IndexOf('=');

                if (index >= 0 || index == trimmed.Length - 1)
                {
                    var key = trimmed.Substring(0, index).Trim();
                    var value = trimmed.Substring(index + 1).Trim();

                    yield return new KeyValuePair<string, string>(key, value);
                }
            }
        }

        private static bool IsComment(string line) => line.StartsWith("#");
    }
}
