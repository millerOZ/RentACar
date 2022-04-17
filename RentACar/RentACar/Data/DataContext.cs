using Microsoft.EntityFrameworkCore;
using RentACar.Data.Entities;

namespace RentACar.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Reserve> Reserves { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reserve>().HasIndex(r => r.Id).IsUnique();

        }
    }
}
