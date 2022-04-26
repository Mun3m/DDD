using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Exceptions;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Application.Assignments.Queries
{
    public class GetAssignmentByIdQueryHandler : IRequestHandler<GetAssignmentByIdQuery, ReadModels.Assignment>
    {
        private readonly IApplicationDbContextEventStore _context;

        public GetAssignmentByIdQueryHandler(IApplicationDbContextEventStore context)
        {
            _context = context;
        }

        public async Task<ReadModels.Assignment> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
        {
            var assignment = await _context.Assignments.FirstOrDefaultAsync(x=> x.Id == request.Id);
            if (assignment == null)
                throw new NotFoundException("Assignment", request.Id);

            return assignment;
        }
    }
}
