using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VistaClaim.Application.Interfaces;
using VistaClaim.Domain.Entities.AssignmentEntity.Events;
using VistaClaim.Domain.Entities.ClientEntity.Events;
using VistaClaim.Persistence.EventStore;
using VistaClaim.Persistence.EventStore.EF;
using static VistaClaim.Domain.Entities.AssignmentEntity.Assignment;

namespace VistaClaim.Persistence.Subscriptions
{
    public class AssignmentProjection : EFContextProjection<ReadModels.Assignment>
    {
        private readonly Func<Guid, Task<string>> _getClientName;

        public AssignmentProjection(Func<IDbContextFactory<ESApplicationContext>> contextFactory, Func<Guid, Task<string>> getClientName) : base(contextFactory)
        {
            _getClientName = getClientName;
        }

        public override async Task Project(object @event)
        {
            switch (@event)
            {
                case AssignmentCreatedEvent e:
                    {
                        await Create(async () =>
                             new ReadModels.Assignment
                             {
                                 Id = e.Id,
                                 Status = AssignmentStatus.New,
                                 ClientName = await _getClientName(e.ClientId),
                                 ClaimNumber = e.ClaimNumber,
                                 ClientId = e.ClientId
                             }
                         );

                        break;
                    }
                case AssignmentSentForApproveEvent e:
                    {
                        await UpdateOne(e.Id, x => x.Status = AssignmentStatus.SentForApprove);

                        break;
                    }
                case AssignmentCompletedEvent e:
                    {
                        await UpdateOne(e.Id, x => x.Status = AssignmentStatus.Completed);

                        break;
                    }

                case ClientEditEvent e:
                    {
                        await UpdateWhere(
                                x => x.ClientId == e.Id,
                                x => x.ClientName = e.Name
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
