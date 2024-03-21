using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentARoom.Models;
using RentARoom.Models.Clients;

namespace CommandLineEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyClientsController : ControllerBase
    {
		private readonly IMyClientRepository myClientRepository;

        //public EmployeesController(IEmployeeRepository employeeRepository)
        //{
        //	this.employeeRepository = employeeRepository;
        //}
        //private readonly AirBbContext _context;

        public MyClientsController(/*AirBbContext*/IMyClientRepository context)
        {
            //_context = context;
            myClientRepository = context;

		}

        // GET: api/MyClients
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<MyClient>>> GetMyClients()
        public async Task<ActionResult> GetMyClients()
        {
            //return await _context.MyClients.ToListAsync();
            var temp = await myClientRepository.GetMyClients();

			return Ok(temp);
        }

        // GET: api/MyClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult/*<MyClientSub>*/> GetMyClient(long id)
        {
            var myClient = await this.myClientRepository.GetMyClient(id);//.FindAsync(id);

            
            if (myClient == null)
            {
                return NotFound();
            }

            //var subClient = new MyClientSub();
            //subClient.Phone = myClient.Phone;
            //subClient.Email = myClient.Email;
            //subClient.FirstName = myClient.FirstName;
            //subClient.LastName = myClient.LastName;


            return Ok(myClient);
        }

        private async Task<ActionResult/*<MyClient>*/> GetMyClientFull(long id)
        {
            var myClient = await myClientRepository.GetMyClientFull(id);// MyClients.FindAsync(id);

            //var subClient = new MyClientSub();
            //subClient.UserName = myClient.UserName;
            //subClient.Room = myClient.Room;

            if (myClient.Id == 0)
            {
                return NotFound();
            }

            //return myClient;
            return Ok(myClient);
        }

        // PUT: api/MyClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult>/*Task<ActionResult<MyClientSub>>*/ PutMyClient(long id,
            MyClientSub myClient)
        {
            var gotClient = myClientRepository.GetMyClientFull(id).Result;//.Value;
            if (id != gotClient?.Id)
            {
                return BadRequest();
            }

            //gotClient.Email = myClient.Email;
            //gotClient.LastName = myClient.LastName;
            //gotClient.FirstName = myClient.FirstName;
            //gotClient.Phone = myClient.Phone;
           
            //_context.Entry(/*myClient*/gotClient).State = EntityState.Modified;

            try
            {
                await myClientRepository.PutMyClient(id, myClient);// SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandLineEF.Controllers.MyClientsController.MyClientExists(id, (DbContext) myClientRepository ))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var subclientSaved = MyClientsController.TranslateFromFullToSub(gotClient);
            //return NoContent();
            return Ok(subclientSaved);
        }

        // POST: api/MyClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult/*<MyClient>*/> PostMyClient(MyClient myClient)
        {
            var gotclient = await myClientRepository.PostMyClient(myClient);// GetMyClients();
             //.Add(myClient);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetMyClient", new { id = gotclient.Id }, gotclient);
        }

        // DELETE: api/MyClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyClient(long id)
        {
            var myClient = await myClientRepository.GetMyClientFull(id);// _context.MyClients.FindAsync(id);
            if (myClient == null)
            {
                return NotFound();
            }

            await myClientRepository.DeleteMyClient(id);
            //_context.MyClients.Remove(myClient);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        public static bool MyClientExists(long id, DbContext _context)
        {
            var temp = (AppDbContext)_context;
            return temp.MyClients.Any(e => e.Id == id);
        }

        public static MyClientSub TranslateFromFullToSub(MyClient myClient)
         => new MyClientSub {  Email = myClient.Email, 
         FirstName = myClient.FirstName, LastName = myClient.LastName, Phone = myClient.Phone
         };
    }
}
