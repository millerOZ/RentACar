using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Common;
using RentACar.Data;
using RentACar.Data.Entities;
using RentACar.Enums;
using RentACar.Helpers;
using RentACar.Models;
using System.Diagnostics;

namespace RentACar.Controllers
{

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly IReserveHelper _reserveHelper;


        public HomeController(ILogger<HomeController> logger, IUserHelper userHelper, DataContext context, IReserveHelper reserveHelper)
        {
            _logger = logger;
            _userHelper = userHelper;
            _context = context;
            _reserveHelper = reserveHelper;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "PriceDesc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IQueryable<Vehicle> query = _context.Vehicles
                .Include(v => v.ImageVehicles)
                .Include(vi => vi.VehicleCategories)
                .Where(v => v.VehicleStatus != VehicleStatus.Reservado);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(v => (v.Serie.ToLower().Contains(searchString.ToLower()) ||
                                          v.VehicleCategories.Any(Vc => Vc.Category.Name.ToLower().Contains(searchString.ToLower()))) &&
                                          v.VehicleStatus != VehicleStatus.Reservado);
            }
            else
            {
                query = query.Where(v => v.VehicleStatus != VehicleStatus.Reservado);
            }

            switch (sortOrder)
            {
                case "NameDesc":
                    query = query.OrderByDescending(p => p.Serie);
                    break;
                case "Price":
                    query = query.OrderBy(p => p.PriceDay);
                    break;
                case "PriceDesc":
                    query = query.OrderByDescending(p => p.PriceDay);
                    break;
                default:
                    query = query.OrderBy(p => p.Serie);
                    break;
            }

            int pageSize = 8;

            HomeViewModel model = new()
            {
                Vehicles = await PaginatedList<Vehicle>.CreateAsync(query, pageNumber ?? 1, pageSize),
                Categories = await _context.Categories.ToListAsync(),

            };


            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        /*Error*/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }


        // GET: Reserves/Create
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            Reserve reserve = new()
            {
                User = user
            };

            _context.Reserves.Add(reserve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

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

            string categories = string.Empty;
            foreach (VehicleCategory category in vehicle.VehicleCategories)
            {
                categories += $"{category.Category.Name}, ";
            }
            categories = categories.Substring(0, categories.Length - 2);

            AddVehicleToReserveViewModel model = new()
            {
                Categories = categories,
                Description = vehicle.Description,
                Id = vehicle.Id,
                Serie = vehicle.Serie,
                Plaque = vehicle.Plaque,
                Brand = vehicle.Brand,
                PriceDay = vehicle.PriceDay,
                ImageVehicles = vehicle.ImageVehicles,
                VehicleStatus = vehicle.VehicleStatus,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(AddVehicleToReserveViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Vehicle vehicle = await _context.Vehicles.FindAsync(model.Id);
            if (vehicle == null)
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            Reserve reserve = new()
            {
                Comments = model.Description,
                User = user
            };

            _context.Reserves.Add(reserve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowReserve));
        }

        [Authorize]
        public async Task<IActionResult> ShowReserve(int? id)
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles
                .Include(v => v.ImageVehicles)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (id == null)
            {
                return NotFound();

            }

            ReserveViewModel model = new()
            {
                User = user,
                Vehicle = vehicle
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowReserve(ReserveViewModel model)
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            Reserve reserve = new()
            {
                DeliveryDate = model.DeliveryDate,
                ReturnDate = model.ReturnDate,
                Comments = model.Comments,
                User = user,
            };

            Response response = await _reserveHelper.ProcessReserveAsync(model);
            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(ReserveSuccess));
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View("Index", "Home");
        }

        [Authorize]
        public IActionResult ReserveSuccess()
        {
            return View();
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reserve reserve = await _context.Reserves.FindAsync(id);
            if (reserve == null)
            {
                return NotFound();
            }

            EditReserveViewModel model = new()
            {
                Id = reserve.Id,
                DeliveryDate = reserve.DeliveryDate,
                ReturnDate = reserve.ReturnDate,
                Comments = reserve.Comments,
                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditReserveViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Reserve reserve = await _context.Reserves.FindAsync(id);
                    reserve.DeliveryDate = model.DeliveryDate;
                    reserve.ReturnDate = model.ReturnDate;
                    reserve.Comments = model.Comments;
                    _context.Update(reserve);
                    await _context.SaveChangesAsync();
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                    return View(model);
                }

                return RedirectToAction(nameof(ShowReserve));
            }

            return View(model);
        }


    }
}