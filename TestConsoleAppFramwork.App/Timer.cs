using ConsoleAppFramework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsoleAppFramwork.App
{
    class Timer : IDisposable
    {
        Task? _task;

        /// <summary>
        /// Start a timer.
        /// </summary>
        /// <param name="startValue">-v, The start value of the timer.</param>
        /// <param name="token">The token that signals the end of the timer.</param>
        [Command( "" )]
        public Task RunAsync( int startValue = 0, CancellationToken token = default ) => _task ??= Task.Run( () => DoRunAsync( startValue, token ), token );

        static async Task DoRunAsync( int value, CancellationToken token )
        {
            while ( !token.IsCancellationRequested )
            {
                Console.WriteLine( "Timer at {0}", value++ );
                await Task.Delay( TimeSpan.FromSeconds( 1 ), token );
            }
            Console.WriteLine( "Timer end of run...");
        }

        public void Dispose()
        {
            Console.WriteLine( "Dispose timer..." );
            _task?.Dispose();
        }
    }
}
