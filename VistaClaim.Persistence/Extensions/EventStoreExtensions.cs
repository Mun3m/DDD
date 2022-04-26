using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaClaim.Persistence.EventStore;

namespace VistaClaim.Persistence.Extensions
{
    public static class EventStoreExtensions
    {
        public static async Task AppendEvents(this IEventStoreConnection connection, string streamName, long version, params object[] events)
        {
            var preparedEvents = events.Select(@event => new EventData(
                                      eventId: Guid.NewGuid(),
                                      type: @event.GetType().Name,
                                      isJson: true,
                                      data: Serialize(@event),
                                      metadata: Serialize(new EventMetadata { ClrType = @event.GetType().AssemblyQualifiedName })
                                  )).ToArray();


            await connection.AppendToStreamAsync(streamName, version, preparedEvents);
        }

        public static object Deserialzie(this ResolvedEvent resolvedEvent)
        {
            var metadata = JsonConvert.DeserializeObject<EventMetadata>(Encoding.UTF8.GetString(resolvedEvent.Event.Metadata));

            var dataType = Type.GetType(metadata.ClrType);

            var jsonData = Encoding.UTF8.GetString(resolvedEvent.Event.Data);

            var data = JsonConvert.DeserializeObject(jsonData, dataType);

            return data;
        }

        private static byte[] Serialize(object data)
            => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    }
}
