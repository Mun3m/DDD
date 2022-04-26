using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Assignments.Queries;
using VistaClaim.Application.Tests.ConfigureServices;
using Xunit;

namespace VistaClaim.Application.Tests.AssignmentTest.Queries
{
    [Collection("ESServiceCollection")]
    public class GetAssignmentByIdQueryTest
    {
        private readonly ESServiceCollectionFixture serviceCollection;

        public GetAssignmentByIdQueryTest(ESServiceCollectionFixture serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        [Fact]
        public async Task Should_get_assignment_by_id()
        {
            var handler = new GetAssignmentByIdQueryHandler(serviceCollection.DbContext);

            var id = new Guid("7621d61e-0090-4f71-9989-d78084964448");
            var result = await handler.Handle(new GetAssignmentByIdQuery { Id = id }, CancellationToken.None);

            result.ShouldNotBeNull();
        }
    }
}
