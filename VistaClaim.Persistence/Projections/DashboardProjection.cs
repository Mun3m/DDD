using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VistaClaim.Application.Interfaces;
using VistaClaim.Domain.Entities.AssignmentEntity.Events;
using VistaClaim.Persistence.EventStore;
using VistaClaim.Persistence.EventStore.EF;

namespace VistaClaim.Persistence.Subscriptions
{
    public class DashboardProjection : EFContextProjection<ReadModels.Dashboard>
    {
        public const string DashboardId = "163ba4a7-7671-42ed-b08d-ddfadfa80586";

        public DashboardProjection(Func<IDbContextFactory<ESApplicationContext>> contextFactory) : base(contextFactory)
        {
        }

        public async override Task Project(object @event)
        {
            switch (@event)
            {
                case AssignmentCreatedEvent e:
                    {
                        await UpdateOne(new Guid(DashboardId), x => x.New++);
                        break;
                    }
                case AssignmentSentForApproveEvent e:
                    {
                         await UpdateWhere(x => x.Id == new Guid(DashboardId),
                            x =>
                            {
                                x.New--;
                                x.Approved++;
                            });

                        break;
                    }
                case AssignmentCompletedEvent e:
                    {
                        await UpdateWhere(x => x.Id == new Guid(DashboardId),
                            x =>
                            {
                                x.Approved--;
                                x.Completed++;
                            });

                        break;
                    }
            }
        }
    }
}
