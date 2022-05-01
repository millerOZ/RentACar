﻿using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Entities
{
    public class LicenceType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }
    }
}