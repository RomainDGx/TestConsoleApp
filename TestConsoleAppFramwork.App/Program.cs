using ConsoleAppFramework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsoleAppFramwork.App
{
    class Program
    {
        static async Task Main( string[] args )
        {
            await ConsoleApp.RunAsync( args, RunTimerAsync );

            //var app = ConsoleApp.Create();
            //app.Add<Timer>();
            //await app.RunAsync( args );
        }

        /// <summary>
        /// Start a timer.
        /// </summary>
        /// <param name="startValue">-v, The start value of the timer.</param>
        /// <param name="token">The token that signals the end of the timer.</param>
        static async Task RunTimerAsync( int startValue, CancellationToken token )
        {
            while( !token.IsCancellationRequested )
            {
                Console.WriteLine( "Timer value at {0}.", startValue++ );
                await Task.Delay( TimeSpan.FromSeconds( 1 ), token );
            }
            Console.WriteLine( "Timer end..." );
        }
    }
}
