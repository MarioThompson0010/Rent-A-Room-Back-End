using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentARoom.Models.Clients;

namespace CommandLineEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyClientOutputController : ControllerBase
    {
        private readonly IMyClientOutputSPRepository _context;

        public MyClientOutputController(IMyClientOutputSPRepository context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpPost]
        public async Task<ActionResult> GetMyClientsSP()
        {
            var temp = await _context.MyClientOutputSP();// MyClientOutputSP();//.FromSqlRaw(storedProc).ToListAsync();
            return Ok(temp);
        }

        // GET: api/MyClients/5

    }
}
