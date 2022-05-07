using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Entities;

namespace RentACar.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync()
        {
            List<SelectListItem> list = await _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            .OrderBy(c => c.Text)
            .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoría...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync(IEnumerable<Category> filter)
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Category> categoriesFiltered = new();
            foreach (Category category in categories)
            {
                if (!filter.Any(c => c.Id == category.Id))
                {
                    categoriesFiltered.Add(category);
                }
            }

            List<SelectListItem> list = categoriesFiltered.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem { Text = "[Seleccione una categoría...", Value = "0" });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboDocumentTypesAsync()
        {
            List<SelectListItem> list = await _context.DocumentTypes.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Seleccione un país...", Value = "0" });
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboDocumentTypesAsync(IEnumerable<DocumentType> filter)
        {
            List<DocumentType> documentTypes = await _context.DocumentTypes.ToListAsync();
            List<DocumentType> documentTypesFiltered = new();
            foreach (DocumentType documentType in documentTypes)
            {
                if (!filter.Any(c => c.Id == documentType.Id))
                {
                    documentTypesFiltered.Add(documentType);
                }
            }

            List<SelectListItem> list = documentTypesFiltered.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem { Text = "[Seleccione un tipo documento...", Value = "0" });
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboLicenceTypesAsync()
        {
            List<SelectListItem> list = await _context.LicenceTypes.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            .OrderBy(c => c.Text)
            .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de licencia...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboLicenceTypesAsync(IEnumerable<DocumentType> filter)
        {
            List<LicenceType> licenceTypes = await _context.LicenceTypes.ToListAsync();
            List<LicenceType> licenceTypesFiltered = new();
            foreach (LicenceType licenceType in licenceTypes)
            {
                if (!filter.Any(c => c.Id == licenceType.Id))
                {
                    licenceTypesFiltered.Add(licenceType);
                }
            }

            List<SelectListItem> list = licenceTypesFiltered.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem { Text = "[Seleccione un tipo de licencia...", Value = "0" });
            return list;
        }
    }
}
