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
