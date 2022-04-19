using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class RentalViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre Cliente")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }
        public float TotalValue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ReserveId { get; set; }
    }
}
