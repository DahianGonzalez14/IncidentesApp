using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace incidentes_api.Models
{
    public partial class IncidentesDBContext : DbContext
    {
        public IncidentesDBContext()
        {
        }

        public IncidentesDBContext(DbContextOptions<IncidentesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<HistorialIncidente> HistorialIncidentes { get; set; }
        public virtual DbSet<Incidente> Incidentes { get; set; }
        public virtual DbSet<Prioridad> Prioridads { get; set; }
        public virtual DbSet<Puesto> Puestos { get; set; }
        public virtual DbSet<Sla> Slas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.ToTable("Comentario");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HistorialIncidente>(entity =>
            {
                entity.ToTable("HistorialIncidente");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Estatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Incidente>(entity =>
            {
                entity.ToTable("Incidente");

                entity.Property(e => e.ComentarioCierre)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Estatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Prioridad>(entity =>
            {
                entity.ToTable("Prioridad");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.ToTable("Puesto");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sla>(entity =>
            {
                entity.ToTable("Sla");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
