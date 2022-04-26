using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Application.Assignments.Queries.GetAll
{
    public class GetAssignmentsQueryHandler : IRequestHandler<GetAssignmentsQuery, IEnumerable<ReadModels.Assignment>>
    {
        private readonly IApplicationDbContextEventStore _context;

        public GetAssignmentsQueryHandler(IApplicationDbContextEventStore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReadModels.Assignment>> Handle(GetAssignmentsQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Assignments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.FilterByClaimNumber))
                result = result.Where(x => x.ClaimNumber.Contains(request.FilterByClaimNumber));

            return await result.ToListAsync();
        }
    }
}
