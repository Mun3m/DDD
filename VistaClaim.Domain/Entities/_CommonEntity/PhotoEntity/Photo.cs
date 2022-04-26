using System;
using VistaClaim.Domain.Entities._Base;
using VistaClaim.Domain.Entities._CommonEntity.PhotoEntity.Properties;

namespace VistaClaim.Domain.Entities._CommonEntity.PhotoEntity
{
    public abstract class Photo : Entity
    {
        protected Photo(Action<object> applier) : base(applier)
        {
        }

        public PictureSize Size { get; protected set; }

        public Uri Url { get; protected set; }

        public int Order { get; protected set; }
    }
}
