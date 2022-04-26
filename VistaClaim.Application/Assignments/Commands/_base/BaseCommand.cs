using MediatR;
using System;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Exceptions;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Domain.Entities._Base;
using VistaClaim.Entities._Base;

namespace VistaClaim.Application.Assignments.Commands
{
    public abstract class BaseCommand
    {
        protected readonly IAggregateStore aggregateStore;

        public BaseCommand(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        protected async Task HandleCreate<T>(T aggregate) where T : AggregateRoot
        {
            if (await aggregateStore.Exists<T, EntityId>(aggregate.Id))
                throw new InvalidOperationException($"Entity with id {aggregate.Id} already exists");

            await aggregateStore.Save<T, int>(aggregate);
        }

        protected async Task<Unit> HandleUpdate<T>(EntityId id, Action<T> operation) where T : AggregateRoot
        {
            var found = await aggregateStore.Exists<T, EntityId>(id);
            if (!found)
                throw new NotFoundException(nameof(T), id);

            var entity = await aggregateStore.Load<T, EntityId>(id);
            operation(entity);

            await aggregateStore.Save<T, int>(entity);

            return Unit.Value;
        }
    }
}
