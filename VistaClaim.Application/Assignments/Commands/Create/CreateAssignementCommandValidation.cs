using FluentValidation;
using System;

namespace VistaClaim.Application.Assignments.Commands
{
    public class CreateAssignementCommandValidation : AbstractValidator<CreateAssignementCommand>
    {
        public CreateAssignementCommandValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotEqual(Guid.Empty).WithMessage("Id is required.");

            RuleFor(x => x.ClientId)
                 .NotEmpty().NotEqual(Guid.Empty).WithMessage("ClientId is required.");

            RuleFor(x => x.ClaimNumber)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(200).WithMessage("Username must not exceed 200 characters.");
        }
    }
}
