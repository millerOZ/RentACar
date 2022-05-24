using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Helpers;

namespace RentACar.Controllers
{
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
            ViewBag.ProductsCount = _context.Reserves.Count();
            //ViewBag.NewOrdersCount = _context.Sales.Where(o => o.OrderStatus == OrderStatus.Nuevo).Count();
            //ViewBag.ConfirmedOrdersCount = _context.Sales.Where(o => o.OrderStatus == OrderStatus.Confirmado).Count();

            return View(await _context.Users
                 .Include(d => d.DocumentType)
                 .Include(l => l.LicenceType)
                 .ToListAsync());
        }

    }
}
