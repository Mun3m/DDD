using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VistaClaim.Persistence.Subscriptions.Manager;

namespace VistaClaim.Persistence.EventStore.EF
{
    public abstract class EFContextProjection<T> : IProjection where T : class
    {
        private readonly Func<IDbContextFactory<ESApplicationContext>> _contextFactory;

        protected IDbContextFactory<ESApplicationContext> Instance { get { return _contextFactory(); } }

        public EFContextProjection(Func<IDbContextFactory<ESApplicationContext>> contextFactory) =>
            _contextFactory = contextFactory;

        public abstract Task Project(object @event);

        protected async Task Create(Func<Task<T>> model) =>
            await UsingSession(async context => await context.AddAsync(await model()));

        protected async Task UpdateOne(Guid id, Action<T> action) =>
            await UsingSession(async context => await UpdateItem(context, id, action));

        protected async Task UpdateWhere(Expression<Func<T, bool>> where, Action<T> action) =>
            await UsingSession(async context => await UpdateMultipleItems(context, where, action));

        private async Task UpdateItem(ESApplicationContext context, Guid id, Action<T> action)
        {
            var item = await context.FindAsync<T>(id);
            if (item == null)
                return;

            action(item);
        }

        private async Task UpdateMultipleItems(ESApplicationContext context, Expression<Func<T, bool>> query, Action<T> action)
        {
            var items = await context.Set<T>().Where(query).ToListAsync();
            foreach (var item in items)
                action(item);
        }

        protected async Task UsingSession(Func<ESApplicationContext, Task> operation)
        {
            using (var session = _contextFactory().CreateDbContext())
            {
                await operation(session);
                await session.SaveChangesAsync();
            }
        }
    }
}
