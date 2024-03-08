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
    public class MyReservationController : ControllerBase
    {
        private readonly MyReservationContext _context;

        public MyReservationController(MyReservationContext context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyReservation>>> GetMyReservations()
        {
            return await _context.MyReservations.ToListAsync();
        }

        // GET: api/MyClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyReservationSub>> GetMyReservation(long id)
        {
            var myClient = await _context.MyReservations.FindAsync(id);

            if (myClient == null)
            {
                return NotFound();
            }

            var subClient = new MyReservationSub();
            subClient.ClientId = myClient.ClientId;
            subClient.RoomId = myClient.RoomId;

            
            //return myClient;
            return subClient;
        }

        private async Task<ActionResult<MyReservation>> GetMyReservationFull(long id)
        {
            var myClient = await _context.MyReservations.FindAsync(id);


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
        public async Task<ActionResult<MyReservationSub>> PutMyReservation(long id,
            MyReservationSub myRes)
        {
            var gotRoom = this.GetMyReservationFull(id).Result.Value;
            if (id != gotRoom?.Id)
            {
                return BadRequest();
            }

            gotRoom.RoomId = myRes.RoomId;
            gotRoom.ClientId = myRes.ClientId;
            //gotClient.name
            _context.Entry(gotRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.MyReservationExists(id))
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
        public async Task<ActionResult<MyReservation>> PostMyClient(MyReservation myRes)
        {
            _context.MyReservations.Add(myRes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMyReservation", new { id = myRes.Id }, myRes);
        }

        // DELETE: api/MyClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyReservation(long id)
        {
            try
            {
                var myClient = await _context.MyReservations.FindAsync(id);
                if (myClient == null)
                {
                    return NotFound();
                }

                _context.MyReservations.Remove(myClient);
                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    string v = ex.Message;
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }
            

            return NoContent();
        }

        private bool MyReservationExists(long id)
        {
            return _context.MyReservations.Any(e => e.Id == id);
        }

        private MyReservationSub TranslateFromFullToSub(MyReservation myRes)
         => new MyReservationSub { ClientId = myRes.ClientId, RoomId = myRes.RoomId };
    }
}
