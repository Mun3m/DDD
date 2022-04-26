using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Persistence.EventStore;
using VistaClaim.Persistence.Extensions;

namespace VistaClaim.Persistence.Subscriptions.Manager
{
    public class ProjectionManager
    {
        private readonly IEventStoreConnection _connection;
        private readonly ICheckpointStore _checkpointStore;
        private readonly IProjection[] _projections;
        private EventStoreAllCatchUpSubscription _subscription;
        private bool _isTestingMode;

        // for testing purpose
        private Position? CurrentPosition { get; set; }
        private Position? SubscriptionCurrentPosition { get; set; }

        private CatchUpSubscriptionSettings _settings  = new CatchUpSubscriptionSettings(2000, 500, false, true, "try-out-subscription");


        public ProjectionManager(IEventStoreConnection connection, ICheckpointStore checkpointStore, params IProjection[] projections)
        {
            _connection = connection;
            _checkpointStore = checkpointStore;
            _projections = projections;
        }

        public async Task Start(bool isTestingMode = false)
        {
            _isTestingMode = isTestingMode;


            var position = await _checkpointStore.GetCheckpoint();
            _subscription = _connection.SubscribeToAllFrom(position, _settings, EventAppeared, ProcessingStarted, SubscriptionDropped);

            // for testing purpose
            if (_isTestingMode)
            {
                while (true)
                {
                    if (SubscriptionCurrentPosition > CurrentPosition)
                        break;

                    SubscriptionCurrentPosition = _subscription.LastProcessedPosition;
                }
            }
        }

        private void SubscriptionDropped(EventStoreCatchUpSubscription arg1, SubscriptionDropReason arg2, Exception arg3)
        {
            _subscription = _connection.SubscribeToAllFrom(_subscription.LastProcessedPosition, _settings, EventAppeared, ProcessingStarted, SubscriptionDropped);
        }

        private void ProcessingStarted(EventStoreCatchUpSubscription obj)
        {
            
        }

        public void Stop() => _subscription.Stop();

        private async Task EventAppeared(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.Event.EventType.StartsWith("$")) return;

            // for testing purpose
            if (_isTestingMode)
                CurrentPosition = resolvedEvent.OriginalPosition;

            var @event = resolvedEvent.Deserialzie();

            await Task.WhenAll(_projections.Select(x => x.Project(@event)));

            await _checkpointStore.SotreCheckpoint(resolvedEvent.OriginalPosition.Value);
        }
    }
}
