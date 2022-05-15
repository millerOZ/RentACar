using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Entities;

namespace RentACar.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImageVehicle> ImageVehicles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<LicenceType> LicenceTypes { get; set; }
        public DbSet<TemporalReserve> TemporalReserves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Vehicle>().HasIndex(v => v.Plaque).IsUnique();
            modelBuilder.Entity<VehicleCategory>().HasIndex("VehicleId", "CategoryId").IsUnique();
            modelBuilder.Entity<DocumentType>().HasIndex(d => d.Name).IsUnique();
            modelBuilder.Entity<LicenceType>().HasIndex(l => l.Name).IsUnique();
            modelBuilder.Entity<TemporalReserve>().HasIndex(tr => tr.Id).IsUnique();


        }
    }
}
