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
    }
}
