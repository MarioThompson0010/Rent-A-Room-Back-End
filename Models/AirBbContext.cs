using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RentARoom.Models;

public partial class AirBbContext : DbContext
{
    public AirBbContext()
    {
    }

    public AirBbContext(DbContextOptions<AirBbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MyClient> MyClients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MKSSB11\\SQLEXPRESS01;Database=AirBB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MyClient__3214EC07DBFBE002");

            entity.ToTable("MyClient");

            entity.Property(e => e.Pword)
                .HasMaxLength(100)
                .HasColumnName("PWord");
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
