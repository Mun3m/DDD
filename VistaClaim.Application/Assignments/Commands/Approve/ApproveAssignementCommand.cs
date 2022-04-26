using MediatR;
using System;

namespace VistaClaim.Application.Assignments.Commands
{
    public class ApproveAssignementCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
