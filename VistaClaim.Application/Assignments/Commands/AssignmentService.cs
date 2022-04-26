using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Domain.Entities.AssignmentEntity.Properties;
using VistaClaim.Entities._Base;

namespace VistaClaim.Application.Assignments.Commands
{
    //public class AssignmentService : BaseCommand,
    //    IRequestHandler<CreateAssignementCommand>,
    //    IRequestHandler<ApproveAssignementCommand>,
    //    IRequestHandler<CompleteAssignmentCommand>
    //{
    //    public AssignmentService(IAggregateStore aggregateStore) : base(aggregateStore)
    //    {
    //    }

    //    public async Task<Unit> Handle(ApproveAssignementCommand request, CancellationToken cancellationToken)
    //    {
    //        return await this.HandleUpdate<Domain.Entities.AssignmentEntity.Assignment>(EntityId.FromGuid(request.Id), assignment => assignment.Approve());
    //    }

    //    public async Task<Unit> Handle(CompleteAssignmentCommand request, CancellationToken cancellationToken)
    //    {
    //        return await this.HandleUpdate<Domain.Entities.AssignmentEntity.Assignment>(EntityId.FromGuid(request.Id), assignment => assignment.Complete());
    //    }

    //    public async Task<Unit> Handle(CreateAssignementCommand request, CancellationToken cancellationToken)
    //    {
    //        var entity = new Domain.Entities.AssignmentEntity.Assignment(
    //           EntityId.FromGuid(request.Id),
    //           EntityId.FromGuid(request.ClientId),
    //           AssignmentClaimNumber.FromString(request.ClaimNumber));

    //        await this.HandleCreate(entity);

    //        return Unit.Value;
    //    }
    //}
}
