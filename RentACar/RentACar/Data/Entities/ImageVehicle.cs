using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Entities
{
    public class ImageVehicle
    {
        public int Id { get; set; }

        public Vehicle Vehicle { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7279/Images/NoImage.png"
            : $"https://rentacar1.blob.core.windows.net/vehicles/{ImageId}";
    }
}
