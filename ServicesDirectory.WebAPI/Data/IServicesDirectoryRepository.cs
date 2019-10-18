using System.Collections.Generic;
using ServicesDirectory.Models;

namespace ServicesDirectory.Data
{
    public interface IServicesDirectoryRepository
    {
        DirectoryModel Get();
        IEnumerable<DirectoryModel.Service> GetServices();
        DirectoryModel.Service GetService(string serviceName);

        void AddOrUpdate(DirectoryModel.Service service);
        void Delete(string serviceName);
    }
}