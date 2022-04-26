using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Entities._Base;

namespace VistaClaim.Application.Assignments.Commands
{
    public class ApproveAssignementCommandHandler : BaseCommand, IRequestHandler<ApproveAssignementCommand, Unit>
    {
        public ApproveAssignementCommandHandler(IAggregateStore aggregateStore) : base(aggregateStore) { }

        public async Task<Unit> Handle(ApproveAssignementCommand request, CancellationToken cancellationToken)
        {
            await this.HandleUpdate<Domain.Entities.AssignmentEntity.Assignment>(EntityId.FromGuid(request.Id), assignment => assignment.Approve());

            //var found = await aggregateStore.Exists<Domain.Entities.AssignmentEntity.Assignment, int>(request.Id);
            //if (!found)
            //    throw new NotFoundException(nameof(Domain.Entities.AssignmentEntity.Assignment), request.Id);

            //var entity = await aggregateStore.Load<Domain.Entities.AssignmentEntity.Assignment, int>(request.Id);
            //entity.Approve();

            return Unit.Value;
        }
    }
}
