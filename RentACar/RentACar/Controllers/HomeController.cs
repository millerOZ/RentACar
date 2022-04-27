using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Entities;
using RentACar.Models;
using System.Diagnostics;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Vehicle>? vehicles = await _context.Vehicles
                .Include(v => v.ImageVehicles)
                .Include(v => v.VehicleCategories)
                .OrderBy(v => v.Plaque)
                .ToListAsync();
            List<VehiclesHomeViewModel> vehiclesHome = new() { new VehiclesHomeViewModel() };
            int i = 1;
            foreach (Vehicle? vehicle in vehicles)
            {
                if (i == 1)
                {
                    vehiclesHome.LastOrDefault().Vehicle1 = vehicle;
                }
                if (i == 2)
                {
                    vehiclesHome.LastOrDefault().Vehicle2 = vehicle;
                }
                if (i == 3)
                {
                    vehiclesHome.LastOrDefault().Vehicle3 = vehicle;
                }
                if (i == 4)
                {
                    vehiclesHome.LastOrDefault().Vehicle4 = vehicle;
                    vehiclesHome.Add(new VehiclesHomeViewModel());
                    i = 0;
                }
                i++;
            }

            return View(vehiclesHome);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}