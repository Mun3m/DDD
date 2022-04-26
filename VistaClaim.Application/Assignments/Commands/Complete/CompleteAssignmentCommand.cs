using MediatR;
using System;

namespace VistaClaim.Application.Assignments.Commands
{
    public class CompleteAssignmentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
