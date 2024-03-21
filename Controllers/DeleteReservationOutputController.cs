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
    public class DeleteReservationOutputController : ControllerBase
    {
        private readonly IDeleteReservationRepository _context;

        public DeleteReservationOutputController(IDeleteReservationRepository context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpPost]
        public async Task<ActionResult<DeleteReservationOutputSP>> DeleteReservation(DeleteReservationInputSP input)
        {

            var temp = await _context.DeleteReservation(input);
            return temp;
            
        }

        // GET: api/MyClients/5

    }
}
