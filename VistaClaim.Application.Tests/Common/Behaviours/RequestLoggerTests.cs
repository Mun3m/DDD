using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using VistaClaim.Application.Common.Behaviours;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Application.Company.Commands.Create;
using Xunit;

namespace VistaClaim.Application.Tests.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private const string UserId = "1";

        public RequestLoggerTests()
        {
        }

        [Fact]
        public void RequestLogger_should_call_currentUserService_once()
        {
            var logger = new Mock<ILogger<CreateCompanyCommand>>();
            var currentUserService = new Mock<ICurrentUserService>();

            currentUserService.Setup(x => x.UserId).Returns(UserId);

            IRequestPreProcessor<CreateCompanyCommand> requestLogger = new RequestLogger<CreateCompanyCommand>(logger.Object, currentUserService.Object);

            requestLogger.Process(new CreateCompanyCommand { Name = "Company 1", Code = "Code 1" }, new CancellationToken());

            currentUserService.Verify(i => i.UserId, Times.Once);
        }
    }
}
