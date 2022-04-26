using System;
using System.Collections.Generic;
using System.Linq;
using VistaClaim.Domain.Entities._Base;
using VistaClaim.Domain.Entities._Base.Exceptions;
using VistaClaim.Domain.Entities._CommonEntity.PhotoEntity.Properties;
using VistaClaim.Domain.Entities.AssignmentEntity.Events;
using VistaClaim.Domain.Entities.AssignmentEntity.Properties;
using VistaClaim.Domain.Entities.AssignmentPhotoEntity;
using VistaClaim.Domain.Entities.AssignmentPhotoEntity.Events;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities.AssignmentEntity
{
    public class Assignment : AggregateRoot
    {
        protected Assignment() { }

        public Assignment(EntityId id, EntityId clientId, AssignmentClaimNumber claimNumber)
        {
            Apply(new AssignmentCreatedEvent
            {
                Id = id,
                ClientId = clientId,
                ClaimNumber = claimNumber
            });
        }

        public AssignmentStatus Status { get; set; }

        public EntityId ClientId { get; internal set; }

        public AssignmentClaimNumber ClaimNumber { get; internal set; }

        public ICollection<AssignmentPhoto> Photos { get; set; } = new HashSet<AssignmentPhoto>();

        public void AddPhoto(PictureSize pictureSize, Uri uri)
        {
            Apply(new PictureAddedToAsignmentEvent
            {
                AssignmentId = Id,
                Height = pictureSize.Height,
                Width = pictureSize.Width,
                Order = NewPhotoOrder(),
                Url = uri.ToString()
            });

            int NewPhotoOrder() => Photos.Any() ? Photos.Max(x => x.Order) + 1 : 0;
        }

        public void Approve()
        {
            Apply(new AssignmentSentForApproveEvent { Id = Id });
        }

        public void Complete()
        {
            Apply(new AssignmentCompletedEvent { Id = Id, ClientId = ClientId });
        }

        protected override void EnsureValidState()
        {
            var valid = ClaimNumber != null;

            if (!valid)
                throw new InvalidEntityStateException(this, "Validation checks failed.");
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case AssignmentCreatedEvent e:
                    Id = new EntityId(e.Id);
                    ClientId = new EntityId(e.ClientId);
                    ClaimNumber = new AssignmentClaimNumber(e.ClaimNumber);
                    Status = AssignmentStatus.New;
                    break;
                case PictureAddedToAsignmentEvent e:
                    {
                        var picture = new AssignmentPhoto(Apply);
                        ApplyToEntity(picture, e);
                        Photos.Add(picture);
                        break;
                    }
                case AssignmentSentForApproveEvent e:
                    {
                        Status = AssignmentStatus.SentForApprove;
                        break;
                    }
                case AssignmentCompletedEvent e:
                    {
                        Status = AssignmentStatus.Completed;
                        break;
                    }
            }
        }

        public enum AssignmentStatus
        {
            New,
            SentForApprove,
            Completed
        }
    }
}
