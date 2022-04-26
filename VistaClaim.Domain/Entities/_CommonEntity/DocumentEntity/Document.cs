using System;
using VistaClaim.Domain.Entities._Base;

namespace VistaClaim.Domain.Entities._CommonEntity.DocumentEntity
{
    public abstract class Document : Entity
    {
        protected Document(Action<object> applier) : base(applier)
        {
        }
    }
}
