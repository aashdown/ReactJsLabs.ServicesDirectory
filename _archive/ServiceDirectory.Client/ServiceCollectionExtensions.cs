using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceDirectory.Client
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceDirectoryClient(this IServiceCollection services, Action<ServiceDirectoryOptions> configureOptions)
        {
            services.Configure<ServiceDirectoryOptions>(configureOptions);
            services.AddSingleton<IServiceDirectoryProvider, ServiceDirectoryProvider>();
        }
    }
}
