using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sistemaLibros.Models;

public partial class ConstruTechContext : DbContext
{
    public ConstruTechContext()
    {
    }

    public ConstruTechContext(DbContextOptions<ConstruTechContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=constru-tech;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCli).HasName("PK__clientes__398F6705970FD937");

            entity.ToTable("clientes");

            entity.Property(e => e.IdCli).HasColumnName("idCli");
            entity.Property(e => e.Cedula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaNac)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fecha_nac");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__libro__3213E83F9DE40B42");

            entity.ToTable("libro");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Autor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("autor");
            entity.Property(e => e.Fechapublicacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fechapublicacion");
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__prestamo__3213E83FA88B6601");

            entity.ToTable("prestamo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fechafin)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fechafin");
            entity.Property(e => e.Fechainicio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fechainicio");
            entity.Property(e => e.Idlibro).HasColumnName("idlibro");
            entity.Property(e => e.Iduser).HasColumnName("iduser");

            entity.HasOne(d => d.IdlibroNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Idlibro)
                .HasConstraintName("FK__prestamo__idlibr__5812160E");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK__prestamo__iduser__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
