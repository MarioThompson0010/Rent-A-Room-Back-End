using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RentARoom.Models;

public partial class AppDbContext : DbContext
{
    IConfiguration Configuration { get; set; }

    public virtual DbSet<MyReservation> MyReservations { get; set; }
	public virtual DbSet<MyClient> MyClients { get; set; }
	public virtual DbSet<DeleteReservationOutputSP> DeleteReservationOutputSPs { get; set; }
	public virtual DbSet<MakeReservationOutputSP> MakeReservationOutputSPs { get; set; }
	public virtual DbSet<MyClientOutputSP> MyClientOutputSPs { get; set; }
	public virtual DbSet<MyRoom> MyRooms { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options, 
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

        });

		modelBuilder.Entity<MyClient>(entity =>
		{
			entity.HasKey(e => e.Id);//.HasName("PK__MyClient__3214EC07DBFBE002");
			entity.ToTable("MyClient");
		});

		modelBuilder.Entity<DeleteReservationOutputSP>(entity =>
		{
			entity.HasNoKey();
		});

		modelBuilder.Entity<MakeReservationOutputSP>(entity =>
		{
			entity.HasNoKey();
		});

		modelBuilder.Entity<MyClientOutputSP>(entity =>
		{
			entity.HasNoKey();


		});

		modelBuilder.Entity<MyRoom>(entity =>
		{
			entity.HasKey(e => e.Id);//.HasName("PK__MyClient__3214EC07DBFBE002");

			entity.ToTable("MyRoom");

		});


		OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
