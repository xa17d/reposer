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

        private string CreateIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public string WebrootPath => CreateIfNotExists(configuration.GetValue<string>("webroot:path"));
        public string WebsiteRepositoryPath => CreateIfNotExists(configuration.GetValue<string>("website-repository"));
    }
}
