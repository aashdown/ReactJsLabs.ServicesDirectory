using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServicesDirectory.Configuration;
using ServicesDirectory.Data;
using ServicesDirectory.Models;
using System;

namespace ServicesDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ServicesDirectoryOptions _options;
        private readonly IServicesDirectoryRepository _repository;

        public ServicesController(
            IOptions<ServicesDirectoryOptions> options,
            IServicesDirectoryRepository repository
        )
        {
            _options = options.Value;
            _repository = repository;
        }

        [HttpGet]
        public DirectoryModel Get()
        {
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            return _repository.Get(); ;
        }

        [HttpGet]
        [Route("{serviceName}")]
        public DirectoryModel.Service Get(string serviceName)
        {
            try
            {
                Response.Headers["Access-Control-Allow-Origin"] = "*";
                return _repository.GetService(serviceName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public void Post(DirectoryModel.Service service)
        {
            _repository.AddOrUpdate(service);
        }

        [HttpDelete]
        [Route("{serviceName}")]
        public void Delete(string serviceName)
        {
            _repository.Delete(serviceName);
        }
    }
}