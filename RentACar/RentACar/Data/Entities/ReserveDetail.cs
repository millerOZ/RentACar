using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Data.Entities
{
    public class ReserveDetail
    {
        public int Id { get; set; }

        public Reserve Reserve { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string Comments { get; set; }

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


        public Vehicle Vehicle { get; set; }

        public decimal Value => Vehicle == null ? 0 : (decimal)ReturnDate.Subtract(DeliveryDate).TotalDays * Vehicle.PriceDay;

        //public decimal Value ()
        //{
        //    decimal result = 0;
        //    TimeSpan Diference = ReturnDate.Subtract(DeliveryDate);
        //    result = (decimal)Diference.Days;

        //    return result * Vehicle.PriceDay;
        //}

    }
}
