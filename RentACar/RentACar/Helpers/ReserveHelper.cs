using Microsoft.EntityFrameworkCore;
using RentACar.Common;
using RentACar.Data;
using RentACar.Data.Entities;
using RentACar.Enums;
using RentACar.Models;

namespace RentACar.Helpers
{
    public class ReserveHelper: IReserveHelper
    {

        private readonly DataContext _context;

        public ReserveHelper(DataContext context)
        {
            _context = context;
        }


        public async Task<Response> ProcessReserveAsync(ReserveViewModel model)
        {

            Reserve reserve = new()
            {
                Date = DateTime.UtcNow,
                User = await _context.Users.FindAsync(model.User.Id),
                Comments = model.Comments,
                DeliveryDate = model.DeliveryDate,
                ReturnDate = model.ReturnDate,
                ReserveDetails = new List<ReserveDetail>(),
                ReserveStatus = ReserveStatus.Nuevo
            };

            reserve.ReserveDetails.Add(new ReserveDetail
            {
                Vehicle = await _context.Vehicles.FindAsync(model.Vehicle.Id),
                DeliveryDate = model.DeliveryDate,
                ReturnDate = model.ReturnDate,
                Comments = model.Comments
            });

            Vehicle vehicle = await _context.Vehicles.FindAsync(model.Vehicle.Id);
            if (vehicle != null)
            {
                vehicle.VehicleStatus = VehicleStatus.Reservado;
                _context.Vehicles.Update(vehicle);
            }


            _context.Reserves.Add(reserve);
            await _context.SaveChangesAsync();
            return new Response { IsSuccess = true };
        }

        public async Task<Response> CancelReserveAsync(int id)
        {
            Reserve reserve = await _context.Reserves
                .Include(rd => rd.ReserveDetails)
                .ThenInclude(rv => rv.Vehicle)
                .FirstOrDefaultAsync(s => s.Id == id);

            foreach (ReserveDetail reserveDetail in reserve.ReserveDetails)
            {
                Vehicle vehicle = await _context.Vehicles.FindAsync(reserveDetail.Vehicle.Id);
                if (vehicle != null)
                {
                    vehicle.VehicleStatus = VehicleStatus.Diponible;
                }

            }
            reserve.ReserveStatus = ReserveStatus.Cancelada;
            await _context.SaveChangesAsync();
            return new Response { IsSuccess = true };
        }

        public async Task<Response> FinaliceReserveAsync(int id)
        {
            Reserve reserve = await _context.Reserves
                .Include(rd => rd.ReserveDetails)
                .ThenInclude(rv => rv.Vehicle)
                .FirstOrDefaultAsync(s => s.Id == id);

            foreach (ReserveDetail reserveDetail in reserve.ReserveDetails)
            {
                Vehicle vehicle = await _context.Vehicles.FindAsync(reserveDetail.Vehicle.Id);
                if (vehicle != null)
                {
                    vehicle.VehicleStatus = VehicleStatus.Diponible;
                }

            }
            reserve.ReserveStatus = ReserveStatus.finalizada;
            await _context.SaveChangesAsync();
            return new Response { IsSuccess = true };
        }

    }
}
