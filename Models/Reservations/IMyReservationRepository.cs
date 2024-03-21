using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace RentARoom.Models.Reservations
{
    public interface IMyReservationRepository
    {
        Task<IEnumerable<MyReservation>> GetMyReservations();
        Task<MyReservationSub> GetMyReservation(long id);
        Task<MyReservation> GetMyReservationFull(long id);

        Task<MyReservationSub> PutMyReservation(long id, MyReservationSub myClient); // don't use
		Task<IEnumerable<MakeReservationOutputSP>> MakeReservation(MakeReservationInputSP input);
		Task<MyReservation> PostMyReservation(MyReservation myRes);
        Task<bool> DeleteMyReservation(long id); // don't use this one
		Task<DeleteReservationOutputSP> DeleteReservation(DeleteReservationInputSP input);
	}
}
