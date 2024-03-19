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
        private readonly MakeReservationOutputSPContext _context;

        

        public MakeReservationOutputController(MakeReservationOutputSPContext context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpPost]
        public async Task<ActionResult<IEnumerable<MakeReservationOutputSP>>> MakeReservation(MakeReservationInputSP input)
        {
            string storedProc = $"exec MakeReservation " +
                $"@Email='{input.Email}', @Phone={(input.Phone == null ? "null" : input.Phone)}";

            try
            {
                var resultgood  = await _context.MakeReservationOutputSPs.FromSqlRaw(storedProc).ToListAsync();
                return resultgood;

            }
            catch (Exception )
            {
                List<MakeReservationOutputSP>? listError = new List<MakeReservationOutputSP>();
                MakeReservationOutputSP error;
                error = new MakeReservationOutputSP
                {
                    Message = "No Room Available",
                    ClientId = null,
                    BeingUsed = null,
                    Email = null,
                    FirstName = null,
                    LastName = null,
                    Phone = null,
                    ReservationId = null,
                    RoomNumber = null
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
