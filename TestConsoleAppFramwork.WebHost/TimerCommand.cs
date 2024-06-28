using CK.Core;
using ConsoleAppFramework;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsoleAppFramwork.WebHost
{
    class TimerCommand : IDisposable
    {
        Task? _task;
        readonly IConfiguration _configuration;

        public TimerCommand( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Start a timer.
        /// </summary>
        /// <param name="startValue">-v, The start value of the timer.</param>
        /// <param name="token">The token that signals the end of the timer.</param>
        [Command( "" )]
        public Task RunAsync( [FromServices] IActivityMonitor monitor, int startValue = 0, CancellationToken token = default )
            => _task ??= Task.Run( () => DoRunAsync( monitor, startValue, token ), token );

        async Task DoRunAsync( IActivityMonitor monitor, int value, CancellationToken token )
        {
            using var _ = monitor.OpenInfo( "Start timer..." );

            if( !int.TryParse( _configuration["Wait"], out int wait ) )
            {
                throw new InvalidOperationException( "Missing 'Wait' configuration." );
            }
            monitor.Info( $"Wait set at {wait}s." );

            while ( !token.IsCancellationRequested )
            {
                try
                {
                    monitor.Info( $"Timer at {value++}" );
                    await Task.Delay( TimeSpan.FromSeconds( wait ), token );
                }
                catch( OperationCanceledException ex ) when (ex.CancellationToken == token )
                {
                    Throw.DebugAssert( token.IsCancellationRequested );
                    monitor.Info( "Timer end of run..." );
                }
                catch( Exception ex )
                {
                    monitor.Error( ex );
                }
            }
        }

        public void Dispose()
        {
            ActivityMonitor.StaticLogger.Info( "Dispose timer..." );
            _task?.Dispose();
        }
    }
}
