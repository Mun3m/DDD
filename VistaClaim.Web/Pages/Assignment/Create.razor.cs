using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VistaClaim.Application.Assignments.Commands;

namespace VistaClaim.Web.Pages.Assignment
{
    public partial class Create : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IMediator Meditor { get; set; }

        private CreateAssignementCommand assignment;

        [Parameter]
        public Guid ClientId { get; set; }

        public Create()
        {
        }

        protected override async Task OnInitializedAsync() => 
            assignment = new CreateAssignementCommand { Id = Guid.NewGuid(), ClientId = ClientId };

        private async Task HandleValidSubmit()
        {
            await Meditor.Send(assignment);

            NavigationManager.NavigateTo("/assignment/index");
        }
    }
}
