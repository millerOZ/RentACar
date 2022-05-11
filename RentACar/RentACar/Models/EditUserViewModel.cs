using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        //[Display(Name = "Tipo de Licencia")]
        //[MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //public string TypeLicence { get; set; }

        [Display(Name = "Licencia")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Licence { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7279/images/Notimage.png"
            : $"https://rentacar13.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Tipo Documento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un tipo documento .")]
        public int DocumentTypeId { get; set; } 
       
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        [Display(Name = "Tipo Licencia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un tipo licencia .")]
        public int LicenceTypeId { get; set; }

        public IEnumerable<SelectListItem> LicenceTypes { get; set; }

    }
}
