using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class EditVehicleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Marca")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Brand { get; set; }

        [Display(Name = "Serie")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Serie { get; set; }

        [Display(Name = "Placa")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Plaque { get; set; }

        [Display(Name = "Observaciones")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Remarks { get; set; }
    }

}

