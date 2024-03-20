using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace RentARoom.Models
{
	public interface IMyReservationRepository
	{
		Task<IEnumerable<MyReservation>> GetMyReservations();
		Task<MyReservationSub> GetMyReservation(long id);
		Task<MyReservation> GetMyReservationFull(long id);

		Task<MyReservationSub> PutMyReservation(long id, MyReservationSub myClient);
		Task<MyReservation> PostMyReservation(MyReservation myRes);
		Task<bool> DeleteMyReservation(long id);
	}
}
