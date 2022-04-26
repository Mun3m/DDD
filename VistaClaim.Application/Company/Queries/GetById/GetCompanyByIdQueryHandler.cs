using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Exceptions;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Application.Company.Queries.GetById
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, GetCompanyByIdQueryResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        //public GetCompanyByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        //{
        //    _dbContext = dbContext;
        //    _mapper = mapper;
        //}

        public GetCompanyByIdQueryHandler()
        {

        }

        public async Task<GetCompanyByIdQueryResponse> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _dbContext.Companies
                .Where(x => x.Id == request.Id)
                .ProjectTo<GetCompanyByIdQueryResponse>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (company == null)
                throw new NotFoundException("Company", request.Id);

            return company;
        }
    }
}
