using RentACar.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Data.Entities
{
    public class Vehicle
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

        [Display(Name = "Estado")]
        public VehicleStatus VehicleStatus { get; set; }


        [Display(Name = "Placa")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Plaque { get; set; }


        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor Diario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal PriceDay { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }

        public ICollection<VehicleCategory> VehicleCategories { get; set; }

        [Display(Name = "Categorías")]
        public int CategoriesNumber => VehicleCategories == null ? 0 : VehicleCategories.Count;

        public ICollection<ImageVehicle> ImageVehicles { get; set; }

        [Display(Name = "Fotos")]
        public int ImagesNumber => ImageVehicles == null ? 0 : ImageVehicles.Count;

        //TODO: Pending to change to the correct path
        [Display(Name = "Fotos")]
        public string ImageFullPath => ImageVehicles == null || ImageVehicles.Count == 0
            ? $"https://rentacarsproyect.azurewebsites.net/images/noimage.png"
            : ImageVehicles.FirstOrDefault().ImageFullPath;


        public ICollection<ReserveDetail> ReserveDetails { get; set; }

    }
}
