using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Entities
{
    public class RentalType
    {
       public int Id { get; set; }
        [Display(Name = "Descripción")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        public string Description { get; set; }
    }
}
