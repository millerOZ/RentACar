﻿using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        [Display(Name = "Nombre Cliente")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float TotalValue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PaymentType { get; set; }
        public ICollection<RentalType> RentalTypes { get; set; }

        public Reserve Reserve { get; set; }
    }
}