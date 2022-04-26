using AutoFixture.Xunit2;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Company.Commands.Create;
using VistaClaim.Application.Tests.ConfigureServices;
using Xunit;

namespace VistaClaim.Application.Tests.CompanyTest.Commands
{
    [Collection("ServiceCollection")]
    public class CreateCompanyTest
    {
        private readonly ServiceCollectionFixture serviceCollection;

        public CreateCompanyTest(ServiceCollectionFixture serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        [Theory, AutoData]
        public async Task Should_create_student(CreateCompanyCommand command)
        {
            //var createStudentCommandHandler = new CreateCompanyCommandHandler(serviceCollection.UnitOfWork, serviceCollection.CompanyRepository);

            //var id = await createStudentCommandHandler.Handle(command, CancellationToken.None);

            //var entity = await serviceCollection.DbContext.Companies.FindAsync(id);
            //entity.ShouldNotBeNull();
        }
    }
}
