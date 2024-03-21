using CommandLineEF.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RentARoom.Models.Clients
{
    public class MyClientRepository : IMyClientRepository
    {
        private readonly AppDbContext appDbContext;

        public MyClientRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<MyClient>> GetMyClients()
        {
            return await appDbContext.MyClients.ToListAsync();
        }

        public async Task<MyClientSub> GetMyClient(long id)
        {
            var myClient = await appDbContext.MyClients.FindAsync(id);



            var subClient = new MyClientSub();
            subClient.Phone = myClient?.Phone;
            subClient.Email = myClient?.Email;
            subClient.FirstName = myClient?.FirstName;
            subClient.LastName = myClient?.LastName;


            return subClient;
        }

        public async Task<MyClient> GetMyClientFull(long id)
        {
            var myClient = await appDbContext.MyClients.FindAsync(id);
            if (myClient == null)
            {
                return new MyClient();
            }


            return myClient;
        }

        public async Task<MyClientSub> PutMyClient(long id, MyClientSub myClient)
        {

            var gotClient = GetMyClientFull(id).Result;

            gotClient.Email = myClient?.Email;
            gotClient.LastName = myClient?.LastName;
            gotClient.FirstName = myClient?.FirstName;
            gotClient.Phone = myClient?.Phone;

            appDbContext.Entry(gotClient).State = EntityState.Modified;

            try
            {
                await appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyClientsController.MyClientExists(id, appDbContext))
                {
                    //to do
                }
                else
                {
                    // to do
                }
            }

            var subclientSaved = MyClientsController.TranslateFromFullToSub(gotClient);
            //return NoContent();
            return subclientSaved;
        }

        public async Task<MyClient> PostMyClient(MyClient myClient)
        {
            var result = await appDbContext.MyClients.AddAsync(myClient);
            try
            {
                await appDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var mess = ex.Message;
                throw;
            }

            return result.Entity;//  CreatedAtAction("GetMyClient", new { id = myClient.Id }, myClient);
        }

        public async Task<bool> DeleteMyClient(long id)
        {
            var myClient = await appDbContext.MyClients.FindAsync(id);
            if (myClient == null)
            {
                return false;// new MyClient();// null;// NotFound();
            }

            appDbContext.MyClients.Remove(myClient);
            await appDbContext.SaveChangesAsync();

            return true;// new MyClient();
        }

        public async Task<IEnumerable<MyClientOutputSP>> MyClientOutputSP()
        {
            string storedProc = $"exec GetClientSP ";

            try
            {
                var resultgood = await appDbContext.MyClientOutputSPs.FromSqlRaw(storedProc).ToListAsync();
                return resultgood;

            }
            catch (Exception)
            {
                List<MyClientOutputSP>? listError = new List<MyClientOutputSP>();
                MyClientOutputSP error;
                error = new MyClientOutputSP
                {
                    Email = null
                };

                listError.Add(error);
                return listError;

                //return new BadRequestObjectResult(ex.Message);

                //return NoContent();
            }
        }

    }
}
