using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace reposer.Config
{
    public class ConfigService
    {
        private ConfigService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private IConfiguration configuration;

        private static ConfigService instance = null;
        public static ConfigService Instance
        {
            get
            {
                if (instance == null)
                { throw new InvalidOperationException("Instance not setup yet"); }
                else
                { return instance; }
            }
        }

        public static void Setup(IConfiguration configuration)
        {
            if (instance != null)
            { throw new InvalidOperationException("Instance already setup"); }

            instance = new ConfigService(configuration);
        }

        private string GetFolderFromConfig(string configKey)
        {
            var path = GetStringFromConfig(configKey);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        private TimeSpan GetTimeSpanFromConfig(string configKey)
        {
            var seconds = configuration.GetValue<int>(configKey);
            return TimeSpan.FromSeconds(seconds);
        }

        private string GetStringFromConfig(string configKey)
        {
            return configuration.GetValue<string>(configKey);
        }

        public string WebrootPath => GetFolderFromConfig("webroot:path");

        public string WebsiteRepositoryPath => GetFolderFromConfig("website-repository:path");
        public TimeSpan WebsiteRepositoryPollerInterval => GetTimeSpanFromConfig("website-repository:poller:interval");
        public string WebsiteRepositoryPollerType => GetStringFromConfig("website-repository:poller:type");

        public string RenderPath => GetFolderFromConfig("renderer:path");
        public string RenderType => GetFolderFromConfig("renderer:type");
    }
}
