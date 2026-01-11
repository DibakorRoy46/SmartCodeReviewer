using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;

namespace Shared.Infrastructure;

public static class LoggingConfiguration
{
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
    (context, loggerConfiguration) =>
    {
        var env = context.HostingEnvironment;
        loggerConfiguration.MinimumLevel.Information()
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("ApplicationName", env.ApplicationName)
                        .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                        .Enrich.WithExceptionDetails()
                        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
                        .WriteTo.Console(new RenderedCompactJsonFormatter())
                        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                        .WriteTo.File("Logs/info-.txt",
                            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                        .WriteTo.File("Logs/error-.txt",
                            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                        .WriteTo.File(new RenderedCompactJsonFormatter(),
                                  "Logs/log-.json",
                                  rollingInterval: RollingInterval.Day);


        if (env.IsDevelopment())
        {
            var serviceNamespaces = new[] { "Catalog", "Basket", "Discount", "Ordering", "Payment", "Identity" };
            foreach (var ns in serviceNamespaces)
                loggerConfiguration.MinimumLevel.Override(ns, LogEventLevel.Debug);
        }


        var elasticUrl = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
        if (!string.IsNullOrEmpty(elasticUrl))
        {
            loggerConfiguration.WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(elasticUrl))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                    IndexFormat = $"ecommerce-Logs-{DateTime.UtcNow:yyyy.MM.dd}",
                    MinimumLogEventLevel = LogEventLevel.Debug
                });
        }
    };
}
