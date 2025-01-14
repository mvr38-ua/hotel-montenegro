using System;
using System.Collections.Generic;

namespace LosMontenegrosAPIWeb.Entidades
{
    public partial class Usuario
    {
        public Usuario()
        {
            Reservas = new HashSet<Reserva>();
        }

        public int Id { get; set; }
        public string? Email { get; set; } = null!;
        public string? Nombre { get; set; } = null!;
        public string? Apellidos { get; set; } = null!;
        public string? Dni { get; set; } = null!;
        public long? Movil { get; set; }
        public long? Telefono { get; set; }
        public string? Genero { get; set; }
        public DateTime? FechaNac { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public int? TipoUsuarioId { get; set; }
        public int? DireccionId { get; set; }
        public int? ContraseñaId { get; set; }

        public virtual ContraseñaUsuario? Contraseña { get; set; } = null!;
        public virtual DireccionUsuario? Direccion { get; set; } = null!;
        public virtual TipoUsuario? TipoUsuario { get; set; } = null!;
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
