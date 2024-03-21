﻿using CommandLineEF.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RentARoom.Models.Reservations
{
    public class DeleteReservationRepository : IDeleteReservationRepository
    {
        private readonly AppDbContext appDbContext;

        public DeleteReservationRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<DeleteReservationOutputSP> DeleteReservation(DeleteReservationInputSP input)
        {
            string storedProc = $"exec DeleteReservation " +
                $"@ReservationId={input.ReservationId}";

            try
            {
                var resultgood = await appDbContext.DeleteReservationOutputSPs.FromSqlRaw(storedProc).ToListAsync();
                if (resultgood == null)
                {
                    return new DeleteReservationOutputSP
                    {
                        Message = "Unable to delete reservation",
                        ReservationId = null,
                        RoomId = null
                    };
                }

                var temp = resultgood.FirstOrDefault();
                if (temp == null)
                {
                    return new DeleteReservationOutputSP
                    {
                        Message = "Unable to delete reservation",
                        ReservationId = null,
                        RoomId = null
                    };
                }
                else
                {
                    return temp;
                }



            }
            catch (Exception)
            {
                List<DeleteReservationOutputSP>? listError = new List<DeleteReservationOutputSP>();
                DeleteReservationOutputSP error;
                error = new DeleteReservationOutputSP
                {
                    Message = "Unable to delete reservation",
                    ReservationId = null,
                    RoomId = null
                };

                return error;
                //return new BadRequestObjectResult(ex.Message);

                //return NoContent();
            }
        }

    }
}
