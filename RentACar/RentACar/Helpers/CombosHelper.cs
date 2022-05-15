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

        public async Task<IEnumerable<SelectListItem>> GetComboDocumenTypeAsync()
        {
            List<SelectListItem> list = await _context.DocumentTypes.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            })
            .OrderBy(d => d.Text)
            .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione tipo de documento...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboLicenceTypesAsync()
        {
            List<SelectListItem> list = await _context.LicenceTypes.Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            })
         .OrderBy(l => l.Text)
         .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione tipo de licencia...]",
                Value = "0"
            });

            return list;
        }
    }
}
