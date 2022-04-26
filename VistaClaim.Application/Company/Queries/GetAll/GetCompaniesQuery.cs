using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Application.Common.Mappings;
using VistaClaim.Application.Company.Queries.ResponseModels;

namespace VistaClaim.Application.Company.Queries.GetAll
{
    public class GetCompaniesQuery : IRequest<IEnumerable<GetCompaniesQueryResponse>>
    {
    }

    public class GetCompaniesQueryResponse : Map<Domain.Entities.CompanyEntity.Company>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
