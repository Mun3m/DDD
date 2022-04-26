using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Application.Company.Queries.GetAll
{
    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, IEnumerable<GetCompaniesQueryResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCompaniesQueryHandler()
        {

        }

        //public GetCompaniesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        //{
        //    _dbContext = dbContext;
        //    _mapper = mapper;
        //}

        public async Task<IEnumerable<GetCompaniesQueryResponse>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            // code reuse example
            var activeCompanies = await _dbContext.Query(_mapper, new Models.CompanyQueryModels.GetActiveCompanies());

            return await _dbContext.Companies
                .ProjectTo<GetCompaniesQueryResponse>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
