using EventStore.ClientAPI;
using System;
using VistaClaim.Persistence.EventStore;
using VistaClaim.Persistence.Subscriptions;
using VistaClaim.Persistence.Subscriptions.Manager;
using Xunit;

namespace VistaClaim.Application.Tests.ConfigureServices
{
    [CollectionDefinition("ESServiceCollection")]
    public class ESServiceCollectionFixture : ICollectionFixture<ESServiceCollectionFixture>, IDisposable
    {
        private readonly IEventStoreConnection _esConn;
        private readonly ProjectionManager _manager;

        public ESServiceCollectionFixture()
        {
            //DbContext = new ESApplicationContextFixture().Instance;
            //DbContext.Dashboards.Add(new Interfaces.ReadModels.Dashboard { Id = new Guid(DashboardProjection.DashboardId) });
            //DbContext.SaveChanges();

            //_esConn = EventStoreConnection.Create("ConnectTo=tcp://admin:changeit@127.0.0.1:1113;DefaultUserCredentials=admin:changeit;", ConnectionSettings.Create().KeepReconnecting());
            //_esConn.ConnectAsync().Wait();

            //var checkpointStore = new EFCheckpointStore(DbContext, "readModels");

            //_manager = new ProjectionManager(_esConn, checkpointStore,
            //                                        new ClientProjection(DbContext),
            //                                        new AssignmentProjection(DbContext,
            //                                        async clientId => (await DbContext.Clients.FindAsync(clientId)).Name),
            //                                        new DashboardProjection(DbContext)
            //                                   );

            //_manager.Start(true).Wait();
        }
        public ESApplicationContext DbContext { get; private set; }

        public void Dispose()
        {
            _manager.Stop();
            _esConn.Close();
        }
    }
}
