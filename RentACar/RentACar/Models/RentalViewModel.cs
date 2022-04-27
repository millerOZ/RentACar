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
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }
        [Display(Name = "Valor Total")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float TotalValue { get; set; }
        [Display(Name = "Tipo Pago")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PaymentType { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ReserveId { get; set; }
    }
}
