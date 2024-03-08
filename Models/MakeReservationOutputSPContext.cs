using RentARoom.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace RentARoom.Models
{
    public class MakeReservationOutputSPContext : DbContext
    {
        IConfiguration Configuration { get; set; }

        public virtual DbSet<MakeReservationOutputSP> MakeReservationOutputSPs { get; set; }

        

        public MakeReservationOutputSPContext(DbContextOptions<MakeReservationOutputSPContext> options,IConfiguration configuration)
           : base(options)
        {
            this.Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MakeReservationOutputSP>(entity =>
            {
                entity.HasNoKey();

                
            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           

        }

    }
}

