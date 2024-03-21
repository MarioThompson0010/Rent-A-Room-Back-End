using CommandLineEF.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RentARoom.Models
{
	public class MyClientOutputSPRepository : IMyClientOutputSPRepository
	{
		private readonly AppDbContext appDbContext;

		public MyClientOutputSPRepository(AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
		}

		public async Task<IEnumerable<MyClientOutputSP>> MyClientOutputSP()
		{
			string storedProc = $"exec GetClientSP ";

			try
			{
				var resultgood = await appDbContext.MyClientOutputSPs.FromSqlRaw(storedProc).ToListAsync();
				return resultgood;

			}
			catch (Exception)
			{
				List<MyClientOutputSP>? listError = new List<MyClientOutputSP>();
				MyClientOutputSP error;
				error = new MyClientOutputSP
				{
				  Email = null
				};

				listError.Add(error);
				return listError;

				//return new BadRequestObjectResult(ex.Message);

				//return NoContent();
			}
		}

		//public async Task<DeleteReservationOutputSP> DeleteReservation(DeleteReservationInputSP input)
		//{
		//	string storedProc = $"exec DeleteReservation " +
		//		$"@ReservationId={input.ReservationId}";

		//	try
		//	{
		//		var resultgood = await appDbContext.DeleteReservationOutputSPs.FromSqlRaw(storedProc).ToListAsync();
		//		if (resultgood == null)
		//		{
		//			return new  DeleteReservationOutputSP
		//			{
		//				Message = "Unable to delete reservation",
		//				ReservationId = null,
		//				RoomId = null
		//			};
		//		}

		//		var temp = resultgood.FirstOrDefault();
		//		if (temp == null)
		//		{
		//			return new DeleteReservationOutputSP
		//			{
		//				Message = "Unable to delete reservation",
		//				ReservationId = null,
		//				RoomId = null
		//			};
		//		}
		//		else
		//		{
		//			return temp;
		//		}



		//	}
		//	catch (Exception)
		//	{
		//		List<DeleteReservationOutputSP>? listError = new List<DeleteReservationOutputSP>();
		//		DeleteReservationOutputSP error;
		//		error = new DeleteReservationOutputSP
		//		{
		//			Message = "Unable to delete reservation",
		//			ReservationId = null,
		//			RoomId = null
		//		};

		//		//listError.Add(error);
		//		//return listError;
		//		return error;
		//		//return new BadRequestObjectResult(ex.Message);

		//		//return NoContent();
		//	}
		//}

	}
}
