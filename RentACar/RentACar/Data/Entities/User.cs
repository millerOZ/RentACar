using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RentACar.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Entities
{
    [Authorize(Roles = "Admin")]
    public class User: IdentityUser
    {

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

        //[Display(Name = "Celular")]
        //[MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //public string Phone { get; set; }

        [Display(Name = "Tipo de Licencia")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public LicenceType LicenceType { get; set; }

        [Display(Name = "Licencia")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Licence { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7279/Images/NotImage.png"
            : $"https://rentacar13.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Usuario")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public Reserve Reserve { get; set; }

        [Display(Name = "Tipo Documento")]
        public DocumentType DocumentType { get; set; }
    }
}
