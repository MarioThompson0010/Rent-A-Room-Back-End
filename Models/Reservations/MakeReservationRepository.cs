using CommandLineEF.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RentARoom.Models.Reservations
{
    public class MakeReservationRepository : IMakeReservationRepository
    {
        private readonly AppDbContext appDbContext;

        public MakeReservationRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<MakeReservationOutputSP>> MakeReservation(MakeReservationInputSP input)
        {
            string storedProc = $"exec MakeReservation " +
                $"@Email='{input.Email}', @Phone={(input.Phone == null ? "null" : input.Phone)}";

            try
            {
                var resultgood = await appDbContext.MakeReservationOutputSPs.FromSqlRaw(storedProc).ToListAsync();
                return resultgood;

            }
            catch (Exception)
            {
                List<MakeReservationOutputSP>? listError = new List<MakeReservationOutputSP>();
                MakeReservationOutputSP error;
                error = new MakeReservationOutputSP
                {
                    Message = "No Room Available",
                    ClientId = null,
                    BeingUsed = null,
                    Email = null,
                    FirstName = null,
                    LastName = null,
                    Phone = null,
                    ReservationId = null,
                    RoomNumber = null
                };

                listError.Add(error);
                return listError;

                //return new BadRequestObjectResult(ex.Message);

                //return NoContent();
            }
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

                //listError.Add(error);
                //return listError;
                return error;
                //return new BadRequestObjectResult(ex.Message);

                //return NoContent();
            }
        }

    }
}
