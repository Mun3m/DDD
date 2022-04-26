using AutoFixture.Xunit2;
using EventStore.ClientAPI;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Assignments.Commands;
using VistaClaim.Domain.Entities._CommonEntity.PhotoEntity.Properties;
using VistaClaim.Domain.Entities.AssignmentEntity.Properties;
using VistaClaim.Domain.Entities.ClientEntity.Properties;
using VistaClaim.Entities._Base;
using VistaClaim.Persistence.EventStore;
using Xunit;

namespace VistaClaim.Application.Tests.AssignmentTest.Commands
{
    public class CreateAssignmentTest
    {
        public CreateAssignmentTest()
        {
        }

        [Theory, AutoData]
        public async Task Should_create_assignement(CreateAssignementCommand command)
        {
            var esConn = EventStoreConnection.Create("ConnectTo=tcp://admin:changeit@127.0.0.1:1113;DefaultUserCredentials=admin:changeit;", ConnectionSettings.Create().KeepReconnecting());
            await esConn.ConnectAsync();

            var handler = new CreateAssignementCommandHandler(new EventStoreAgrregateStore(esConn));

            var id = await handler.Handle(command, CancellationToken.None);

            esConn.Close();
        }

        [Fact]
        public async Task Event_sourcing_example()
        {
            var esConn = EventStoreConnection.Create("ConnectTo=tcp://admin:changeit@127.0.0.1:1113;DefaultUserCredentials=admin:changeit;", ConnectionSettings.Create().KeepReconnecting());
            await esConn.ConnectAsync();

            var store = new EventStoreAgrregateStore(esConn);

            var client = new Domain.Entities.ClientEntity.Client(EntityId.FromGuid(Guid.NewGuid()),
                                                                 EntityId.FromGuid(Guid.NewGuid()),
                                                                 ClientName.FromString("Inssio"),
                                                                 ClientEmail.FromString("Inssio@gmail.com"));

            await store.Save<Domain.Entities.ClientEntity.Client, EntityId>(client);

            for (var i = 0; i < 10; i++)
            {
                var assignment = new Domain.Entities.AssignmentEntity.Assignment(EntityId.FromGuid(Guid.NewGuid()), client.Id, AssignmentClaimNumber.FromString($"{0}"));

                assignment.Approve();

                if (i % 2 == 0)
                    assignment.Complete();

                await store.Save<Domain.Entities.AssignmentEntity.Assignment, EntityId>(assignment);
            }

            // var loadedEntity = await store.Load<Domain.Entities.AssignmentEntity.Assignment, EntityId>(entity.Id);

            esConn.Close();
        }
    }
}
