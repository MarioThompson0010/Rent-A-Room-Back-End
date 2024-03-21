using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentARoom.Models.Reservations;

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

        [HttpPost]
        public async Task<ActionResult> MakeReservation(MakeReservationInputSP input)
        {
            var temp = await _context.MakeReservation(input);
            
                return Ok(temp);
                
                //return new BadRequestObjectResult(ex.Message);

                //return NoContent();
            
        }

		

	}
}
