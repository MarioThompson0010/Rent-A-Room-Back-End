using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RentARoom.Models;

//!!!!
public partial class AirBbContext : /*Identity*/DbContext
{
    IConfiguration Configuration { get; set; }
    
    public AirBbContext(DbContextOptions<AirBbContext> options, IConfiguration configuration)
        : base(options)
    {
        this.Configuration = configuration;
    }

    public virtual DbSet<MyClient> MyClients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MyClient>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("PK__MyClient__3214EC07DBFBE002");

            entity.ToTable("MyClient");

            //entity.Property(e => e.Pword).HasMaxLength(100).HasColumnName("PWord");
            //entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
