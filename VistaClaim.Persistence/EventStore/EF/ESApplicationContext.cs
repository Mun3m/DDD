using EventStore.ClientAPI;
using Microsoft.EntityFrameworkCore;
using System;
using VistaClaim.Application.Interfaces;

namespace VistaClaim.Persistence.EventStore
{
    public class ESApplicationContext : DbContext, IApplicationDbContextEventStore
    {
        public ESApplicationContext(DbContextOptions<ESApplicationContext> options) : base(options)
        {
        }

        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<ReadModels.Assignment> Assignments { get; set; }
        public DbSet<ReadModels.Client> Clients { get; set; }
        public DbSet<ReadModels.Dashboard> Dashboards { get; set; }
    }

    public class Checkpoint
    {
        public string Id { get; set; }

        public CurrentPosition Position { get; set; }
    }

    public class CurrentPosition
    {
        public Guid Id { get; set; }

        public Checkpoint Checkpoint { get; set; }
        public string CheckpointId { get; set; }

        public long CommitPosition { get; set; }
        public long PreparePosition { get; set; }
    }
}
