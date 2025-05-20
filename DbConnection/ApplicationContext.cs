using DbConnection.Models;
using Microsoft.EntityFrameworkCore;

namespace DbConnection
{

    public class ApplicationContext : DbContext
    {
        public DbSet<Box> Boxes => Set<Box>();
        public DbSet<Pallet> Pallets => Set<Pallet>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=warehouse;Username=user;Password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Box>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Pallet>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity
                    .HasMany(e => e.Boxes)
                    .WithOne(e => e.Pallet)
                    .HasForeignKey(e => e.PalletId)
                    .IsRequired();
            });
        }
    }
}
