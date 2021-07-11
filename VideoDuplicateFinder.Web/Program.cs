using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.CommandLine; 

namespace DuplicateFinderWeb
{
    public class Program
    {
		/// <param name="logSink">The URL of the seq instance to use as a log sink</param>
		/// <param name="hostBuilderArgs">Args to pass onto the host builder</param>
        public static void Main(string logSink, string[] hostBuilderArgs)
        {
			Console.Error.WriteLine($"Logging to {logSink}");
			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.WriteTo.Seq(logSink)
				.WriteTo.Console()
				.CreateLogger();

            CreateHostBuilder(hostBuilderArgs).Build().Run();
        }

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging(
					logging => {
						logging.AddSerilog();
				})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
