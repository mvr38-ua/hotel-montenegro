using System;
using System.Collections.Generic;

namespace LosMontenegrosAPIWeb.Entidades
{
    public partial class Servicio
    {
        public Servicio()
        {
            Reservas = new HashSet<Reserva>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public double PrecioServicio { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
