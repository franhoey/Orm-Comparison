using Microsoft.EntityFrameworkCore;

namespace OrmComparison.EntityFrameworkCore
{
    public partial class OrmtestContext : DbContext
    {
        public OrmtestContext()
        {
        }

        public OrmtestContext(DbContextOptions<OrmtestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dtoes> Dtoes { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<TestData> TestData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(local);Database=Ormtest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dtoes>(entity =>
            {
                entity.HasKey(e => e.IntData);

                entity.Property(e => e.DateData).HasColumnType("datetime");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<TestData>(entity =>
            {
                entity.HasKey(e => e.IntData);

                entity.Property(e => e.DateData).HasColumnType("datetime");

                entity.Property(e => e.StringData)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
