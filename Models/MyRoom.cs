namespace RentARoom.Models
{
    public class MyRoom
    {
        public long Id { get; set; }
        public int RoomNumber { get; set; }
        public bool BeingUsed { get; set; }
    }

    public class MyRoomSub 
    {
        public int RoomNumber { get; set; }
        public bool BeingUsed { get; set; }
    }
}
