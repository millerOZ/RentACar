using RentACar.Data.Entities;
using RentACar.Helpers;
using Shooping.Enums;


namespace RentACar.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;

        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckReservesAsync();
            await CheckRolesAsync();
            await CheckCategoriesAsync();
            await CheckVehiclesAsync();
            await CheckUserAsync("prueba@prueba.com", "Luis", "Higuita", "Cedula Ciudadania", "1035442878", "3004340561", "A2", "54124566", UserType.Admin);
            await CheckUserAsync("prueba2@prueba.com", "Eduardo", "Espitia", "Cedula Ciudadania", "1034142878", "3002340561", "A1", "54124566", UserType.User);
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(
             string email,
             string firstName,
             string lastName,
             string documentType,
             string document,
             string phone,
             string typeLicence,
             string licence,
             UserType userType)

        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    DocumentType = documentType,
                    Document = document,
                    PhoneNumber = phone,
                    TypeLicence = typeLicence,
                    Licence = licence,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckVehiclesAsync()
        {
            if (!_context.Vehicles.Any())
            {
                await AddVehicleAsync("ZTS-01G", "TVS", "APACHE RT 160 4v - 2023", "Experimenta su tecnología Smart Xconnect y disfruta su ecosistema de conexión," +
                    " telemetría en ruta, alerta y asistencia en navegación.", new List<string>() { "Motos", "Sport", "Urbanos" }, new List<string>() { "Apache160.png",
                        "Apache_160.png" });

                await AddVehicleAsync("QRT-91G", "YAMAHA", "MT-09 - 2023", "Transmisión constante de 6 velocidades, Full Inyeccion," +
                    " 890cc", new List<string>() { "Motos", "Sport" }, new List<string>() { "mt-09.jfif", "mt-09_2.jfif" });

                await AddVehicleAsync("TGH-903", "MAZDA", "CX-5 - 2023", "Sistema Inteligente de tracción AWD con Función Off-Road, Diseñada para" +
                    " maximizar tu comodidad. permite selección de diferentes modos de conducción, Sport, Off-Road.", new List<string>() { "Carros", "Sport", "Off-Road" },
                    new List<string>() { "Cx-5.jfif", "CX-5_2.jfif" });

                await AddVehicleAsync("BC-01", "MOUNTAIN GEAR", "E-Hawk Aro 27.5", "Batería de Litio 36V 10AH; Tiempo de Carga 4 -6 horas; Distancia " +
                    "40 - 60 Km; Velocidad max 25Km/hr; Pantalla Led", new List<string>() { "Bicicletas", "Electricos", "Urbanos" }, new List<string>() { "BiciE1.png", "BiciE2.png" });

                await AddVehicleAsync("BC-02", "MOUNTAIN GEAR", "MTB Crane Aro 26", "Disco de freno mecanico, 21 cambios, Marco de acero Light Compress, " +
                    "suspensión delantera", new List<string>() { "Bicicletas", "Urbanos" }, new List<string>() { "Bici1.png",
                        "Bici2.png" });

                await AddVehicleAsync("SCO-01", "CERO MOTORS", "Cero E6", "Es plegable para fácil almacenamiento y transporte, cuenta display digital, " +
                    "alcanza velocidad máxima de 25km/hr.", new List<string>() { "Scooter", "Urbanos", "Electricos" }, new List<string>() { "Scoo1.png", "Scoo2.png" });

                await AddVehicleAsync("SCO-02", "KROSSRIDE", "Ace", "cuenta con doble suspensión que hará tu viaje más confortable, Ahorro de combustible" +
                    " Eco Amigable, alcanza velocidad máxima de 55km/hr.", new List<string>() { "Scooter", "Urbanos", "Electricos" }, new List<string>() { "ScooA1.png", "ScooA2.png" });


                await _context.SaveChangesAsync();

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

            foreach (string? category in categories)
            {
                vehicle.VehicleCategories.Add(new VehicleCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }

            foreach (string? image in images)
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
                                new RentalType {Name ="kilometros "},
                            }
                        },
                        new Rental
                        {   Name = "henry",
                            Quantity = 222,
                            TotalValue = 22,
                            PaymentType = "efectivo",
                            RentalTypes = new List<RentalType>()
                            {
                                new RentalType {Name ="tiempo"},
                            }
                        }
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Sport" });
                _context.Categories.Add(new Category { Name = "Off-Road" });
                _context.Categories.Add(new Category { Name = "Urbanos" });
                _context.Categories.Add(new Category { Name = "Carros" });
                _context.Categories.Add(new Category { Name = "Scooter" });
                _context.Categories.Add(new Category { Name = "Electricos" });
                _context.Categories.Add(new Category { Name = "Bicicletas" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
