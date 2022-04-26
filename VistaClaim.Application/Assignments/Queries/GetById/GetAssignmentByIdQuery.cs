using MediatR;
using System;
using System.Collections.Generic;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Application.Assignments.Queries
{
    public class GetAssignmentByIdQuery : IRequest<ReadModels.Assignment>
    {
        public Guid Id { get; set; }
    }
}
