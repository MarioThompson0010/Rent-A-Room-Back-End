using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentARoom.Models;

namespace CommandLineEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeReservationOutputController : ControllerBase
    {
        private readonly IMakeReservationRepository _context;

        

        public MakeReservationOutputController(IMakeReservationRepository context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpPost]
        public async Task<ActionResult> MakeReservation(MakeReservationInputSP input)
        {
            var temp = await _context.MakeReservation(input);
            //string storedProc = $"exec MakeReservation " +
            //    $"@Email='{input.Email}', @Phone={(input.Phone == null ? "null" : input.Phone)}";

            //try
            //{
            //    var resultgood  = await _context.MakeReservationOutputSPs.FromSqlRaw(storedProc).ToListAsync();
            //    return resultgood;

            //}
            //catch (Exception )
            //{
            //    List<MakeReservationOutputSP>? listError = new List<MakeReservationOutputSP>();
            //    MakeReservationOutputSP error;
            //    error = new MakeReservationOutputSP
            //    {
            //        Message = "No Room Available",
            //        ClientId = null,
            //        BeingUsed = null,
            //        Email = null,
            //        FirstName = null,
            //        LastName = null,
            //        Phone = null,
            //        ReservationId = null,
            //        RoomNumber = null
            //    };

            //    listError.Add(error);
                return Ok(temp);
                
                //return new BadRequestObjectResult(ex.Message);

                //return NoContent();
            
        }

		//[HttpPost]
		//public async Task<ActionResult> DeleteReservation(DeleteReservationInputSP input)
		//{
  //          var temp = await _context.DeleteReservation(input);
  //          return Ok(temp);
		//	//string storedProc = $"exec DeleteReservation " +
		//	//	$"@ReservationId={input.ReservationId}";

		//	//try
		//	//{
		//	//	var resultgood = await _context.DeleteReservationOutputSPs.FromSqlRaw(storedProc).ToListAsync();
		//	//	return resultgood;

		//	//}
		//	//catch (Exception)
		//	//{
		//	//	List<DeleteReservationOutputSP>? listError = new List<DeleteReservationOutputSP>();
		//	//	DeleteReservationOutputSP error;
		//	//	error = new DeleteReservationOutputSP
		//	//	{
		//	//		Message = "Unable to delete reservation",
		//	//		ReservationId = null,
		//	//		RoomId = null
		//	//	};

		//	//	listError.Add(error);
		//	//	return listError;

		//	//	//return new BadRequestObjectResult(ex.Message);

		//	//	//return NoContent();
		//	//}
		//}

		// GET: api/MyClients/5

	}
}
