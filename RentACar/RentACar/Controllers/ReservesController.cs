using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Entities;
using RentACar.Enums;
using RentACar.Helpers;
using Vereyon.Web;

namespace RentACar.Controllers
{
    public class ReservesController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;
        private readonly IReserveHelper _reserveHelper;

        public ReservesController(DataContext context, IFlashMessage flashMessage, IReserveHelper reserveHelper)
        {
            _context = context;
            _flashMessage = flashMessage;
            _reserveHelper = reserveHelper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserves
                .Include(s => s.User)
                .Include(s => s.ReserveDetails)
                .ThenInclude(sd => sd.Vehicle)
                .ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reserve reserve = await _context.Reserves
                .Include(s => s.User)
                .Include(s => s.ReserveDetails)
                .ThenInclude(sd => sd.Vehicle)
                .ThenInclude(p => p.ImageVehicles)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Confirm(int? id)
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

            if (reserve.ReserveStatus != ReserveStatus.Nuevo)
            {
                _flashMessage.Danger("Solo se pueden confirmar reservas que estén en estado 'Nueva'.");
            }
            else
            {
                reserve.ReserveStatus = ReserveStatus.Confirmada;
                _context.Reserves.Update(reserve);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("Su reserva ha sido confirada con exito'.");
            }

            return RedirectToAction(nameof(Details), new { Id = reserve.Id });
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Cancel(int? id)
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

            if (reserve.ReserveStatus == ReserveStatus.Cancelada)
            {
                _flashMessage.Danger("No se puede cancelar una reserva que esté en estado 'cancelada'.");
            }
            else
            {
                await _reserveHelper.CancelReserveAsync(reserve.Id);
                _flashMessage.Confirmation("Su reserva ha sido cancelada con exito'.");
            }

            return RedirectToAction(nameof(Details), new { Id = reserve.Id });
        }
        
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CancelUser(int? id)
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

            if (reserve.ReserveStatus == ReserveStatus.Cancelada)
            {
                _flashMessage.Danger("No se puede cancelar una reserva que esté en estado 'cancelada'.");
            }
            else
            {
                await _reserveHelper.CancelReserveAsync(reserve.Id);
                _flashMessage.Confirmation("Su reserva ha sido cancelada con exito'.");
            }

            return RedirectToAction(nameof(MyDetails), new { Id = reserve.Id });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Finalice(int? id)
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

            if (reserve.ReserveStatus != ReserveStatus.Confirmada)
            {
                _flashMessage.Danger("Solo se pueden finalizar reservas en estado confirmada'.");
            }
            else
            {
                await _reserveHelper.FinaliceReserveAsync(reserve.Id);
                _flashMessage.Confirmation("Su reserva ha sido Finalizada con exito'.");
            }

            return RedirectToAction(nameof(Details), new { Id = reserve.Id });
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyReserves()
        {
            return View(await _context.Reserves
                .Include(s => s.User)
                .Include(s => s.ReserveDetails)
                .ThenInclude(sd => sd.Vehicle)
                .Where(s => s.User.UserName == User.Identity.Name)
                .ToListAsync());
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reserve reserve = await _context.Reserves
                .Include(s => s.User)
                .Include(s => s.ReserveDetails)
                .ThenInclude(sd => sd.Vehicle)
                .ThenInclude(p => p.ImageVehicles)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

    }
}
