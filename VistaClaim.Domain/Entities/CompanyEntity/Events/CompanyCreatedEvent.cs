using System;
using System.Collections.Generic;
using System.Text;

namespace VistaClaim.Domain.Entities.CompanyEntity.Events
{
    public class CompanyCreatedEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get;set; }
    }
}
