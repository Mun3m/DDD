using VistaClaim.Application.Common.Mappings;

namespace VistaClaim.Application.Company.Queries.ResponseModels
{
    public static class CompanyReadModels
    {
        public class CompanyDto : Map<Domain.Entities.CompanyEntity.Company>
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
    }
}
