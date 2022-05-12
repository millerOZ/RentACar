using RentACar.Data.Entities;

namespace RentACar.Models
{
    public class HomeViewModel
    {
       
            public ICollection<Vehicle> Vehicles { get; set; }

            public float Quantity { get; set; }
        
    }

}

