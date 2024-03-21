using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace RentARoom.Models.Reservations
{
    public interface IMakeReservationRepository
    {
        Task<IEnumerable<MakeReservationOutputSP>> MakeReservation(MakeReservationInputSP input);

    }
}
