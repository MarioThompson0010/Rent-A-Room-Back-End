using RentARoom.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace RentARoom.Models
{
    public class MyClientOutputSPContext: DbContext
    {
        IConfiguration Configuration { get; set; }
        public virtual DbSet<MyClientOutputSP> MyClientOutputSPs { get; set; }

        

        public MyClientOutputSPContext(DbContextOptions<MyClientOutputSPContext> options,IConfiguration configuration)
           : base(options)
        {
            this.Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyClientOutputSP>(entity =>
            {
                entity.HasNoKey();

                
            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MyConnSP" /*"Server=DESKTOP-MKSSB11\\SQLEXPRESS01;Database=AirBB;Trusted_Connection=True;TrustServerCertificate=True"*/));

        }

    }
}

