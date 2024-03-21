using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace RentARoom.Models.Reservations
{
    public interface IDeleteReservationRepository
    {
        Task<DeleteReservationOutputSP> DeleteReservation(DeleteReservationInputSP input);

    }
}
