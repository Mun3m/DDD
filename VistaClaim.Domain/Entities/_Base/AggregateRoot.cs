using System;
using System.Collections.Generic;
using System.Linq;
using VistaClaim.Domain.Entities._Base.Interfaces;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities._Base
{
    public abstract class AggregateRoot : IEntity, IInternalEventHandler
    {
        private readonly List<object> _changes;

        protected AggregateRoot()
        {
            _changes = new List<object>();
        }

        public EntityId Id { get; protected set; }

        public string CreatedBy { get; protected set; }

        public DateTime Created { get; protected set; }

        public string LastModifiedBy { get; protected set; }

        public DateTime? LastModified { get; protected set; }

        public long Version { get; protected set; } = -1;

        protected abstract void When(object @event);

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }

        public void Load(IEnumerable<object> history)
        {
            foreach (var item in history)
            {
                When(item);
                Version++;
            }
        }

        protected abstract void EnsureValidState();
        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();
        public void ClearChanges() => _changes.Clear();
        protected void ApplyToEntity(IInternalEventHandler entity, object @event) => entity?.Handle(@event);
        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}
