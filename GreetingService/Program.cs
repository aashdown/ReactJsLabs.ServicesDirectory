using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ServicesDirectory.Extensions;

namespace GreetingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .RegisterWithServicesDirectory()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
