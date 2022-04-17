using RentACar.Data.Entities;

namespace RentACar.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public SeedDb(DataContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckReservesAsync();
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
                    StartReserve = false,
                    Rentals = new List<Rental>()
                    {
                        new Rental
                        {
                            Quantity = 1212,
                            TotalValue = 122,
                            PaymentType = "tajerta",
                            RentalTypes = new List<RentalType>()
                            {
                                new RentalType {Description =" por kilometros "},
                            }
                        },
                        new Rental
                        {
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
