#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Entities;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class ReservesController : Controller
    {
        private readonly DataContext _context;

        public ReservesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserves.
                Include(r => r.Rentals).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves
                .Include(r => r.Rentals)
                 .ThenInclude(r => r.RentalTypes) //seconds leves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

        public IActionResult Create()
        {
            Reserve reserve = new() { Rentals = new List<Rental>() };
            return View(reserve);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserve reserve)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    _context.Add(reserve);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Reserva con el mismo nombre de cliente.");
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

            return View(reserve);
        }
        public async Task<IActionResult> AddRental(int? id)
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
            RentalViewModel model = new()
            {
                ReserveId = reserve.Id,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRental(RentalViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Rental rental = new()
                    {
                        RentalTypes = new List<RentalType>(),
                        Reserve = await _context.Reserves.FindAsync(model.ReserveId),
                        Name = model.Name,
                        Quantity = model.Quantity,
                        TotalValue = model.TotalValue,
                        PaymentType = model.PaymentType,
                    };
                    _context.Add(rental);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.ReserveId });

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un alquiler con el mismo nombre de cliente en reserva.");//TOTAL
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

            return View(model);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves.FindAsync(id);
            if (reserve == null)
            {
                return NotFound();
            }
            return View(reserve);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateReserve,DateStartReserve,DateFinishReserve,PlaceFinishReserve,StartReserve")] Reserve reserve)
        {
            if (id != reserve.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveExists(reserve.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reserve);
        }
        public async Task<IActionResult> EditRental(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(s => s.Reserve)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (rental == null)
            {
                return NotFound();
            }
            RentalViewModel model = new()
            {
                ReserveId = rental.Reserve.Id,
                Id = rental.Id,
                Name = rental.Name,
                Quantity = rental.Quantity,
                TotalValue = rental.TotalValue,
                PaymentType = rental.PaymentType,
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRental(int id, RentalViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Rental rental = new()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Quantity = model.Quantity,
                        TotalValue = model.TotalValue,
                        PaymentType = model.PaymentType,
                    };
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.ReserveId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un alquiler con el mismo nombre en esa renta de vehículo.");
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
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserve = await _context.Reserves.FindAsync(id);
            _context.Reserves.Remove(reserve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveExists(int id)
        {
            return _context.Reserves.Any(e => e.Id == id);
        }
    }
}
