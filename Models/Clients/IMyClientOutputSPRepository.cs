﻿using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace RentARoom.Models.Clients
{
    public interface IMyClientOutputSPRepository
    {
        Task<IEnumerable<MyClientOutputSP>> MyClientOutputSP();

    }
}
