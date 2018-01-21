namespace Bashi.Core.Interface.Log
{
    public interface IBashiLogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
        void Debug(string message);
    }
}
