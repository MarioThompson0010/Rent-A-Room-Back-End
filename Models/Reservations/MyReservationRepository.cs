using CommandLineEF.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RentARoom.Models.Reservations
{
    public class MyReservationRepository : IMyReservationRepository
    {
        private readonly AppDbContext appDbContext;

        public MyReservationRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<MyReservation>> GetMyReservations()
        {
            return await appDbContext.MyReservations.ToListAsync();
        }

        public async Task<MyReservationSub> GetMyReservation(long id)
        {
            var myRes = await appDbContext.MyReservations.FindAsync(id);


            if (myRes == null)
            {
                return new MyReservationSub();// null; // new MyClientSub();
            }

            var subRes = new MyReservationSub();
            subRes.ClientId = myRes.ClientId;
            subRes.RoomId = myRes.RoomId;
            //subRes.FirstName = myRes?.FirstName;
            //subRes.LastName = myRes?.LastName;


            return subRes;
        }

        public async Task<MyReservation> GetMyReservationFull(long id)
        {
            var myRes = await appDbContext.MyReservations.FindAsync(id);
            if (myRes == null)
            {
                return new MyReservation();// MyClient();
            }
            //var subClient = new MyClientSub();
            //subClient.UserName = myClient.UserName;
            //subClient.Room = myClient.Room;

            //if (myClient == null)
            //{
            //	return null;// NotFound();
            //}

            //return myClient;
            return myRes;
        }

        public async Task<MyReservationSub> PutMyReservation(long id, MyReservationSub myClient)
        {

            var gotRes = appDbContext.MyReservations.FindAsync(id).Result;// .GetMyClientFull(id).Result;
            if (gotRes == null)
            {
                return new MyReservationSub();// BadRequest();
            }

            gotRes.ClientId = myClient.ClientId;
            gotRes.RoomId = myClient.RoomId;// LastName;
                                            //gotRes.FirstName = myClient?.FirstName;
                                            //gotRes.Phone = myClient?.Phone;

            appDbContext.Entry(gotRes).State = EntityState.Modified;

            try
            {
                await appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyReservationController.MyReservationExists(id, appDbContext))
                {
                    //return null;// NotFound();
                }
                else
                {
                    throw;
                }
            }

            var subclientSaved = MyReservationController.TranslateFromFullToSub(gotRes);
            //return NoContent();
            return subclientSaved;
        }

		public async Task<IEnumerable<MakeReservationOutputSP>> MakeReservation(MakeReservationInputSP input)
		{
			string storedProc = $"exec MakeReservation " +
				$"@Email='{input.Email}', @Phone={(input.Phone == null ? "null" : input.Phone)}";

			try
			{
				var resultgood = await appDbContext.MakeReservationOutputSPs.FromSqlRaw(storedProc).ToListAsync();
				return resultgood;

			}
			catch (Exception)
			{
				List<MakeReservationOutputSP>? listError = new List<MakeReservationOutputSP>();
				MakeReservationOutputSP error;
				error = new MakeReservationOutputSP
				{
					Message = "No Room Available",
					ClientId = null,
					BeingUsed = null,
					Email = null,
					FirstName = null,
					LastName = null,
					Phone = null,
					ReservationId = null,
					RoomNumber = null
				};

				listError.Add(error);
				return listError;

				//return new BadRequestObjectResult(ex.Message);

				//return NoContent();
			}
		}

		public async Task<MyReservation> PostMyReservation(MyReservation myRes)
        {
            var result = await appDbContext.MyReservations.AddAsync(myRes);
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

        public async Task<bool> DeleteMyReservation(long id)
        {
            var myClient = await appDbContext.MyReservations.FindAsync(id);
            if (myClient == null)
            {
                return false;// new MyClient();// null;// NotFound();
            }

            appDbContext.MyReservations.Remove(myClient);
            await appDbContext.SaveChangesAsync();

            return true;// new MyClient();
        }

		public async Task<DeleteReservationOutputSP> DeleteReservation(DeleteReservationInputSP input)
		{
			string storedProc = $"exec DeleteReservation " +
				$"@ReservationId={input.ReservationId}";

			try
			{
				var resultgood = await appDbContext.DeleteReservationOutputSPs.FromSqlRaw(storedProc).ToListAsync();
				if (resultgood == null)
				{
					return new DeleteReservationOutputSP
					{
						Message = "Unable to delete reservation",
						ReservationId = null,
						RoomId = null
					};
				}

				var temp = resultgood.FirstOrDefault();
				if (temp == null)
				{
					return new DeleteReservationOutputSP
					{
						Message = "Unable to delete reservation",
						ReservationId = null,
						RoomId = null
					};
				}
				else
				{
					return temp;
				}



			}
			catch (Exception)
			{
				List<DeleteReservationOutputSP>? listError = new List<DeleteReservationOutputSP>();
				DeleteReservationOutputSP error;
				error = new DeleteReservationOutputSP
				{
					Message = "Unable to delete reservation",
					ReservationId = null,
					RoomId = null
				};

				return error;
				//return new BadRequestObjectResult(ex.Message);

				//return NoContent();
			}
		}
	}
}
