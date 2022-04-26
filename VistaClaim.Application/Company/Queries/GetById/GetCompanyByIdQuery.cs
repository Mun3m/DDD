using AutoMapper;
using MediatR;
using System;
using VistaClaim.Application.Common.Mappings;

namespace VistaClaim.Application.Company.Queries.GetById
{
    public class GetCompanyByIdQuery : IRequest<GetCompanyByIdQueryResponse>, ICacheMap<GetCompanyByIdQuery>
    {
        public Guid Id { get; set; }
    }

    public class GetCompanyByIdQueryResponse : Map<Domain.Entities.CompanyEntity.Company>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        //// Custom mapper
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<Domain.Entities.CompanyEntity.Company, GetCompanyByIdQueryResponse>();
        //    //.ForMember(x => x.Name, opt => opt.MapFrom(x => $"{x.Name}-{x.Code}"));
        //}

        //// Custom Caching
        //public void Caching()
        //{
        //    // if default does not fit
        //}
    }
}
