using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace VistaClaim.Application.Company.Commands.Create
{
    public class CreateCompanyCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
