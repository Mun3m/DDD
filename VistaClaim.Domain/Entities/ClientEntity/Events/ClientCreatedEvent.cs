using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Domain.Entities.CompanyEntity.Events;

namespace VistaClaim.Domain.Entities.ClientEntity.Events
{
    public class ClientCreatedEvent
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
