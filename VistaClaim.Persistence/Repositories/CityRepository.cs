using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VistaClaim.Application.Interfaces;
using VistaClaim.Domain.Entities.CompanyEntity;
using VistaClaim.Entities._Base;
using VistaClaim.Persistence.EF;

namespace VistaClaim.Persistence.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationContextEF dbContext;

        public CompanyRepository(ApplicationContextEF dbContext)
            => this.dbContext = dbContext;

        public async Task Add(Company company)
            => await dbContext.AddAsync(company);

        public async Task<bool> Exists(EntityId id)
            => await dbContext.Companies.FindAsync(id) != null;

        public async Task<Company> Get(EntityId id)
            => await dbContext.Companies.FindAsync(id);
    }
}
