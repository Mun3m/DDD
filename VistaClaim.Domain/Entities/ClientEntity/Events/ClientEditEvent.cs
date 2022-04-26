using System;
using System.Collections.Generic;
using System.Text;

namespace VistaClaim.Domain.Entities.ClientEntity.Events
{
    public class ClientEditEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
