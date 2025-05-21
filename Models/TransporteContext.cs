namespace MantencionCamiones.Models;
using bodega.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


public partial class TransporteContext : DbContext
{
    public TransporteContext()
    {
    }

    public TransporteContext(DbContextOptions<TransporteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Camione> Camiones { get; set; }

    public virtual DbSet<Mantencione> Mantenciones { get; set; }
    public virtual DbSet<viaje> viaje { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=transporte;user=root;password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Camione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("camiones");

            entity.Property(e => e.Estado).HasColumnType("enum('Disponible','EnViaje','Malo','EnReparación')");
            entity.Property(e => e.Marca).HasMaxLength(50);
            entity.Property(e => e.Modelo).HasMaxLength(50);
        });
        modelBuilder.Entity<viaje>(entity =>
        {
            entity.ToTable("viaje");

            entity.Property(v => v.camion_id).HasColumnName("camion_id");

            entity.HasOne(v => v.camion)
                .WithMany(c => c.Viajes)
                .HasForeignKey(v => v.camion_id);
        });
        modelBuilder.Entity<Mantencione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mantenciones");

            entity.HasIndex(e => e.CamionId, "CamionId");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Observacion).HasColumnType("text");

            entity.HasOne(d => d.Camion).WithMany(p => p.Mantenciones)
                .HasForeignKey(d => d.CamionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mantenciones_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
