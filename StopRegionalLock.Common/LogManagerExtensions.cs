using System;
using StopRegionalLock.Common.Logger;

namespace StopRegionalLock.Common
{
    public static class LogManagerExtensions
    {
        private static Logger.Logger GetLogger(Type logType)
        {
            return new Logger.Logger(logType);
        }

        public static void Trace(this Type logType, string message, params object[] args)
        {
            GetLogger(logType).Trace(message, args);
        }

        public static void Trace(this Type logType, Exception ex, string message = null)
        {
            GetLogger(logType).Trace(ex, message);
        }

        public static void Debug(this Type logType, string message, params object[] args)
        {
            GetLogger(logType).Debug(message, args);
        }

        public static void Debug(this Type logType, Exception ex, string message = null)
        {
            GetLogger(logType).Debug(ex, message);
        }

        public static void Info(this Type logType, string message, params object[] args)
        {
            GetLogger(logType).Info(message, args);
        }

        public static void Info(this Type logType, Exception ex, string message = null)
        {
            GetLogger(logType).Info(ex, message);
        }

        public static void Warn(this Type logType, string message, params object[] args)
        {
            GetLogger(logType).Warn(message, args);
        }

        public static void Warn(this Type logType, Exception ex, string message = null)
        {
            GetLogger(logType).Warn(ex, message);
        }

        public static void Error(this Type logType, string message, params object[] args)
        {
            GetLogger(logType).Error(message, args);
        }

        public static void Error(this Type logType, Exception ex, string message = null)
        {
            GetLogger(logType).Error(ex, message);
        }

        public static void Fatal(this Type logType, string message, params object[] args)
        {
            GetLogger(logType).Fatal(message, args);
        }

        public static void Fatal(this Type logType, Exception ex, string message = null)
        {
            GetLogger(logType).Fatal(ex, message);
        }

        public static void Log(this Type logType, Logger.LogLevel level, string message, params object[] args)
        {
            GetLogger(logType).Log(level, message, args);
        }

        public static void Log(this Type logType, Logger.LogLevel level, Exception ex, string message = null)
        {
            GetLogger(logType).Log(level, message, ex);
        }
    }
}
