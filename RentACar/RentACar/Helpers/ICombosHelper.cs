using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Data.Entities;

namespace RentACar.Helpers
{
    public interface ICombosHelper
    {

        Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync();
        Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync(IEnumerable<Category> filter);
        Task<IEnumerable<SelectListItem>> GetComboDocumentTypesAsync();
        Task<IEnumerable<SelectListItem>> GetComboDocumentTypesAsync(IEnumerable<DocumentType> filter);
        Task<IEnumerable<SelectListItem>> GetComboLicenceTypesAsync();
        Task<IEnumerable<SelectListItem>> GetComboLicenceTypesAsync(IEnumerable<DocumentType> filter);


    }
}
