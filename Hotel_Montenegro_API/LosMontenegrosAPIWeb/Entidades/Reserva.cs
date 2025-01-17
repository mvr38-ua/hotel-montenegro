using System;
using System.Collections.Generic;

namespace LosMontenegrosAPIWeb.Entidades
{
    public partial class Reserva
    {
        public Reserva()
        {
            Servicios = new HashSet<Servicio>();
        }

        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public double PrecioTotal { get; set; }
        public int UsuarioId { get; set; }
        public int HabitacionId { get; set; }

        public virtual Habitacion? Habitacion { get; set; } = null!;
        public virtual Usuario? Usuario { get; set; } = null!;

        public virtual ICollection<Servicio>? Servicios { get; set; }
    }
}
