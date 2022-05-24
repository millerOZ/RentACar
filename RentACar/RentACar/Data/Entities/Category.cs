using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Entities
{
    public class Category
    {

        public int Id { get; set; }

        [Display(Name = "Categoría")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        public ICollection<VehicleCategory> VehicleCategories { get; set; }

        [Display(Name = "# vehiculos")]
        public int VehicleNumber => VehicleCategories == null ? 0 : VehicleCategories.Count();
    }
}
