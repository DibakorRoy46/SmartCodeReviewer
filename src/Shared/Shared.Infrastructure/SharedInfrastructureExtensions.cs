using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Application.Abstractions;
using Shared.Infrastructure.Caching;
using Shared.Infrastructure.Clock;
using Shared.Infrastructure.EventBus;
using Shared.Infrastructure.FeatureFlags;
using Shared.Infrastructure.FileStorage;
using Shared.Infrastructure.IdGeneration;
using Shared.Infrastructure.Logging;

namespace Shared.Infrastructure;

public static class SharedInfrastructureExtensions
{
    public static IHostBuilder UseSharedLogging(this IHostBuilder builder)
    {
        builder.UseSerilog(LoggingConfiguration.ConfigureLogger);
        return builder;
    }

    public static IServiceCollection AddNotificationInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        //Add Services Here
        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<IIdGenerator, GuidIdGenerator>();
        services.AddSingleton<IFileStorage>(provider =>
            new LocalFileStorage(configuration["FileStorage:BasePath"] ?? "Uploads"));
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        services.AddSingleton<ICache, InMemoryCache>();
        services.AddSingleton<IFeatureManager, FeatureManager>();

        //Logger Adapter
        services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

        return services;
    }

}
