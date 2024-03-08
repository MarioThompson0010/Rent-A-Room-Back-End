namespace RentARoom.Models
{
    public class MyReservation
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long RoomId { get; set; }

    }

    public class MyReservationSub
    {
        public long ClientId { get; set; }
        public long RoomId { get; set; }

    }
}
