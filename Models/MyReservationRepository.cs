using CommandLineEF.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RentARoom.Models
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
				if (!CommandLineEF.Controllers.MyReservationController.MyReservationExists(id,  appDbContext))
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
	}
}
