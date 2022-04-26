using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Company.Queries.GetById;
using VistaClaim.Application.Tests.ConfigureServices;
using VistaClaim.Domain.Entities.CompanyEntity.Properties;
using VistaClaim.Entities._Base;
using Xunit;

namespace VistaClaim.Application.Tests.CompanyTest.Queries
{
    [Collection("ServiceCollection")]
    public class GetCompanyByIdQueryTest
    {
        private readonly ServiceCollectionFixture serviceCollection;

        public GetCompanyByIdQueryTest(ServiceCollectionFixture serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        [Fact]
        public async Task Should_get_company_by_id()
        {
            //var entity = new Domain.Entities.CompanyEntity.Company(EntityId.FromGuid(Guid.NewGuid()), 
            //                                                    CompanyName.FromString("Company 1"), 
            //                                                    CompanyCode.FromString("Code 1"));

            //await serviceCollection.DbContext.Companies.AddAsync(entity);
            //await serviceCollection.DbContext.SaveChangesAsync();

            //var handler = new GetCompanyByIdQueryHandler(serviceCollection.DbContext, serviceCollection.Mapper);

            //var result = await handler.Handle(new GetCompanyByIdQuery { Id = entity.Id }, CancellationToken.None);

            //result.ShouldNotBeNull();
        }
    }
}
