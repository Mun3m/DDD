using System;
using System.Collections.Generic;
using System.Text;

namespace VistaClaim.Domain.Entities.AssignmentPhotoEntity.Events
{
    public class PictureAddedToAsignmentEvent
    {
        public Guid AssignmentId { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Order { get; set; }
    }
}
