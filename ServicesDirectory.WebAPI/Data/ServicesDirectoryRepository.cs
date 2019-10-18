using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServicesDirectory.Configuration;
using ServicesDirectory.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServicesDirectory.Data
{
    public class ServicesDirectoryRepository : IServicesDirectoryRepository
    {
        private ServicesDirectoryOptions _options;
        private DirectoryModel _directoryModel;

        public ServicesDirectoryRepository(IOptions<ServicesDirectoryOptions> options)
        {
            _options = options.Value;

            Setup();
        }

        private void Setup()
        {
            if (_directoryModel == null)
            {
                _directoryModel = LoadFromFile() ?? new DirectoryModel()
                {
                    LastUpdate = DateTime.Now,
                    Services = new List<DirectoryModel.Service>()
                };
            }

            #region internal functions

            DirectoryModel LoadFromFile()
            {
                try
                {
                    if (File.Exists(_options.DirectoryFile))
                    {
                        return JsonConvert.DeserializeObject<DirectoryModel>(_options.DirectoryFile);
                    }

                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            #endregion
        }

        public DirectoryModel Get() => _directoryModel;

        public IEnumerable<DirectoryModel.Service> GetServices() => _directoryModel?.Services;

        public DirectoryModel.Service GetService(string serviceName)
        {
            return _directoryModel?.Services?.FirstOrDefault(s => s.Name == serviceName);
        }

        public void AddOrUpdate(DirectoryModel.Service service)
        {
            SaveAfterUpdate(d =>
            {
                if (d.Services.Any(s => s.Name == service.Name))
                {
                    d.Services.RemoveAll(s => s.Name == service.Name);
                }

                service.LastUpdate = DateTime.Now;
                service.Status = "OK";

                d.Services.Add(service);

                return true;
            });
        }

        public void Delete(string serviceName)
        {
            SaveAfterUpdate(d =>
            {
                d.Services?.RemoveAll(s => s.Name == serviceName);
                return true;
            });
        }

        private void SaveAfterUpdate(Func<DirectoryModel, bool> updateDirectory)
        {
            try
            {
                if (updateDirectory(_directoryModel))
                {
                    _directoryModel.LastUpdate = DateTime.Now;

                    File.WriteAllText(
                        _options.DirectoryFile,
                        JsonConvert.SerializeObject(_directoryModel, Formatting.Indented)
                    );
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
