using RentACar.Data.Entities;
using RentACar.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models
{
    public class AddVehicleToReserveViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Marca")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Brand { get; set; }

        [Display(Name = "Estado")]
        public VehicleStatus VehicleStatus { get; set; }

        [Display(Name = "Serie")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Serie { get; set; }

        [Display(Name = "Placa")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Plaque { get; set; }


        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor Diario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal PriceDay { get; set; }

        [Display(Name = "Categorías")]
        public string Categories { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }

        public ICollection<ImageVehicle> ImageVehicles { get; set; }



    }
}
