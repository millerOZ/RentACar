using RentACar.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class ReserveViewModel
    {

        public User User { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha inicio reserva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha final reserva")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime ReturnDate { get; set; }
        public ICollection<Reserve> Reserves { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        public decimal Value => Reserves == null ? 0 : (decimal)ReturnDate.Subtract(DeliveryDate).TotalDays * Vehicle.PriceDay;   

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string Comments { get; set; }
        public Vehicle Vehicle { get;  set; }
    }
}
