using Netcorext.Worker.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWorkerRunner<TWorker, TRunner>(this IServiceCollection services) where TWorker : BackgroundWorker
                                                                                                         where TRunner : class, IWorkerRunner<TWorker>
    {
        services.AddHostedService<TWorker>();

        services.AddTransient<IWorkerRunner<TWorker>, TRunner>();
        
        return services;
    }
}