using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class RentalTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        public int RentalId { get; set; }
    }
}
