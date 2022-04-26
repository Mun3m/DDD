using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Application.Assignments.Queries.Dashboard
{
    public class GetDashboardQuery : IRequest<ReadModels.Dashboard>
    {
    }
}
