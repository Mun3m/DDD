using EventStore.ClientAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Application.Interfaces;
using VistaClaim.Persistence.EventStore;
using VistaClaim.Persistence.Subscriptions;
using VistaClaim.Persistence.Subscriptions.Manager;

namespace VistaClaim.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddVistaClaimPersistence(this IServiceCollection services, string readSideConnString, string writeSideConnString, string checkpointName)
        {
            var esConnection = EventStoreConnection.Create(writeSideConnString,
                                                           ConnectionSettings.Create().KeepReconnecting(),
                                                           "VistaClaim");

            var store = new EventStoreAgrregateStore(esConnection);

            services.AddSingleton(esConnection);
            services.AddSingleton<IAggregateStore>(store);

            Func<IDbContextFactory<ESApplicationContext>> getSession = () =>
                services.BuildServiceProvider().GetRequiredService<IDbContextFactory<ESApplicationContext>>();

            services.AddDbContextFactory<ESApplicationContext>(options => options.UseSqlServer(readSideConnString));
            services.AddScoped<IApplicationDbContextEventStore>(s =>
            {
                return getSession().CreateDbContext();
            });

            var projectManager = new ProjectionManager(esConnection,
                                        new EFCheckpointStore(getSession, checkpointName),
                                        new ClientProjection(getSession),
                                        //new DashboardProjection(getSession),
                                        new AssignmentProjection(getSession,
                                    async clientId => (await getSession().CreateDbContext().Clients.FindAsync(clientId)).Name));

            services.AddSingleton<IHostedService>(s =>
            {
                return new HostedService(esConnection, projectManager);
            });

            return services;
        }
    }
}
