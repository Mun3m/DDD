using MediatR;
using System;

namespace VistaClaim.Application.Assignments.Commands
{
    public class CreateAssignementCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string ClaimNumber { get; set; }
    }
}
