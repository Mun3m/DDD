using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Persistence.Subscriptions.Manager;

namespace VistaClaim.Persistence.EventStore
{
    public class HostedService : IHostedService
    {
        private readonly IEventStoreConnection _connection;
        private readonly ProjectionManager _projectionManager;

        public HostedService(IEventStoreConnection connection, ProjectionManager projectionManager)
        {
            _connection = connection;
            _projectionManager = projectionManager;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _connection.ConnectAsync();
            await _projectionManager.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _projectionManager.Stop();
            _connection.Close();

            return Task.CompletedTask;
        }
    }
}
