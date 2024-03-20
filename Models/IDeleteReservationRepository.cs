using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace RentARoom.Models
{
	public interface IDeleteReservationRepository
	{
		Task<DeleteReservationOutputSP> DeleteReservation(DeleteReservationInputSP input);
		
	}
}
