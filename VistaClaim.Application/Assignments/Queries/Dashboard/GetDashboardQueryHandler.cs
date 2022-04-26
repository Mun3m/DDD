using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Application.Assignments.Queries.Dashboard
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, ReadModels.Dashboard>
    {
        private readonly IApplicationDbContextEventStore _context;

        public GetDashboardQueryHandler(IApplicationDbContextEventStore context)
        {
            _context = context;
        }

        public async Task<ReadModels.Dashboard> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _context.Dashboards.FirstAsync();
        }
    }
}
