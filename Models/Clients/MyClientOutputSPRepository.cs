using CommandLineEF.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RentARoom.Models.Clients
{
    public class MyClientOutputSPRepository : IMyClientOutputSPRepository
    {
        private readonly AppDbContext appDbContext;

        public MyClientOutputSPRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
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
