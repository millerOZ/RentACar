using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Enums;
using RentACar.Helpers;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController: Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public DashboardController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.UsersCount = _context.Users.Count();
            ViewBag.ProductsCount = _context.Vehicles.Count();
            ViewBag.ReservesCount = _context.Reserves.Count();
            ViewBag.VehiclesReservesCount = _context.Vehicles.Where(o => o.VehicleStatus == VehicleStatus.Reservado).Count();
            ViewBag.VehiclesDispCount = _context.Vehicles.Where(o => o.VehicleStatus == VehicleStatus.Diponible).Count();
            ViewBag.NewReservesCount = _context.Reserves.Where(o => o.ReserveStatus == ReserveStatus.Nuevo).Count();
            ViewBag.ConfirmeReservesCount = _context.Reserves.Where(o => o.ReserveStatus == ReserveStatus.Confirmada).Count();


            return View(await _context.Users
                 .Include(d => d.DocumentType)
                 .Include(l => l.LicenceType)
                 .ToListAsync());
        }

    }
}
