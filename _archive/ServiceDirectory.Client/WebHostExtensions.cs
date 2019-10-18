using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ServiceDirectory.Client
{
    public static class WebHostExtensions
    {
        public static IWebHost RegisterWithServiceDirectory(this IWebHost webHost, string asServiceName)
        {
            var log = webHost.Services.GetService<ILogger<IServiceDirectoryProvider>>();
            var sdClient = webHost.Services.GetService<IServiceDirectoryProvider>();
            var serverAddresses = webHost.ServerFeatures.Get<IServerAddressesFeature>();

            if(sdClient != null)
            {
                var address = serverAddresses.Addresses?.First();

                log.LogInformation($"ServiceDirectory.Client: Registering service [{asServiceName} for address [{address}] in Service Directory [{sdClient.Options.ServiceURL}]");

                sdClient.RegisterWithServiceDirectory(
                    asServiceName,
                    serverAddresses.Addresses?.First()
                );
            }

            return webHost;
        }
    }
}
