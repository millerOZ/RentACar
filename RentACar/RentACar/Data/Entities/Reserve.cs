using RentACar.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Data.Entities
{
    public class Reserve
    {
        public int Id { get; set; }

        public User User { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        [Display(Name = "Estado")]
        public ReserveStatus ReserveStatus { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        public decimal Value => Vehicle == null ? 0 : Vehicle.PriceDay * (decimal)(ReturnDate - DeliveryDate).Days;

        [Display(Name = "vehiculo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Vehicle Vehicle { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string Comments { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha inicio reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DeliveryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha final reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime ReturnDate { get; set; }

    }

}
