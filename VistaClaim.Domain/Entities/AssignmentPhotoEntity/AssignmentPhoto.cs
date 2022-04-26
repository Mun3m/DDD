using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Domain.Entities._Base;
using VistaClaim.Domain.Entities._CommonEntity.PhotoEntity;
using VistaClaim.Domain.Entities._CommonEntity.PhotoEntity.Properties;
using VistaClaim.Domain.Entities.AssignmentPhotoEntity.Events;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities.AssignmentPhotoEntity
{
    public class AssignmentPhoto : Photo
    {
        public AssignmentPhoto(Action<object> applier) : base(applier)
        {
        }

        public EntityId AssignmentId { get; set; }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case PictureAddedToAsignmentEvent e:
                    AssignmentId = new EntityId(e.AssignmentId);
                    Url = new Uri(e.Url);
                    Size = new PictureSize { Height = e.Height, Width = e.Width };
                    Order = e.Order;
                    break;
            }
        }
    }
}
