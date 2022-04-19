using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Entities;
using RentACar.Helpers;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;

        public VehiclesController(DataContext context, ICombosHelper combosHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicles
                .Include(v => v.ImageVehicles)
                .Include(v => v.VehicleCategories)
                .ThenInclude(vc => vc.Category)
                .ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            CreateVehicleViewModel model = new()
            {
                Categories = await _combosHelper.GetComboCategoriesAsync(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "vehicles");
                }

                Vehicle vehicle = new()
                {
                    Plaque = model.Plaque,
                    Brand = model.Brand,
                    Serie = model.Serie,
                    Remarks = model.Remarks
                };

                vehicle.VehicleCategories = new List<VehicleCategory>()
                {
                    new VehicleCategory
                    {
                        Category = await _context.Categories.FindAsync(model.CategoryId)
                    }
                };

                if (imageId != Guid.Empty)
                {
                    vehicle.ImageVehicles = new List<ImageVehicle>()
                    {
                        new ImageVehicle { ImageId = imageId }
                    };
                }

                try
                {
                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Vehículo con ña misma Placa.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Categories = await _combosHelper.GetComboCategoriesAsync();
            return View(model);
        }


        [HttpGet] //Edit/Vehicles
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            EditVehicleViewModel model = new()
            {
                Plaque = vehicle.Plaque,
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                Serie = vehicle.Serie,
                Remarks = vehicle.Remarks
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateVehicleViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                Vehicle vehicle = await _context.Vehicles.FindAsync(model.Id);
                vehicle.Plaque = model.Plaque;
                vehicle.Brand = model.Brand;
                vehicle.Serie = model.Serie;
                vehicle.Remarks = model.Remarks;
                _context.Update(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un vehículo con la misma placa.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(model);
        }

        //Details-Vehicles
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles
                .Include(v => v.ImageVehicles)
                .Include(v => v.VehicleCategories)
                .ThenInclude(vc => vc.Category)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }


        // AddImage-Vehicles
        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            AddVehicleImageViewModel model = new()
            {
                VehicleId = vehicle.Id,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(AddVehicleImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "vehicles");
                }

                Vehicle vehicle = await _context.Vehicles.FindAsync(model.VehicleId);
                ImageVehicle imageVehicle = new()
                {
                    Vehicle = vehicle,
                    ImageId = imageId,
                };

                try
                {
                    _context.Add(imageVehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = vehicle.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }


        //Delete Images-Vehicles
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ImageVehicle imageVehicle = await _context.ImageVehicles
                .Include(vi => vi.Vehicle)
                .FirstOrDefaultAsync(vi => vi.Id == id);
            if (imageVehicle == null)
            {
                return NotFound();
            }

            await _blobHelper.DeleteBlobAsync(imageVehicle.ImageId, "vehicles");
            _context.ImageVehicles.Remove(imageVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = imageVehicle.Vehicle.Id });
        }

        //Add Category/Vehicles
        [HttpGet]
        public async Task<IActionResult> AddCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            AddVehicleCategoryViewModel model = new()
            {
                VehicleId = vehicle.Id,
                Categories = await _combosHelper.GetComboCategoriesAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(AddVehicleCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Vehicle vehicle = await _context.Vehicles.FindAsync(model.VehicleId);
                VehicleCategory vehicleCategory = new()
                {
                    Category = await _context.Categories.FindAsync(model.CategoryId),
                    Vehicle = vehicle,
                };

                try
                {
                    _context.Add(vehicleCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = vehicle.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }


        // Delete Category/Vehicle
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehicleCategory vehicleCategory = await _context.VehicleCategories
                .Include(vc => vc.Vehicle)
                .FirstOrDefaultAsync(vc => vc.Id == id);
            if (vehicleCategory == null)
            {
                return NotFound();
            }

            _context.VehicleCategories.Remove(vehicleCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = vehicleCategory.Vehicle.Id });
        }


        //Delete /Vehicles
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles
                .Include(v => v.VehicleCategories)
                .Include(v => v.ImageVehicles)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Vehicle model)
        {
            Vehicle vehicle = await _context.Vehicles
               .Include(v => v.ImageVehicles)
               .Include(v => v.VehicleCategories)
                .FirstOrDefaultAsync(v => v.Id == model.Id);

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            foreach (ImageVehicle imageVehicle in vehicle.ImageVehicles)
            {
                await _blobHelper.DeleteBlobAsync(imageVehicle.ImageId, "vehicles");
            }

            
            return RedirectToAction(nameof(Index));
        }





    }
}
