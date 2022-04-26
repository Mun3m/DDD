using Microsoft.EntityFrameworkCore;

namespace VistaClaim.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entities.CompanyEntity.Company> Companies { get; set; }
    }
}
