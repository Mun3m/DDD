using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaClaim.Application.Interfaces;
using VistaClaim.Domain.Entities.CompanyEntity;

namespace VistaClaim.Persistence.EF
{
    public class ApplicationContextEF : DbContext, IApplicationDbContext
    {
        public ApplicationContextEF(DbContextOptions<ApplicationContextEF> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
        }

        public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
        {
            public void Configure(EntityTypeBuilder<Company> builder)
            {
                builder.HasKey(x => x.Id);
                builder.OwnsOne(x => x.Name);
                builder.OwnsOne(x => x.Code);
            }
        }
    }
}
