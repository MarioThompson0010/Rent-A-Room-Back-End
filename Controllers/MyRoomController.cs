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
        private readonly IMyRoomRepository _context;

        public MyRoomController(IMyRoomRepository context)
        {
            _context = context;
        }

        // GET: api/MyClients
        [HttpGet]
        public async Task<ActionResult> GetMyRooms()
        {
            var temp = await _context.GetMyRooms();
            return Ok(temp);
        }

        // GET: api/MyClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult/*<MyRoomSub>*/> GetMyRoom(long id)
        {
            var myClient = await _context.GetMyRoom(id);

            
            //var myClient = await _context.MyRooms.FindAsync(id);

            var subClient = new MyRoomSub();
            //subClient.UserName = myClient.UserName;

            if (myClient == null)
            {
                return NotFound();
            }

            subClient.RoomNumber = myClient.RoomNumber;
            subClient.BeingUsed = myClient.BeingUsed;

            return Ok(subClient);
        }

        private async Task<ActionResult<MyRoom>> GetMyRoomFull(long id)
        {
            var myClient = await _context.GetMyRoom(id);


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
        public async Task<ActionResult> PutMyClient(long id,
            MyRoomSub myRoom)
        {
            var gotRoom = this.GetMyRoomFull(id).Result.Value;
            if (id != gotRoom?.Id)
            {
                return BadRequest();
            }

            var temp = await _context.PutMyRoom(id, myRoom);
            //gotClient.RoomId = myClient.RoomId;
            //gotRoom.BeingUsed = temp.BeingUsed;
            //gotRoom.RoomNumber = temp.RoomNumber;
            ////gotClient.name
            //_context.Entry(/*myClient*/gotRoom).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!MyClientExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //var subRoomSaved = this.TranslateFromFullToSub(temp);
            //return NoContent();
            return Ok(temp);
        }

        // POST: api/MyClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostMyRoom(MyRoom myRoom)
        {
            var temp = await _context.PostMyRoom(myRoom);
            //_context.MyRooms.Add(myRoom);
            //await _context.SaveChangesAsync();
            if (temp == null)
            {
                return NoContent();
            }

            return CreatedAtAction("GetMyRoom", new { id = temp.Id }, myRoom);
        }

        // DELETE: api/MyClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyClient(long id)
        {
            var myClient = await _context.DeleteMyRoom(id);
            //var myClient = await _context.MyRooms.FindAsync(id);
            if (myClient == false)
            {
                return NotFound();
            }

            //_context.MyRooms.Remove(myClient);
            //await _context.SaveChangesAsync();

            return Ok(myClient);
        }

        //private bool MyClientExists(long id)
        //{
        //    return _context.MyRooms.Any(e => e.Id == id);
        //}

        private MyRoomSub TranslateFromFullToSub(MyRoom myRoom)
         => new MyRoomSub { RoomNumber = myRoom.RoomNumber, BeingUsed = myRoom.BeingUsed};
    }
}
