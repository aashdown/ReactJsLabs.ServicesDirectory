using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceDirectory.Client.ServiceDirectoryAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceDirectory.Client
{
    public class ServiceDirectoryProvider : IServiceDirectoryProvider
    {
        private readonly ILogger<ServiceDirectoryProvider> _log;
        private readonly ServiceDirectoryOptions _serviceDirectoryOptions;

        public ServiceDirectoryOptions Options { get => _serviceDirectoryOptions; }
        
        private ServiceDirectoryAPI.Client _serviceDirectoryClient;

        public ServiceDirectoryProvider(
            ILogger<ServiceDirectoryProvider> logger,
            IOptions<ServiceDirectoryOptions> serviceDirectoryOptions
        )
        {
            _log = logger;
            _serviceDirectoryOptions = serviceDirectoryOptions.Value;
        }

        private ServiceDirectoryAPI.Client GetClient()
        {
            if (_serviceDirectoryClient == null)
                InitClient();

            return _serviceDirectoryClient;
        }

        private void InitClient()
        {
            _serviceDirectoryClient = new ServiceDirectoryAPI.Client(_serviceDirectoryOptions.ServiceURL, new HttpClient());
        }

        public async Task RegisterWithServiceDirectory(string serviceName, string serviceBaseUrl)
        {
            _log.LogInformation($"Registering service [{serviceName}] for URL [{serviceBaseUrl}] in Service Directory [{_serviceDirectoryOptions.ServiceURL}]");

            var sdClient = GetClient();
            await sdClient.PostAsync(new ServiceDirectoryAPI.ServiceRegistration()
            {
                ServiceName = serviceName,
                Endpoint = serviceBaseUrl
            });
        }

        public async Task<string> GetServiceEndpoint(string serviceName)
        {
            return (await GetServiceRegistration(serviceName))?.Endpoint;
        }

        public async Task<ServiceRegistration> GetServiceRegistration(string serviceName)
        {
            var sdClient = GetClient();

            return await sdClient.Get2Async(serviceName);
        }
    }
}
