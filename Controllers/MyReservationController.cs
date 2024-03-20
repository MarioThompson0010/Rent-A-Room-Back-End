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
        private readonly IMyReservationRepository _context;

        public MyReservationController(IMyReservationRepository context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpGet]
        public async Task<ActionResult> GetMyReservations()
        {
            var temp = await _context.GetMyReservations(); // MyReservations.ToListAsync();
            return Ok(temp);
        }

        // GET: api/MyClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMyReservation(long id)
        {
            var myClient = await _context.GetMyReservation(id);// MyReservations.FindAsync(id);

            if (myClient == null)
            {
                return NotFound();
            }

            var subClient = new MyReservationSub();
            subClient.ClientId = myClient.ClientId;
            subClient.RoomId = myClient.RoomId;

            
            //return myClient;
            return Ok(subClient);
        }

        private async Task<ActionResult> GetMyReservationFull(long id)
        {
            var myClient = await _context.GetMyReservation(id);//.FindAsync(id);


            if (myClient == null)
            {
                return NotFound();
            }

            //return myClient;
            return Ok(myClient);
        }

        // PUT: api/MyClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutMyReservation(long id,
            MyReservationSub myRes)
        {
            var gotRoom = _context.GetMyReservationFull(id).Result;
            if (id != gotRoom?.Id)
            {
                return BadRequest();
            }

            if (gotRoom == null)
            {
				return BadRequest();
			}

            var gotsub = await _context.PutMyReservation(id, myRes);
            //gotRoom.RoomId = myRes.RoomId;
            //gotRoom.ClientId = myRes.ClientId;
            //gotClient.name
            //_context.Entry(gotRoom).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CommandLineEF.Controllers.MyReservationController.MyReservationExists(id,  _context))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //var subRoomSaved = MyReservationController.TranslateFromFullToSub(gotSub);
            //return NoContent();
            return Ok(gotsub);
        }

        // POST: api/MyClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostMyReservation(MyReservation myRes)
        {
            var posted = await _context.PostMyReservation(myRes);
            //_context.MyReservations.Add(myRes);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetMyReservation", new { id = posted.Id }, posted);
        }

        // DELETE: api/MyClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyReservation(long id)
        {
            try
            {
                var myClient = await _context.GetMyReservation(id);// MyReservations.FindAsync(id);
                if (myClient == null)
                {
                    return NotFound();
                }

                var result = await _context.DeleteMyReservation(id);
                //_context.MyReservations.Remove(myClient);
                //try
                //{
                //    await _context.SaveChangesAsync();

                //}
                //catch (Exception ex)
                //{
                //    string v = ex.Message;
                //}
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }
            

            return NoContent();
        }

        public static bool MyReservationExists(long id, AppDbContext _context)
        {
            return _context.MyReservations.Any(e => e.Id == id);
        }

        public static MyReservationSub TranslateFromFullToSub(MyReservation myRes)
         => new MyReservationSub { ClientId = myRes.ClientId, RoomId = myRes.RoomId };
    }
}
