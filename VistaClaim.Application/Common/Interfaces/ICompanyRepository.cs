using System.Threading.Tasks;
using VistaClaim.Entities._Base;

namespace VistaClaim.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task Add(Domain.Entities.CompanyEntity.Company company);
        Task<Domain.Entities.CompanyEntity.Company> Get(EntityId id);
        Task<bool> Exists(EntityId id);
    }
}
