using System.Collections.Generic;
using Bashi.Core.Interface.Config;

namespace Bashi.Config.File
{
    internal class BashiConfigFile : IBashiConfigFile
    {
        private readonly string filepath;

        public IEnumerable<string> ReadLines() => System.IO.File.ReadLines(filepath);

        public BashiConfigFile(string filepath)
        {
            this.filepath = filepath;
        }
    }
}
