using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Entities._Base;

namespace VistaClaim.Application.Assignments.Commands
{
    public class CompleteAssignmentCommandHandler : BaseCommand, IRequestHandler<CompleteAssignmentCommand, Unit>
    {
        public CompleteAssignmentCommandHandler(IAggregateStore aggregateStore) : base(aggregateStore) { }

        public async Task<Unit> Handle(CompleteAssignmentCommand request, CancellationToken cancellationToken)
        {
            await this.HandleUpdate<Domain.Entities.AssignmentEntity.Assignment>(EntityId.FromGuid(request.Id), assignment => assignment.Complete());

            //var found = await aggregateStore.Exists<Domain.Entities.AssignmentEntity.Assignment, int>(request.Id);
            //if (!found)
            //    throw new NotFoundException(nameof(Domain.Entities.AssignmentEntity.Assignment), request.Id);

            //var entity = await aggregateStore.Load<Domain.Entities.AssignmentEntity.Assignment, int>(request.Id);
            //entity.Complete();

            return Unit.Value;
        }
    }
}
