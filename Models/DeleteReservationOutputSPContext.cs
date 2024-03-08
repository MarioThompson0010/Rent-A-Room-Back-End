using RentARoom.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace RentARoom.Models
{
    public class DeleteReservationOutputSPContext : DbContext
    {
        IConfiguration Configuration { get; set; }

        public virtual DbSet<DeleteReservationOutputSP> DeleteReservationOutputSPs { get; set; }

        

        public DeleteReservationOutputSPContext(DbContextOptions<DeleteReservationOutputSPContext> options,IConfiguration configuration)
           : base(options)
        {
            this.Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeleteReservationOutputSP>(entity =>
            {
                entity.HasNoKey();

                
            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           

        }

    }
}

