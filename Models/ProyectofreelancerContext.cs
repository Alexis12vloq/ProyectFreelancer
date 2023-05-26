using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectFreeelancer.Models;

public partial class ProyectofreelancerContext : DbContext
{
    public ProyectofreelancerContext()
    {
    }

    public ProyectofreelancerContext(DbContextOptions<ProyectofreelancerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Recibo> Recibos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-R42C73H0; DataBase=PROYECTOFREELANCER;TrustServerCertificate=True; Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recibo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recibo__3213E83F5E01367F");

            entity.ToTable("Recibo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("logo");
            entity.Property(e => e.MontoCobrar)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto_cobrar");
            entity.Property(e => e.NombresCompletos)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombres_completos");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_documento");
            entity.Property(e => e.PdfRecibo)
                .HasColumnType("text")
                .HasColumnName("pdf_recibo");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");
            entity.Property(e => e.TipoMoneda)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_moneda");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
