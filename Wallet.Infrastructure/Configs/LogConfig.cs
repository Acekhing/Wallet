using Microsoft.Extensions.Configuration;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;
using System;
using Serilog.Exceptions;

namespace Wallet.Infrastructure.Configs
{
    public static class LogConfig
    {
        public static void ConfigureLogger()
        {
            // Get runing environment
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Elasticsearch(ConfigureElasticSearch(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
               .CreateLogger();
        }

        public static ElasticsearchSinkOptions ConfigureElasticSearch(
            IConfigurationRoot configuration, 
            string environment) => 
            new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
            NumberOfReplicas = 1,
            NumberOfShards = 2
        };
    }
}
