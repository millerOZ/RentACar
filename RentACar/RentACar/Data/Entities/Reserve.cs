using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Entities
{
    public class Reserve
    {
        public int Id { get; set; }
        [Display(Name = "Fecha Reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateReserve { get; set; }
        [Display(Name = "Fecha Inicio Reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStartReserve { get; set; }
        [Display(Name = "Fecha Final Reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateFinishReserve { get; set; }
        [Display(Name = "Lugar Reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        public string PlaceFinishReserve { get; set; }
        [Display(Name = "Estado")]
        public Boolean StartReserve { get; set; }
        [Display(Name = "Alquier")]
        public ICollection<Rental> Rentals { get; set; }
        [Display(Name = "Cant Alquier")]
        public int RentalNumber => Rentals == null ? 0 : Rentals.Count;
    }
}
