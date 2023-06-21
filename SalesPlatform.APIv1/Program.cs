using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SalesPlatform.APIv1
{
    /// <summary>
    /// Program clas.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main fucntion.
        /// </summary>
        /// <param name="args">Array arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// WebHost.
        /// </summary>
        /// <param name="args">Array arguments.</param>
        /// <returns>Host builder create.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.ConfigureAppConfiguration(ConfigConfiguration);
                        webBuilder.UseStartup<Startup>();
                    });
        }

        /// <summary>
        /// ConfigConfiguration.
        /// </summary>
        /// <param name="arg1">WebHostBuilderContext object.</param>
        /// <param name="arg2">IConfigurationBuilder object.</param>
        private static void ConfigConfiguration(WebHostBuilderContext arg1, IConfigurationBuilder arg2)
        {
            arg2.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{arg1.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        }
    }
}
