﻿#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Entities;
using RentACar.Models;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> DetailsRental(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(s => s.Reserve)
                .Include(s => s.RentalTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        public async Task<IActionResult> DetailsRentalType(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RentalType rentalType = await _context.RentalTypes
                .Include(c => c.Rental)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (rentalType == null)
            {
                return NotFound();
            }

            return View(rentalType);
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

        public async Task<IActionResult> AddRentalType(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Rental rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            RentalTypeViewModel model = new()
            {
                RentalId = rental.Id,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRentalType(RentalTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RentalType rentalType = new()
                    {
                        Rental = await _context.Rentals.FindAsync(model.RentalId),
                        Name = model.Name,
                    };
                    _context.Add(rentalType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsRental), new { Id = model.RentalId });

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un tipo alquiler con el mismo nombre en este alquiler.");
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
        public async Task<IActionResult> Edit(int id, Reserve reserve)
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

        public async Task<IActionResult> EditRentalType(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RentalType rentalType = await _context.RentalTypes
                .Include(s => s.Rental)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (rentalType == null)
            {
                return NotFound();
            }
            RentalTypeViewModel model = new()
            {
                RentalId = rentalType.Rental.Id,
                Id = rentalType.Id,
                Name = rentalType.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRentalType(int id, RentalTypeViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    RentalType rentalType = new()
                    {
                        Id = model.Id,
                        Name = model.Name,
                    };
                    _context.Update(rentalType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsRental), new { Id = model.RentalId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un tipo alquiler con el mismo nombre en este alquiler");
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

        public async Task<IActionResult> DeleteRental(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rental rental = await _context.Rentals
                .Include(s => s.Reserve)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        [HttpPost, ActionName("DeleteRental")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRentalConfirmed(int id)
        {
            Rental rental = await _context.Rentals
                .Include(s => s.Reserve)
                .FirstOrDefaultAsync(s => s.Id == id);
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = rental.Reserve.Id });
        }

        public async Task<IActionResult> DeleteRentalType(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RentalType city = await _context.RentalTypes
                .Include(c => c.Rental)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost, ActionName("DeleteRentalType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRentalTypeConfirmed(int id)
        {
            RentalType rentalType = await _context.RentalTypes
                .Include(c => c.Rental)
                .FirstOrDefaultAsync(s => s.Id == id);
            _context.RentalTypes.Remove(rentalType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DetailsRental), new { Id = rentalType.Rental.Id });
        }

        private bool ReserveExists(int id)
        {
            return _context.Reserves.Any(e => e.Id == id);
        }
    }
}
