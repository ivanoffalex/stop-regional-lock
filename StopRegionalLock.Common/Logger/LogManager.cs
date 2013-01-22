using System;

namespace StopRegionalLock.Common.Logger
{
    public static class LogManager<T>
    {
        private static Logger _logger = new Logger(typeof(T));

        public static void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public static void Trace(Exception ex, string message = null)
        {
            _logger.Trace(ex, message);
        }

        public static void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public static void Debug(Exception ex, string message = null)
        {
            _logger.Debug(ex, message);
        }

        public static void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public static void Info(Exception ex, string message = null)
        {
            _logger.Info(ex, message);
        }

        public static void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public static void Warn(Exception ex, string message = null)
        {
            _logger.Warn(ex, message);
        }

        public static void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public static void Error(Exception ex, string message = null)
        {
            _logger.Error(ex, message);
        }

        public static void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public static void Fatal(Exception ex, string message = null)
        {
            _logger.Fatal(ex, message);
        }

        public static void Log(LogLevel level, string message, params object[] args)
        {
            _logger.Log(level, message, args);
        }

        public static void Log(LogLevel level, Exception ex, string message = null)
        {
            _logger.Log(level, message, ex);
        }

    }
}
