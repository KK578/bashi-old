using System.Collections.Generic;
using System.Linq;
using Bashi.Core.Interface.Config;
using Bashi.Core.Interface.Log;

namespace Bashi.Config.Parser
{
    internal class EnvConfigParser : IEnvConfigParser
    {
        private readonly IBashiLogger log;

        public EnvConfigParser(IBashiLogger log)
        {
            this.log = log;
        }

        public IEnumerable<KeyValuePair<string, string>> Parse(IEnumerable<string> lines)
        {
            var filteredLines = lines.Select(line => line.Trim())
                                     .Where(line => !string.IsNullOrWhiteSpace(line))
                                     .Where(line => !IsComment(line));

            foreach (var line in filteredLines)
            {
                if (line.Contains('='))
                {
                    var index = line.IndexOf('=');
                    var key = line.Substring(0, index).Trim();
                    var value = line.Substring(index + 1).Trim();

                    yield return new KeyValuePair<string, string>(key, value);
                }
                else
                {
                    log.Error($"Unset config line: {line}");
                }
            }
        }

        private static bool IsComment(string line) => line.StartsWith("#");
    }
}
