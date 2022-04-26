using Microsoft.EntityFrameworkCore;
using System;
using static VistaClaim.Domain.Entities.AssignmentEntity.Assignment;

namespace VistaClaim.Application.Interfaces
{
    public interface IApplicationDbContextEventStore
    {
        DbSet<ReadModels.Client> Clients { get; set; }

        DbSet<ReadModels.Assignment> Assignments { get; set; }

        DbSet<ReadModels.Dashboard> Dashboards { get; set; }
    }

    public static class ReadModels
    {
        public class Client
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class Assignment
        {
            public Guid Id { get; set; }
            public AssignmentStatus Status { get; set; }
            public string ClaimNumber { get; set; }
            public Guid ClientId { get; set; }
            public string ClientName { get; set; }
        }

        public class Dashboard
        {
            public Guid Id { get; set; }
            public int New { get; set; }
            public int Approved { get; set; }
            public int Completed { get; set; }
        }
    }
}
