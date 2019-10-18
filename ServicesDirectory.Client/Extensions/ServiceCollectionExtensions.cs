using Microsoft.Extensions.DependencyInjection;
using ServicesDirectory.Client;
using ServicesDirectory.Configuration;
using System;

namespace ServicesDirectory.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesDirectoryClient(this IServiceCollection services, Action<ServicesDirectoryClientOptions> configureOptions)
        {
            services.Configure(configureOptions);
            services.AddSingleton<IServicesDirectoryClient, ServicesDirectoryClient>();

            return services;
        }
    }
}
