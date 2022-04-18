using Microsoft.EntityFrameworkCore;
using RentACar.Data.Entities;


namespace RentACar.Data 
{
    public class RentCarContext : DbContext
    {
       

        public RentCarContext(DbContextOptions<RentCarContext> options) : base(options)
        {
        }

        
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImageVehicle> ImageVehicles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Vehicle>().HasIndex(v => v.Plaque).IsUnique();
            modelBuilder.Entity<VehicleCategory>().HasIndex("VehicleId", "CategoryId").IsUnique();
           

        }


    }
}
