using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VistaClaim.Application.Interfaces;
using VistaClaim.Domain.Entities.ClientEntity.Events;
using VistaClaim.Persistence.EventStore;
using VistaClaim.Persistence.EventStore.EF;

namespace VistaClaim.Persistence.Subscriptions
{
    public class ClientProjection : EFContextProjection<ReadModels.Client>
    {
        public ClientProjection(Func<IDbContextFactory<ESApplicationContext>> contextFactory) : base(contextFactory)
        {
        }

        public override async Task Project(object @event)
        {
            switch (@event)
            {
                case ClientCreatedEvent e:
                    {
                        await Create(async () =>
                            new ReadModels.Client
                            {
                                Id = e.Id,
                                Name = e.Name,
                                Email = e.Email
                            }
                        );

                        break;
                    }
                case AssignmentUpcastersProjection.AssignmentUpcastedEvents.V1.AssignmentCompletedEvent e:
                    {
                        var clientEmail = e.ClientEmail;

                        break;
                    }
            }
        }
    }
}
