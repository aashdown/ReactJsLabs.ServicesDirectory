using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServicesDirectory.Configuration;
using ServicesDirectory.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ServicesDirectory.Client
{
    public class ServicesDirectoryClient : IServicesDirectoryClient
    {
        private ServicesDirectoryClientOptions _options;
        public ServicesDirectoryClientOptions Options => _options;

        public ServicesDirectoryClient(IOptions<ServicesDirectoryClientOptions> options)
        {
            _options = options.Value;
        }

        private HttpClient GetHttpClient()
        {
            return new HttpClient()
            {
                BaseAddress = new Uri(_options.DirectoryUrl),
            };
        }


        public async Task<DirectoryModel> Get()
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync("api/services");

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DirectoryModel>(responseContent);
            }
        }

        public async Task<IEnumerable<DirectoryModel.Service>> GetServices()
        {
            return (await Get())?.Services;
        }

        public async Task<DirectoryModel.Service> GetService(string serviceName)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync($"api/services/{serviceName}");

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DirectoryModel.Service>(responseContent);
            }
        }

        public async Task Register()
        {
            await Register(o => { });
        }

        public async Task Register(DirectoryModel.Service service)
        {
            using (var client = GetHttpClient())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(service)
                );

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await client.PostAsync("api/services", content);
            }
        }

        public async Task Register(Action<ServicesDirectoryClientOptions> configureOptions)
        {
            using (var client = GetHttpClient()) 
            { 

                configureOptions(_options);

                var service = new DirectoryModel.Service()
                {
                    ServiceName = _options.ServiceName,
                    Endpoint = _options.ServiceUrl,
                    LastUpdate = DateTime.Now,
                    Status = "OK"
                };
                
                var content = new StringContent(
                    JsonConvert.SerializeObject(service)
                );

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await client.PostAsync("api/services", content);
            }
        }

        public async Task Delete(string serviceName)
        {
            using (var client = GetHttpClient())
            {
                await client.DeleteAsync($"api/services/{serviceName}");
            }
        }
    }
}
