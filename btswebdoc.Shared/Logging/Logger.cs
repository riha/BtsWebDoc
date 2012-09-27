using System;
using System.Reflection;
using log4net;
using log4net.Config;

namespace btswebdoc.Shared.Logging
{
    public static class Log
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static Log()
        {
            XmlConfigurator.Configure();
        }

        public static void Debug(string format, params object[] arguments)
        {
            Debug(string.Format(format, arguments));
        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }

        public static void Info(string format, params object[] arguments)
        {
            Info(string.Format(format, arguments));
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Warn(string format, params object[] arguments)
        {
            Warn(string.Format(format, arguments));
        }

        public static void Warn(string message)
        {
            Logger.Warn(message);
        }

        public static void Error(string message, Exception ex)
        {
            Logger.Error(message, ex);
        }

    }
}
