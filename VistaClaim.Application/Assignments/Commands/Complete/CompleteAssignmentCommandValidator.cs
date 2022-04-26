using FluentValidation;
using System;

namespace VistaClaim.Application.Assignments.Commands
{
    public class CompleteAssignmentCommandValidator : AbstractValidator<CompleteAssignmentCommand>
    {
        public CompleteAssignmentCommandValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().NotEqual(Guid.Empty).WithMessage("Id is required.");
        }
    }
}

