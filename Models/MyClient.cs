using System;
using System.Collections.Generic;

namespace RentARoom.Models;

public partial class MyClient
{
    public long Id { get; set; }

    

    public string? Pword { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    //public int? RoomId { get; set; }
}
public class MyClientSub
{

    
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    
}
