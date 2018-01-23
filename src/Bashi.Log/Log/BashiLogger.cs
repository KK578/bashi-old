using Bashi.Core.Interface.Log;
using log4net;

namespace Bashi.Log.Log
{
    internal class BashiLogger : IBashiLogger
    {
        private readonly ILog logger;

        public BashiLogger(ILog logger)
        {
            this.logger = logger;
        }

        public void Info(string message) => logger.Info(message);
        public void Warn(string message) => logger.Warn(message);
        public void Error(string message) => logger.Error(message);
        public void Fatal(string message) => logger.Fatal(message);
        public void Debug(string message) => logger.Debug(message);
    }
}
