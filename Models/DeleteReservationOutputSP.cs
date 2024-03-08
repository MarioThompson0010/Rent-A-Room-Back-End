namespace RentARoom.Models
{
    public class DeleteReservationOutputSP
    {
        //Select 'Reservation Deleted Successfully'[Message], @ReservationId[ReservationId], @RoomId[RoomId]
        public string? Message { get; set; }
        public long? ReservationId { get; set; }
        public long? RoomId { get; set; }
        
    }
}
