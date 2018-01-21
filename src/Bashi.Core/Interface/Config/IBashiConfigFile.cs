using System.Collections.Generic;

namespace Bashi.Core.Interface.Config
{
    public interface IBashiConfigFile
    {
        IEnumerable<string> ReadLines();
    }
}
