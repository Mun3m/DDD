using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VistaClaim.Application.Interfaces;
using VistaClaim.Persistence.EF;

namespace VistaClaim.Persistence.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContextEF dbContext;

        public EFUnitOfWork(ApplicationContextEF dbContext) => this.dbContext = dbContext;

        public Task Commit() => this.dbContext.SaveChangesAsync();
    }
}
