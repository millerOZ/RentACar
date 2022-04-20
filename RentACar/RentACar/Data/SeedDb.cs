using Microsoft.EntityFrameworkCore;
using RentACar.Data.Entities;
using RentACar.Helpers;

namespace RentACar.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;


        public SeedDb(DataContext context, IBlobHelper blobHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckReservesAsync();
            await CheckVehiclesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Sport" });
                _context.Categories.Add(new Category { Name = "Off-Road" });
                _context.Categories.Add(new Category { Name = "Urbanos" });
                _context.Categories.Add(new Category { Name = "Carros" });
                _context.Categories.Add(new Category { Name = "Motos" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckVehiclesAsync()
        {
            if (!_context.Vehicles.Any())
            {
                await AddVehicleAsync("ZTS-01G", "TVS", "APACHE RT 160 4v - 2023", "Experimenta su tecnología Smart Xconnect y disfruta su ecosistema de conexión," +
                    " telemetría en ruta, alerta y asistencia en navegación.", new List<string> { "Motos", "Sport", "Urbanos" }, new List<string> { "Apache160.png", "Apache_160.png" });

                await AddVehicleAsync("QRT-91G", "YAMAHA", "MT-09 - 2023", "Transmisión constante de 6 velocidades, Full Inyeccion," +
                    " 890cc", new List<string> { "Motos", "Sport" }, new List<string> { "mt-09.jfif", "mt-09_2.jfif" });

                await AddVehicleAsync("TGH-903", "MAZDA", "CX-5 - 2023", "Sistema Inteligente de tracción AWD con Función Off-Road, Diseñada para" +
                    " maximizar tu comodidad. permite selección de diferentes modos de conducción, Sport, Off-Road.", new List<string> { "Carros", "Sport", "Off-Road" }, new List<string> { "Cx-5.jfif", "CX-5_2.jfif" });

            }
        }

        private async Task AddVehicleAsync(string plaque, string brand, string serie, string remarks, List<string> categories, List<string> images)
        {
            Vehicle vehicle = new()
            {
                Plaque = plaque,
                Brand = brand,
                Serie = serie,
                Remarks = remarks,
                VehicleCategories = new List<VehicleCategory>(),
                ImageVehicles = new List<ImageVehicle>()

            };

            foreach (string category in categories)
            {
                vehicle.VehicleCategories.Add(new VehicleCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }

            foreach (string image in images)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\vehicles\\{image}", "vehicles");
                vehicle.ImageVehicles.Add(new ImageVehicle { ImageId = imageId });
            }

            _context.Vehicles.Add(vehicle);
        }

        private async Task CheckReservesAsync()
        {
            if (!_context.Reserves.Any())
            {
                _context.Reserves.Add(new Reserve
                {
                    DateReserve = new System.DateTime(2015, 3, 10, 2, 15, 10),
                    DateStartReserve = new System.DateTime(2015, 3, 10, 2, 15, 10),
                    DateFinishReserve = new System.DateTime(2015, 3, 10, 2, 15, 10),
                    PlaceFinishReserve = "Floresta",
                    StartReserve = true,
                    Rentals = new List<Rental>()
                    {
                        new Rental
                        {
                            Name = "mILLER",
                            Quantity = 1212,
                            TotalValue = 122,
                            PaymentType = "tajerta",
                            RentalTypes = new List<RentalType>()
                            {
                                new RentalType {Description =" por kilometros "},
                            }
                        },
                        new Rental
                        {   Name = "henry",
                            Quantity = 222,
                            TotalValue = 22,
                            PaymentType = "efectivo",
                            RentalTypes = new List<RentalType>()
                            {
                                new RentalType {Description ="Por tiempo"},
                            }
                        }
                    }
                });

                await _context.SaveChangesAsync();
            }

            
        }
    }
}
