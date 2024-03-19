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
    public class DeleteReservationOutputController : ControllerBase
    {
        private readonly DeleteReservationOutputSPContext _context;

        public DeleteReservationOutputController(DeleteReservationOutputSPContext context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpPost]
        public async Task<ActionResult<IEnumerable<DeleteReservationOutputSP>>> DeleteReservation(DeleteReservationInputSP input)
        {
            string storedProc = $"exec DeleteReservation " +
                $"@ReservationId={input.ReservationId}";

            try
            {
                var resultgood  = await _context.DeleteReservationOutputSPs.FromSqlRaw(storedProc).ToListAsync();
                return resultgood;

            }
            catch (Exception )
            {
                List<DeleteReservationOutputSP>? listError = new List<DeleteReservationOutputSP>();
                DeleteReservationOutputSP error;
                error = new DeleteReservationOutputSP
                {
                    Message = "Unable to delete reservation",
                    ReservationId = null,
                    RoomId = null
                };

                listError.Add(error);
                return listError;
                
                //return new BadRequestObjectResult(ex.Message);

                //return NoContent();
            }
        }

        // GET: api/MyClients/5

    }
}
