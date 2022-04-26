using VistaClaim.Domain.Entities._Base.Interfaces;
using VistaClaim.Entities._Base;
using System;

namespace VistaClaim.Domain.Entities._Base
{
    public abstract class Entity : IEntity, IInternalEventHandler
    {
        private readonly Action<object> _applier;

        protected Entity(Action<object> applier) => _applier = applier;

        public EntityId Id { get; protected set; }

        public string CreatedBy { get; protected set; }

        public DateTime Created { get; protected set; }

        public string LastModifiedBy { get; protected set; }

        public DateTime? LastModified { get; protected set; }

        protected void Apply(object @event)
        {
            When(@event);
            _applier(@event);
        }

        protected abstract void When(object @event);
        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}
