using EventStore.ClientAPI;
using System;
using System.Threading.Tasks;
using VistaClaim.Domain.Entities.AssignmentEntity.Events;
using VistaClaim.Persistence.Extensions;
using VistaClaim.Persistence.Subscriptions.Manager;
using static VistaClaim.Persistence.Subscriptions.AssignmentUpcastersProjection.AssignmentUpcastedEvents;

namespace VistaClaim.Persistence.Subscriptions
{
    public class AssignmentUpcastersProjection : IProjection
    {
        private readonly Func<Guid, Task<string>> _getClientEmail;
        private readonly IEventStoreConnection _connection;
        private const string SteamName = "UpcastedAssignmentEvents";

        public AssignmentUpcastersProjection(IEventStoreConnection connection, Func<Guid, Task<string>> getClientEmail)
        {
            _getClientEmail = getClientEmail;
            _connection = connection;
        }

        public async Task Project(object @event)
        {
            switch (@event)
            {
                case AssignmentCompletedEvent e:
                    {
                        // new enriching event
                        var newEvent = new V1.AssignmentCompletedEvent
                        {
                            Id = e.Id,
                            ClientEmail = await _getClientEmail(e.ClientId)
                        };

                        await _connection.AppendEvents(SteamName, ExpectedVersion.Any, newEvent);

                        break;
                    }
            }
        }

        public static class AssignmentUpcastedEvents
        {
            public static class V1
            {
                public class AssignmentCompletedEvent
                {
                    public Guid Id { get; set; }

                    public string ClientEmail { get; set; }
                }
            }
        }
    }
}
