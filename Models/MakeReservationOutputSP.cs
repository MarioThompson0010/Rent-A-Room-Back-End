namespace RentARoom.Models
{
    public class MakeReservationOutputSP
    {
        public string? Message { get; set; }
        public long? ClientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? BeingUsed { get; set; }
        public int? RoomNumber { get; set; }
        public long? ReservationId { get; set; }
        //     Select 'Good Insert By Email', mc.Id[ClientId], mc.FirstName, 
        //mc.LastName, mc.Phone, mc.Email, 
        //mr.BeingUsed, mr.RoomNumber, mres.Id[ReservationId]
    }
}
