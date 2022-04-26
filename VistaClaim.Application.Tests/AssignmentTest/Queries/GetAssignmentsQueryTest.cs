using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Assignments.Queries.Dashboard;
using VistaClaim.Application.Assignments.Queries.GetAll;
using VistaClaim.Application.Tests.ConfigureServices;
using Xunit;

namespace VistaClaim.Application.Tests.AssignmentTest.Queries
{
    [Collection("ESServiceCollection")]
    public class GetAssignmentsQueryTest
    {
        private readonly ESServiceCollectionFixture serviceCollection;

        public GetAssignmentsQueryTest(ESServiceCollectionFixture serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        [Fact]
        public async Task Should_get_all_assignmens()
        {
            var handler = new GetAssignmentsQueryHandler(serviceCollection.DbContext);

            var result = await handler.Handle(new GetAssignmentsQuery {}, CancellationToken.None);
            result.Count().ShouldBeGreaterThan(0);

            var clientName = result.First().ClientName;
            clientName.ShouldBe("Inssio");
        }

        [Fact]
        public async Task Should_get_dashboard()
        {
            var handler = new GetDashboardQueryHandler(serviceCollection.DbContext);

            var result = await handler.Handle(new GetDashboardQuery { }, CancellationToken.None);

            result.New.ShouldBe(0);
            result.Completed.ShouldBeGreaterThan(1);
            result.Approved.ShouldBeGreaterThan(1);
        }
    }
}
