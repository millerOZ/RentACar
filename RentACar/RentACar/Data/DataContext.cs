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
        public DbSet<Reserve> Reserves { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalType> RentalTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reserve>().HasIndex(r => r.Id).IsUnique();
            modelBuilder.Entity<Rental>().HasIndex("Name", "ReserveId").IsUnique();
            modelBuilder.Entity<RentalType>().HasIndex("Name", "RentalId").IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Vehicle>().HasIndex(v => v.Plaque).IsUnique();
            modelBuilder.Entity<VehicleCategory>().HasIndex("VehicleId", "CategoryId").IsUnique();

        }
    }
}
