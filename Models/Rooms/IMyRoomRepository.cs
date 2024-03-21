using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace RentARoom.Models.Rooms
{
    public interface IMyRoomRepository
    {
        Task<IEnumerable<MyRoom>> GetMyRooms();
        Task<MyRoom> GetMyRoom(long id);
        //Task<MyRoom> GetMyRoomFull(long id);

        Task<MyRoomSub> PutMyRoom(long id, MyRoomSub myRoom);
        Task<MyRoom> PostMyRoom(MyRoom myRoom);
        Task<bool> DeleteMyRoom(long id);
    }
}
