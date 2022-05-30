using RentACar.Common;
using RentACar.Data.Entities;

namespace RentACar.Models
{
    public class HomeViewModel
    {
        public PaginatedList<Vehicle> Vehicles { get; set; }

        public ICollection<Category> Categories { get; set; }

        public float Quantity { get; set; }

    }
}
