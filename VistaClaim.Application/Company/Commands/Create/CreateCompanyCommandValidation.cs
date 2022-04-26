using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace VistaClaim.Application.Company.Commands.Create
{
    public class CreateCompanyCommandValidation : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Username must not exceed 200 characters.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(200).WithMessage("Username must not exceed 200 characters.");
        }
    }
}
