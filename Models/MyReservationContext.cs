using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RentARoom.Models;

public partial class MyReservationContext : DbContext
{
    IConfiguration Configuration { get; set; }

    public virtual DbSet<MyReservation> MyReservations { get; set; }

    public MyReservationContext(DbContextOptions<MyReservationContext> options, 
        IConfiguration configuration)
        : base(options)
    {
        this.Configuration = configuration;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyReservation>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("PK__MyClient__3214EC07DBFBE002");

            entity.ToTable("MyReservation");

            //entity.Property(e => e.Pword).HasMaxLength(100).HasColumnName("PWord");
            //entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
