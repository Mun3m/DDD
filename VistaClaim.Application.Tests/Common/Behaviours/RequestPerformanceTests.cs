using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Behaviours;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Application.Company.Commands.Create;
using Xunit;

namespace VistaClaim.Application.Tests.Common.Behaviours
{
    public class RequestPerformanceTests
    {
        private const string UserId = "1";

        public RequestPerformanceTests()
        {
        }

        [Fact]
        public async Task RequestPerformance_should_call_currentUserService_once()
        {
            var logger = new Mock<ILogger<CreateCompanyCommand>>();
            var currentUserService = new Mock<ICurrentUserService>();

            currentUserService.Setup(x => x.UserId).Returns(UserId);

            IPipelineBehavior<CreateCompanyCommand, int> requestPerformance = new RequestPerformanceBehaviour<CreateCompanyCommand, int>(logger.Object, currentUserService.Object);

            await requestPerformance.Handle(new CreateCompanyCommand { Name = "Company 1", Code = "Code 1" }, new CancellationToken(),
                                           () =>
                                           {
                                               Thread.Sleep(1000);
                                               return Task.FromResult(1);
                                           });

            currentUserService.Verify(i => i.UserId, Times.Once);
        }
    }
}
