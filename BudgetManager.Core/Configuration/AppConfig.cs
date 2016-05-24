using System;
using System.IO;

namespace BudgetManager.Core.Configuration
{
    public class AppConfig : IConfig
    {
        private static readonly object LockField = new object();
        private static volatile AppConfig appConfig;

        private IConfig _config;

        private AppConfig()
        {
            _config = new XMLConfig();

            string location = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\BudgetManager";
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }

            AppDomain.CurrentDomain.SetData("DataDirectory", location);
        }

        public static AppConfig Config
        {
            get
            {
                if (appConfig == null)
                {
                    lock (LockField)
                    {
                        if (appConfig == null)
                        {
                            appConfig = new AppConfig();
                        }
                    }
                }

                return appConfig;
            }
        }

        public string ConnectionString
        {
            get { return _config.ConnectionString; }
        }

        public void Register(IConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            lock (LockField)
            {
                _config = config;
            }
        }
    }
}