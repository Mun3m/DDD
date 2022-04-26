using AutoMapper;
using System;
using VistaClaim.Application.Company.Queries.GetById;
using Xunit;

namespace VistaClaim.Application.Tests.Common.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Should_have_valid_configuration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Domain.Entities.CompanyEntity.Company), typeof(GetCompanyByIdQueryResponse))]
        public void Should_support_mapping_from_source_to_destination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
