using EventStore.ClientAPI;
using System;
using System.Linq;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Domain.Entities._Base;
using VistaClaim.Entities._Base;
using VistaClaim.Persistence.Extensions;

namespace VistaClaim.Persistence.EventStore
{
    public class EventStoreAgrregateStore : IAggregateStore
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreAgrregateStore(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Exists<T, TId>(TId aggregateId)
        {
            var stream = GetStreamName<T, TId>(aggregateId);
            var result = await _connection.ReadEventAsync(stream, 1, false);

            return result.Status != EventReadStatus.NoStream;
        }

        public async Task<T> Load<T, TId>(TId aggregateId) where T : AggregateRoot
        {
            if (aggregateId == null)
                throw new ArgumentNullException(nameof(aggregateId));

            var stream = GetStreamName<T, TId>(aggregateId);
            var aggregate = (T)Activator.CreateInstance(typeof(T), true);

            var page = await _connection.ReadStreamEventsForwardAsync(stream, 0, 1024, false);

            aggregate.Load(page.Events.Select(resolvedEvent => resolvedEvent.Deserialzie()).ToArray());

            return aggregate;
        }

        public async Task Save<T, TId>(T aggregate) where T : AggregateRoot
        {
            if (aggregate == null)
                throw new ArgumentNullException(nameof(aggregate));

            var changes = aggregate.GetChanges();
            if (changes.Any() == false)
                return;

            var stream = GetStreamName<T, TId>(aggregate);

            await _connection.AppendEvents(stream, aggregate.Version, changes.ToArray());

            aggregate.ClearChanges();
        }

        private static string GetStreamName<T, TId>(TId aggregateId)
                => $"{typeof(T).Name}-{aggregateId}";

        private static string GetStreamName<T, TId>(T aggregate) where T : AggregateRoot
            => $"{typeof(T).Name}-{aggregate.Id.Value}";
    }

    public class EventMetadata
    {
        public string ClrType { get; set; }
    }
}
