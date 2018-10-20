using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using reposer.Config;
using reposer.Repository;

namespace reposer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddCommandLine(args)
                .AddJsonFile("config.json", optional: false, reloadOnChange: false)
                .Build();

            ConfigService.Setup(config);
            var configService = ConfigService.Instance;

            return new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseWebRoot(configService.WebrootPath)
                .UseStartup<Startup>();
        }
    }
}
