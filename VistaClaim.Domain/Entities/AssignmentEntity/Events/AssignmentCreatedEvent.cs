using System;
using System.Collections.Generic;
using System.Text;

namespace VistaClaim.Domain.Entities.AssignmentEntity.Events
{
    public class AssignmentCreatedEvent
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string ClaimNumber { get; set; }
    }
}
