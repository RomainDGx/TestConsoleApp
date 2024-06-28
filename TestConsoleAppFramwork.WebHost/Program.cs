using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace TestConsoleAppFramwork.WebHost
{
    class Program
    {
        static async Task Main( string[] args )
        {
            var builder = Host.CreateDefaultBuilder();
            builder.UseCKMonitoring();
            using var host = builder.Build();
            ConsoleApp.ServiceProvider = host.Services;

            var app = ConsoleApp.Create();
            app.Add<TimerCommand>();
            await app.RunAsync( args );
        }
    }
}
