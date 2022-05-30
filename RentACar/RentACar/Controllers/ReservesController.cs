using Microsoft.AspNetCore.Mvc;
using RentACar.Data;

namespace RentACar.Controllers
{
    public class ReservesController : Controller
    {
        private readonly DataContext _dataContext;

        public ReservesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        

    }
}
