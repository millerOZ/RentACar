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
        public String PlaceFinishReserve { get; set; }
        [Display(Name = "Estado")]
        public Boolean StartReserve { get; set; }
    }
}
