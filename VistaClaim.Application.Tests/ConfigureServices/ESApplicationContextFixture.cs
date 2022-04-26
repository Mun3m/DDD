using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Persistence.EventStore;

namespace VistaClaim.Application.Tests.ConfigureServices
{
    public class ESApplicationContextFixture : IDisposable
    {
        public ESApplicationContextFixture()
        {
            var builder = new DbContextOptionsBuilder<ESApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.Instance = new ESApplicationContext(builder.Options);
            this.Instance.Database.EnsureCreated();
        }

        public ESApplicationContext Instance { get; set; }

        public void Dispose()
        {
            this.Instance.Database.EnsureDeleted();
            this.Instance.Dispose();
        }
    }
}