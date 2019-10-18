using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ServicesDirectory.Client;
using System;

namespace ServicesDirectory.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost RegisterWithServicesDirectory(this IWebHost webHost)
        {
            try
            {
                var sdClient = webHost.Services.GetService<IServicesDirectoryClient>();
                sdClient.Register();
            }
            catch (Exception)
            {
            }

            return webHost;
        }

        private class UnableToRegisterWithServiceDirectoryException : Exception
        {
            public UnableToRegisterWithServiceDirectoryException(string message, Exception innerException): base(message, innerException) { }
        }
    }
}
