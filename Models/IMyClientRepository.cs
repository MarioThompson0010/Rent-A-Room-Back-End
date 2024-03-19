﻿using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace RentARoom.Models
{
	public interface IMyClientRepository
	{
		Task<IEnumerable<MyClient>> GetMyClients();
		Task<MyClientSub> GetMyClient(long id);
		Task<MyClient> GetMyClientFull(long id);

		Task<MyClientSub> PutMyClient(long id, MyClientSub myClient);
		Task<MyClient> PostMyClient(MyClient myClient);
		Task<bool> DeleteMyClient(long id);
	}
}
