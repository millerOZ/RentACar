using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class AddVehicleImageViewModel
    {

        public int VehicleId { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public IFormFile ImageFile { get; set; }


    }
}
