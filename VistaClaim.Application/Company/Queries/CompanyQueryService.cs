using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VistaClaim.Application.Company.Queries.Models;
using VistaClaim.Application.Company.Queries.ResponseModels;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Application.Company
{
    public static class CompanyQueryService
    {
        public static async Task<IEnumerable<CompanyReadModels.CompanyDto>> Query(this IApplicationDbContext dbContext, IMapper mapper, CompanyQueryModels.GetActiveCompanies query)
        {
            return await dbContext.Companies
                .Where(x => x.Active)
                .ProjectTo<CompanyReadModels.CompanyDto>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
