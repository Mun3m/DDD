using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Behaviours;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Application.Company.Commands.Create;
using Xunit;

namespace VistaClaim.Application.Tests.Common.Behaviours
{
    public class UnhandledExceptionTests
    {
        private const string UserId = "1";

        public UnhandledExceptionTests()
        {
        }

        [Fact]
        public async Task UnhandledException_should_call_currentUserService_once()
        {
            var logger = new Mock<ILogger<CreateCompanyCommand>>();
            var currentUserService = new Mock<ICurrentUserService>();

            currentUserService.Setup(x => x.UserId).Returns(UserId);

            IPipelineBehavior<CreateCompanyCommand, int> unhandledException = new UnhandledExceptionBehaviour<CreateCompanyCommand, int>(logger.Object, currentUserService.Object);

            try
            {
                await unhandledException.Handle(new CreateCompanyCommand { Name = "Company 1", Code = "Code 1" }, new CancellationToken(),
                                               () =>
                                               {
                                                   throw new Exception("Hello, World!");
                                               });
            }
            catch
            {
                currentUserService.Verify(i => i.UserId, Times.Once);
            }
        }
    }
}
