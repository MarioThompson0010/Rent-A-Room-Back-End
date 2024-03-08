using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentARoom.Models;

namespace CommandLineEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyClientOutputController : ControllerBase
    {
        private readonly MyClientOutputSPContext _context;

        public MyClientOutputController(MyClientOutputSPContext context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpPost]
        public async Task<ActionResult<IEnumerable<MyClientOutputSP>>> GetMyClientsSP()
        {
            string storedProc = "exec GetClientSP";
            return await _context.MyClientOutputSPs.FromSqlRaw(storedProc).ToListAsync();
        }

        // GET: api/MyClients/5

    }
}
