using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VistaClaim.Application.Assignments.Commands;
using VistaClaim.Application.Assignments.Queries.GetAll;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Web.Pages.Assignment
{
    public partial class Index : ComponentBase
    {
        private string filter { get; set; } = string.Empty;

        private IEnumerable<ReadModels.Assignment> assignments;

        [Inject]
        public IMediator Meditor { get; set; }

        private Guid ClinetId { get; set; } = new Guid("B3C31130-11FE-4BF2-9E22-E737764EB2DB");

        public Index()
        {
        }

        protected override async Task OnInitializedAsync() =>
            assignments = await Meditor.Send(new GetAssignmentsQuery());

        private async Task FilterBy() =>
            assignments = await Meditor.Send(new GetAssignmentsQuery { FilterByClaimNumber = filter });

        private async Task Approve(Guid id) =>
            await Meditor.Send(new ApproveAssignementCommand { Id = id });

        private async Task Complete(Guid id) =>
            await Meditor.Send(new CompleteAssignmentCommand { Id = id });
    }
}
