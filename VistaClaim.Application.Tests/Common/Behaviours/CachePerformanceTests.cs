using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Behaviours;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Application.Common.Mappings;
using VistaClaim.Application.Company.Commands.Create;
using VistaClaim.Application.Company.Queries.GetById;
using Xunit;

namespace VistaClaim.Application.Tests.Common.Behaviours
{
    public class CachePerformanceTests
    {
        private const string UserId = "1";

        public CachePerformanceTests()
        {
        }

        [Fact]
        public async Task RequestPerformance_should_call_currentUserService_once()
        {
            var logger = new Mock<ILogger<CreateCompanyCommand>>();
            var currentUserService = new Mock<ICurrentUserService>();
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var mappingCache = new MappingCache();

            currentUserService.Setup(x => x.UserId).Returns(UserId);

            IPipelineBehavior<GetCompanyByIdQuery, GetCompanyByIdQueryResponse> requestPerformance = new CachePerformanceBehaviour<GetCompanyByIdQuery, GetCompanyByIdQueryResponse>(memoryCache, currentUserService.Object, mappingCache);

            var id = Guid.NewGuid();

            await requestPerformance.Handle(new GetCompanyByIdQuery { Id = id }, new CancellationToken(),
                                           () =>
                                           {
                                               return Task.FromResult(new GetCompanyByIdQueryResponse { Name = "company 1", Code = "Code 1" });
                                           });

            await requestPerformance.Handle(new GetCompanyByIdQuery { Id = id }, new CancellationToken(),
                                          () =>
                                          {
                                              return Task.FromResult(new GetCompanyByIdQueryResponse { Name = "company 1", Code = "Code 1" });
                                          });
        }
    }
}
