using RentACar.Enums;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<TemporalReserve> TemporalReserves { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        public decimal Value => TemporalReserves == null ? 0 : TemporalReserves.Sum(tr => tr.Value);

    }

}
