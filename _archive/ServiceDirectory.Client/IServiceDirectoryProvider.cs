using ServiceDirectory.Client.ServiceDirectoryAPI;
using System.Threading.Tasks;

namespace ServiceDirectory.Client
{
    public interface IServiceDirectoryProvider
    {
        ServiceDirectoryOptions Options { get;  }

        Task<ServiceRegistration> GetServiceRegistration(string serviceName);
        Task<string> GetServiceEndpoint(string serviceName);
        Task RegisterWithServiceDirectory(string serviceName, string serviceBaseUrl);
    }
}