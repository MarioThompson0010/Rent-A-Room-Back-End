using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RentARoom.Models;

public partial class MyRoomContext : DbContext
{
    IConfiguration Configuration { get; set; }

    public virtual DbSet<MyRoom> MyRooms { get; set; }

    public MyRoomContext(DbContextOptions<MyRoomContext> options, IConfiguration configuration)
        : base(options)
    {
        this.Configuration = configuration;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyRoom>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("PK__MyClient__3214EC07DBFBE002");

            entity.ToTable("MyRoom");

            //entity.Property(e => e.Pword).HasMaxLength(100).HasColumnName("PWord");
            //entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
