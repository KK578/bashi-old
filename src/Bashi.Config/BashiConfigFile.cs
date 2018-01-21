using System.Collections.Generic;
using System.IO;
using Bashi.Core.Interface.Config;

namespace Bashi.Config
{
    public class BashiConfigFile : IBashiConfigFile
    {
        private readonly string filepath;

        public IEnumerable<string> ReadLines() => File.ReadLines(filepath);

        public BashiConfigFile(string filepath)
        {
            this.filepath = filepath;
        }
    }
}
