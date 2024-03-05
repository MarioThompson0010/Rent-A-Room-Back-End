using System;
using System.Collections.Generic;

namespace RentARoom.Models;

public partial class MyClient
{
    public long Id { get; set; }

    public string? UserName { get; set; }

    public string? Pword { get; set; }

    public int? Room { get; set; }
}


public  class MyClientSub
{

    public string? UserName { get; set; }


    public int? Room { get; set; }
}
