using RentACar.Common;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Helpers
{
    public interface IReserveHelper
    {

        Task<Response> ProcessReserveAsync(ReserveViewModel model);

        Task<Response> CancelReserveAsync(int? id);

    }
}
