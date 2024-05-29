using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json")
                           .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
               .UseMetricsWebTracking()
               .UseMetrics(options =>
               {
                   options.EndpointOptions = endPointsOptions =>
                   {
                       endPointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                       endPointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                       endPointsOptions.EnvironmentInfoEndpointEnabled = false;
                   };
               })
               .UseSerilog()
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
        }
    }
}