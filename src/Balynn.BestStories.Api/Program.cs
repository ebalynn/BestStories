using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Balynn.BestStories.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
