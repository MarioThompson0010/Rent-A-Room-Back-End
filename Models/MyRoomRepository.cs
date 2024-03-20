using CommandLineEF.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RentARoom.Models
{
	public class MyRoomRepository : IMyRoomRepository
	{
		private readonly AppDbContext appDbContext;

		public MyRoomRepository(AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
		}

		public async Task<IEnumerable<MyRoom>> GetMyRooms()
		{
			return await appDbContext.MyRooms.ToListAsync();
		}

		public async Task<MyRoom> GetMyRoom(long id)
		{
			var myRoom = await appDbContext.MyRooms.FindAsync(id);
			if (myRoom == null)
			{
				return new MyRoom();
			}


			//var subClient = new MyClientSub();
			//subClient.Phone = myClient?.Phone;
			//subClient.Email = myClient?.Email;
			//subClient.FirstName = myClient?.FirstName;
			//subClient.LastName = myClient?.LastName;


			return myRoom;
		}

		//public async Task<MyClient> GetMyClientFull(long id)
		//{
		//	var myClient = await appDbContext.MyClients.FindAsync(id);


		//	return myClient;
		//}

		public async Task<MyRoomSub> PutMyRoom(long id, MyRoomSub myRoom)
		{

			var gotRoom = this.GetMyRoom(id).Result;

			
			gotRoom.RoomNumber = myRoom.RoomNumber;
			gotRoom.BeingUsed = myRoom.BeingUsed;

			appDbContext.Entry(gotRoom).State = EntityState.Modified;

			try
			{
				await appDbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (gotRoom == null)
				{
					MyRoomSub retnull = new MyRoomSub() { RoomNumber = 0 };
					return retnull;
					//return null;// NotFound();
				}
				else
				{
					MyRoomSub retnull = new MyRoomSub() { RoomNumber = 0 };
					return retnull;
				}
			}

			//var subclientSaved = MyClientsController.TranslateFromFullToSub(gotClient);
			MyRoomSub myRoomSub = new MyRoomSub();
			myRoomSub.RoomNumber = gotRoom.RoomNumber;
			myRoomSub.BeingUsed = gotRoom.BeingUsed;

			return myRoomSub;
		}

		public async Task<MyRoom> PostMyRoom(MyRoom myRoom)
		{
			var result = await appDbContext.MyRooms.AddAsync(myRoom);
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

		public async Task<bool> DeleteMyRoom(long id)
		{
			var myClient = await appDbContext.MyRooms.FindAsync(id);
			if (myClient == null)
			{
				return false;// new MyClient();// null;// NotFound();
			}

			appDbContext.MyRooms.Remove(myClient);
			await appDbContext.SaveChangesAsync();

			return true;// new MyClient();
		}
	}
}
