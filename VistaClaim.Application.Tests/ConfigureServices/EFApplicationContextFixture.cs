using Microsoft.EntityFrameworkCore;
using System;
using VistaClaim.Persistence.EF;

namespace VistaClaim.Application.Tests.ConfigureServices
{
    public class EFApplicationContextFixture : IDisposable
    {
        public EFApplicationContextFixture()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContextEF>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.Instance = new ApplicationContextEF(builder.Options);
            this.Instance.Database.EnsureCreated();
        }

        public ApplicationContextEF Instance { get; set; }

        public void Dispose()
        {
            this.Instance.Database.EnsureDeleted();
            this.Instance.Dispose();
        }
    }
}
