using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class EditUserViewModel
    {

        public string Id { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string Document { get; set; }

        [Display(Name = "Celular")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Phone { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7279/image/NoImage.png"
            : $"https://rentacar1.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }


        [Display(Name = "Tipo de licencia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un tipo de licencia.")]
        public int? LicenceTypeId { get; set; }

        public IEnumerable<SelectListItem> LicenceTypes { get; set; }


        [Display(Name = "Tipo de documento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar tipo de Documento.")]
        public int? DocumentTypeId { get; set; }

        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
    }
}
