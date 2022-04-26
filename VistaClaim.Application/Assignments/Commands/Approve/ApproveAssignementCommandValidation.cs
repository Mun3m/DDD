using FluentValidation;
using System;

namespace VistaClaim.Application.Assignments.Commands
{
    public class ApproveAssignementCommandValidation : AbstractValidator<ApproveAssignementCommand>
    {
        public ApproveAssignementCommandValidation()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().NotEqual(Guid.Empty).WithMessage("Id is required.");
        }
    }
}
