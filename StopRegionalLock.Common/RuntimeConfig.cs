using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace StopRegionalLock.Common
{
    public static class RuntimeConfig
    {
        private const string CONFIG_FILENAME = "runtime.config";
        private const bool CONFIG_FILEWATCH = true;

        private static IDictionary<string, object> _container = null;
        private static string _configFilepath = null;
        private static object _lock = new object();

        private static FileSystemWatcher Watcher = new FileSystemWatcher(AppDomain.CurrentDomain.BaseDirectory);

        static RuntimeConfig()
        {
            Watcher.Changed += Watcher_Changed;
            Watcher.NotifyFilter = NotifyFilters.LastWrite;

            Initialize();
        }

        private static void Initialize()
        {
            typeof(RuntimeConfig).Info("Initialize");
            lock (_lock)
            {
                string filename = ConfigurationManager.AppSettings["runtimeConfigFilename"] ?? CONFIG_FILENAME;
                bool fileWatch;
                if (!bool.TryParse(ConfigurationManager.AppSettings["runtimeConfigFileWatch"], out fileWatch))
                {
                    fileWatch = CONFIG_FILEWATCH;
                }

                _configFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

                if (!File.Exists(_configFilepath))
                {
                    _container = new Dictionary<string, object>();
                }
                else
                {
                    typeof(RuntimeConfig).Info("Loading from file {0}", _configFilepath);

                    try
                    {
                        using (StreamReader re = new StreamReader(_configFilepath))
                        {
                            using (JsonTextReader reader = new JsonTextReader(re))
                            {
                                JsonSerializer se = new JsonSerializer();
                                _container = (IDictionary<string, object>)se.Deserialize(reader, typeof(IDictionary<string, object>));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        typeof(RuntimeConfig).Info(ex, "Unable to load");
                    }
                    typeof(RuntimeConfig).Info("Load was done successfully");
                }

                Watcher.Filter = filename;
                Watcher.EnableRaisingEvents = fileWatch;
            }
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            typeof(RuntimeConfig).Info("Configuration file was changed. Reinitialize");

            bool watcherStatus = Watcher.EnableRaisingEvents;
            Watcher.EnableRaisingEvents = false;

            try
            {
                Initialize();
            }
            finally
            {
                Watcher.EnableRaisingEvents = watcherStatus;
            }
        }


        public static T GetSection<T>(string key, Func<T> createFunc)
        {
            object section;
            if (!_container.TryGetValue(key, out section))
            {
                section = createFunc();
                _container[key] = section;
            }
            JObject obj = section as JObject;
            if (obj != null)
            {
                using (JsonReader jr = new JTokenReader(obj))
                {
                    section = new JsonSerializer().Deserialize<T>(jr);
                }
                _container[key] = section;
            }
            return (T)section;
        }


        public static void Save()
        {
            typeof(RuntimeConfig).Info("Saving to file {0}", _configFilepath);
            lock (_lock)
            {
                bool watcherStatus = Watcher.EnableRaisingEvents;
                Watcher.EnableRaisingEvents = false;
                try
                {
                    if (File.Exists(_configFilepath))
                    {
                        File.Delete(_configFilepath);
                    }

                    using (FileStream fs = File.Open(_configFilepath, FileMode.CreateNew))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            using (JsonWriter jw = new JsonTextWriter(sw))
                            {
                                jw.Formatting = Formatting.Indented;

                                JsonSerializer serializer = new JsonSerializer() 
                                { 
                                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                                    DateParseHandling = DateParseHandling.DateTime,
                                    DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
                                };
                                serializer.Serialize(jw, _container);
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    typeof(RuntimeConfig).Info(ex, "Unable to save");
                }
                finally
                {
                    Watcher.EnableRaisingEvents = watcherStatus;
                }
            }
            typeof(RuntimeConfig).Info("Save was done successfully");
        }
    }
}
