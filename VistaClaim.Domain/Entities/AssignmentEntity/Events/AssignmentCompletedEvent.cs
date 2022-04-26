using System;
using System.Collections.Generic;
using System.Text;

namespace VistaClaim.Domain.Entities.AssignmentEntity.Events
{
    public class AssignmentCompletedEvent
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
    }
}
