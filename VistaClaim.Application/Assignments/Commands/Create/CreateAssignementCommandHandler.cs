using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Domain.Entities.AssignmentEntity.Properties;
using VistaClaim.Entities._Base;

namespace VistaClaim.Application.Assignments.Commands
{
    public class CreateAssignementCommandHandler : BaseCommand, IRequestHandler<CreateAssignementCommand>
    {
        public CreateAssignementCommandHandler(IAggregateStore aggregateStore) : base(aggregateStore) { }

        public async Task<Unit> Handle(CreateAssignementCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.AssignmentEntity.Assignment(
                EntityId.FromGuid(request.Id),
                EntityId.FromGuid(request.ClientId),
                AssignmentClaimNumber.FromString(request.ClaimNumber));

            await this.HandleCreate(entity);

            return Unit.Value;

            // await aggregateStore.Save<Domain.Entities.AssignmentEntity.Assignment, int>(entity);
        }
    }
}
