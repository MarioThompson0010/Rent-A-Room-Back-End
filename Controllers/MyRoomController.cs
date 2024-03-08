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
    public class MyRoomController : ControllerBase
    {
        private readonly MyRoomContext _context;

        public MyRoomController(MyRoomContext context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyRoom>>> GetMyRooms()
        {
            return await _context.MyRooms.ToListAsync();
        }

        // GET: api/MyClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyRoomSub>> GetMyRoom(long id)
        {
            var myClient = await _context.MyRooms.FindAsync(id);

            var subClient = new MyRoomSub();
            //subClient.UserName = myClient.UserName;

            if (myClient == null)
            {
                return NotFound();
            }

            return subClient;
        }

        private async Task<ActionResult<MyRoom>> GetMyRoomFull(long id)
        {
            var myClient = await _context.MyRooms.FindAsync(id);

            
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
        public async Task<ActionResult<MyRoomSub>> PutMyClient(long id,
            MyRoomSub myRoom)
        {
            var gotRoom = this.GetMyRoomFull(id).Result.Value;
            if (id != gotRoom?.Id)
            {
                return BadRequest();
            }

            //gotClient.RoomId = myClient.RoomId;
            gotRoom.BeingUsed = myRoom.BeingUsed;
            //gotClient.name
            _context.Entry(/*myClient*/gotRoom).State = EntityState.Modified;

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

            var subRoomSaved = this.TranslateFromFullToSub(gotRoom);
            //return NoContent();
            return subRoomSaved;
        }

        // POST: api/MyClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MyRoom>> PostMyClient(MyRoom myRoom)
        {
            _context.MyRooms.Add(myRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMyRoom", new { id = myRoom.Id }, myRoom);
        }

        // DELETE: api/MyClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyClient(long id)
        {
            var myClient = await _context.MyRooms.FindAsync(id);
            if (myClient == null)
            {
                return NotFound();
            }

            _context.MyRooms.Remove(myClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MyClientExists(long id)
        {
            return _context.MyRooms.Any(e => e.Id == id);
        }

        private MyRoomSub TranslateFromFullToSub(MyRoom myRoom)
         => new MyRoomSub { RoomNumber = myRoom.RoomNumber, BeingUsed = myRoom.BeingUsed};
    }
}
