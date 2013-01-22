using NLog.Web;
using System;
using System.IO;
using System.Xml.Linq;

namespace StopRegionalLock.Common.Logger
{
    internal sealed class Logger
    {
        static Logger()
        {
            NLog.LayoutRenderers.AspNetRequestValueLayoutRenderer r = new NLog.LayoutRenderers.AspNetRequestValueLayoutRenderer();

            string setupLocation = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            XDocument xml = XDocument.Load(setupLocation);
            XElement xnode = xml.Root.Element("appSettings");
            string appSettingsFile = (xnode.Attribute("file") != null) ? xnode.Attribute("file").Value : null;

            if (!string.IsNullOrEmpty(appSettingsFile))
            {
                setupLocation = Path.Combine(Path.GetDirectoryName(setupLocation), appSettingsFile);
            }

            string path = Path.Combine(Path.GetDirectoryName(setupLocation), "nlog.config");
            if (!string.IsNullOrEmpty(path))
            {
                if (!path.Contains(":"))
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
                }
                if (File.Exists(path))
                {
                    NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(path, true);
                }
            }
        }

        //Copy NLog.Extended to target directory
        private static NLogHttpModule _module = new NLogHttpModule();

        private NLog.Logger _logger;

        private Logger(string name)
        {
            _logger = NLog.LogManager.GetLogger(name);
        }

        public Logger(Type type) : this(type.FullName)
        {
        }

        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public void Trace(Exception ex, string message)
        {
            _logger.TraceException(message, ex);
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Debug(Exception ex, string message)
        {
            _logger.DebugException(message, ex);
        }


        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Info(Exception ex, string message)
        {
            _logger.InfoException(message, ex);
        }

        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public void Warn(Exception ex, string message)
        {
            _logger.WarnException(message, ex);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error(Exception ex, string message)
        {
            _logger.ErrorException(message, ex);
        }

        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public void Fatal(Exception ex, string message)
        {
            _logger.FatalException(message, ex);
        }
        
        public void Log(LogLevel level, string message, params object[] args)
        {
            _logger.Log(NLog.LogLevel.FromString(level.ToString()), message, args);
        }

        public void Log(LogLevel level, Exception ex, string message)
        {
            _logger.Log(NLog.LogLevel.FromString(level.ToString()), message, ex);
        }
    }
}
