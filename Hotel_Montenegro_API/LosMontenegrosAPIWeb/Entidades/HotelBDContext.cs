using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LosMontenegrosAPIWeb.Entidades
{
    public partial class HotelBDContext : DbContext
    {
        public HotelBDContext()
        {
        }

        public HotelBDContext(DbContextOptions<HotelBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<ContraseñaUsuario> ContraseñaUsuarios { get; set; } = null!;
        public virtual DbSet<DireccionUsuario> DireccionUsuarios { get; set; } = null!;
        public virtual DbSet<Habitacion> Habitacions { get; set; } = null!;
        public virtual DbSet<Reserva> Reservas { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<Temporadum> Temporada { get; set; } = null!;
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HotelBD;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<ContraseñaUsuario>(entity =>
            {
                entity.ToTable("ContraseñaUsuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(255)
                    .HasColumnName("contraseña");

                entity.Property(e => e.UltimaModi)
                    .HasColumnType("date")
                    .HasColumnName("ultima_modi");
            });

            modelBuilder.Entity<DireccionUsuario>(entity =>
            {
                entity.ToTable("DireccionUsuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodPostal)
                    .HasMaxLength(20)
                    .HasColumnName("cod_postal");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .HasColumnName("direccion");

                entity.Property(e => e.Municipio)
                    .HasMaxLength(100)
                    .HasColumnName("municipio");

                entity.Property(e => e.Pais)
                    .HasMaxLength(100)
                    .HasColumnName("pais");

                entity.Property(e => e.Provincia)
                    .HasMaxLength(100)
                    .HasColumnName("provincia");
            });

            modelBuilder.Entity<Habitacion>(entity =>
            {
                entity.ToTable("Habitacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adaptada).HasColumnName("adaptada");

                entity.Property(e => e.Bloqueada).HasColumnName("bloqueada");

                entity.Property(e => e.Capacidad).HasColumnName("capacidad");

                entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.PrecioBase).HasColumnName("precio_base");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Habitacions)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Habitacio__categ__286302EC");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.ToTable("Reserva");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaFinal)
                    .HasColumnType("date")
                    .HasColumnName("fecha_final");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("fecha_inicio");

                entity.Property(e => e.HabitacionId).HasColumnName("habitacion_id");

                entity.Property(e => e.PrecioTotal).HasColumnName("precio_total");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Habitacion)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.HabitacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reserva__habitac__3A81B327");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reserva__usuario__398D8EEE");

                entity.HasMany(d => d.Servicios)
                    .WithMany(p => p.Reservas)
                    .UsingEntity<Dictionary<string, object>>(
                        "ReservaServicio",
                        l => l.HasOne<Servicio>().WithMany().HasForeignKey("ServicioId").OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK__Reserva_S__servi__3E52440B"),
                        r => r.HasOne<Reserva>().WithMany().HasForeignKey("ReservaId").OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK__Reserva_S__reser__3D5E1FD2"),
                        j =>
                        {
                            j.HasKey("ReservaId", "ServicioId").HasName("PK__Reserva___2BB0DED80D8E2AEC");

                            j.ToTable("Reserva_Servicio");

                            j.IndexerProperty<int>("ReservaId").HasColumnName("reserva_id");

                            j.IndexerProperty<int>("ServicioId").HasColumnName("servicio_id");
                        });
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.ToTable("Servicio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.Property(e => e.PrecioServicio).HasColumnName("precio_servicio");
            });

            modelBuilder.Entity<Temporadum>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("fecha_fin");

                entity.Property(e => e.FechaIni)
                    .HasColumnType("date")
                    .HasColumnName("fecha_ini");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.Property(e => e.Tarifa).HasColumnName("tarifa");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.ToTable("TipoUsuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(255)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Email, "UQ__Usuario__AB6E616490628397")
                    .IsUnique();

                entity.HasIndex(e => e.Dni, "UQ__Usuario__D87608A7C5AE5617")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .HasColumnName("apellidos");

                entity.Property(e => e.ContraseñaId).HasColumnName("contraseña_id");

                entity.Property(e => e.DireccionId).HasColumnName("direccion_id");

                entity.Property(e => e.Dni)
                    .HasMaxLength(20)
                    .HasColumnName("dni");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("date")
                    .HasColumnName("fecha_alta");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("date")
                    .HasColumnName("fecha_baja");

                entity.Property(e => e.FechaNac)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nac");

                entity.Property(e => e.Genero)
                    .HasMaxLength(20)
                    .HasColumnName("genero");

                entity.Property(e => e.Movil).HasColumnName("movil");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono).HasColumnName("telefono");

                entity.Property(e => e.TipoUsuarioId).HasColumnName("tipo_usuario_id");

                entity.HasOne(d => d.Contraseña)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.ContraseñaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__contras__34C8D9D1");

                entity.HasOne(d => d.Direccion)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.DireccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__direcci__33D4B598");

                entity.HasOne(d => d.TipoUsuario)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.TipoUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__tipo_us__32E0915F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
