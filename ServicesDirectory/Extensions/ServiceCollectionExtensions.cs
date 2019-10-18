using Microsoft.Extensions.DependencyInjection;
using ServicesDirectory.Configuration;
using ServicesDirectory.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesDirectory.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesDirectory(this IServiceCollection services, Action<ServicesDirectoryOptions> configureOptions)
        {
            services.Configure<ServicesDirectoryOptions>(configureOptions);
            services.AddSingleton<IServicesDirectoryRepository, ServicesDirectoryRepository>();
            return services;
        }
    }
}
