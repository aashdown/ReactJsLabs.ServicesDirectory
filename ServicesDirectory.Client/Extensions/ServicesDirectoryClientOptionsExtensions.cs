using Microsoft.Extensions.Configuration;
using ServicesDirectory.Configuration;

namespace ServicesDirectory.Extensions
{
    public static class ServicesDirectoryClientOptionsExtensions
    {
        public static ServicesDirectoryClientOptions Configure(
            this ServicesDirectoryClientOptions options,
            IConfigurationSection configSection
        )
        {
            options.DirectoryUrl = configSection.GetValue<string>("DirectoryUrl");
            options.ServiceName = configSection.GetValue<string>("ServiceName");
            options.ServiceUrl = configSection.GetValue<string>("ServiceUrl");

            return options;
        }
    }
}
