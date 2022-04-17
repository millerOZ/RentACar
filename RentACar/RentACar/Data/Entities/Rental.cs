using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Entities
{
    public class Rental
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float TotalValue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PaymentType { get; set; }
        public ICollection<RentalType> RentalTypes { get; set; }
    }
}
