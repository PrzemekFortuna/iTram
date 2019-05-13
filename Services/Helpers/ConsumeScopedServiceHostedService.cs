using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Services.Helpers
{
    public class ConsumeScopedServiceHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public ConsumeScopedServiceHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var scope = _serviceProvider.CreateScope();
            var beaconTokenService = scope.ServiceProvider.GetRequiredService<BeaconTokenService>();
            _timer = new Timer(beaconTokenService.RenewTokens, null, TimeSpan.Zero, TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}