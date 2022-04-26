using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Application.Assignments.Queries.GetAll
{
    public class GetAssignmentsQuery : IRequest<IEnumerable<ReadModels.Assignment>>
    {
        public string FilterByClaimNumber { get; set; }
    }
}
