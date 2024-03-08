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
    public class MyClientsController : ControllerBase
    {
        private readonly AirBbContext _context;

        public MyClientsController(AirBbContext context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyClient>>> GetMyClients()
        {
            return await _context.MyClients.ToListAsync();
        }

        // GET: api/MyClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyClientSub>> GetMyClient(long id)
        {
            var myClient = await _context.MyClients.FindAsync(id);

            
            if (myClient == null)
            {
                return NotFound();
            }

            var subClient = new MyClientSub();
            subClient.Phone = myClient.Phone;
            subClient.Email = myClient.Email;
            subClient.FirstName = myClient.FirstName;
            subClient.LastName = myClient.LastName;


            return subClient;
        }

        private async Task<ActionResult<MyClient>> GetMyClientFull(long id)
        {
            var myClient = await _context.MyClients.FindAsync(id);

            //var subClient = new MyClientSub();
            //subClient.UserName = myClient.UserName;
            //subClient.Room = myClient.Room;

            if (myClient == null)
            {
                return NotFound();
            }

            //return myClient;
            return myClient;
        }

        // PUT: api/MyClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async /*Task<IActionResult>*/Task<ActionResult<MyClientSub>> PutMyClient(long id,
            MyClientSub myClient)
        {
            var gotClient =  this.GetMyClientFull(id).Result.Value;
            if (id != gotClient?.Id)
            {
                return BadRequest();
            }

            //gotClient.RoomId = myClient.RoomId;
            gotClient.Email = myClient.Email;
            gotClient.LastName = myClient.LastName;
            gotClient.FirstName = myClient.FirstName;
            gotClient.Phone = myClient.Phone;
            //gotClient.name
            _context.Entry(/*myClient*/gotClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var subclientSaved = this.TranslateFromFullToSub(gotClient);
            //return NoContent();
            return subclientSaved;
        }

        // POST: api/MyClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MyClient>> PostMyClient(MyClient myClient)
        {
            _context.MyClients.Add(myClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMyClient", new { id = myClient.Id }, myClient);
        }

        // DELETE: api/MyClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyClient(long id)
        {
            var myClient = await _context.MyClients.FindAsync(id);
            if (myClient == null)
            {
                return NotFound();
            }

            _context.MyClients.Remove(myClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MyClientExists(long id)
        {
            return _context.MyClients.Any(e => e.Id == id);
        }

        private MyClientSub TranslateFromFullToSub(MyClient myClient)
         => new MyClientSub {  Email = myClient.Email, 
         FirstName = myClient.FirstName, LastName = myClient.LastName, Phone = myClient.Phone
         };
    }
}
