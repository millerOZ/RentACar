using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentACar.Helpers
{
    public interface ICombosHelper
    {

        Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync();

        //Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync();




    }
}
