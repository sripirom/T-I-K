using System;
namespace TIK.Core.Logging
{
    public class NullLogger : ILog
    {
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            return true;
        }
    }
}
