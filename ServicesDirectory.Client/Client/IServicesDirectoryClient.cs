using ServicesDirectory.Configuration;
using ServicesDirectory.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesDirectory.Client
{
    public interface IServicesDirectoryClient
    {
        ServicesDirectoryClientOptions Options { get; }
        Task Delete(string serviceName);
        Task<DirectoryModel> Get();
        Task<DirectoryModel.Service> GetService(string serviceName);
        Task<IEnumerable<DirectoryModel.Service>> GetServices();
        Task Register();
        Task Register(DirectoryModel.Service service);
        Task Register(Action<ServicesDirectoryClientOptions> configureOptions);
    }
}