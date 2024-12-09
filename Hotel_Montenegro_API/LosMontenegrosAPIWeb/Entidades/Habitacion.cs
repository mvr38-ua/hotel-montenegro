using System;
using System.Collections.Generic;

namespace LosMontenegrosAPIWeb.Entidades
{
    public partial class Habitacion
    {
        public Habitacion()
        {
            Reservas = new HashSet<Reserva>();
        }

        public int Id { get; set; }
        public int Numero { get; set; }
        public double PrecioBase { get; set; }
        public int Capacidad { get; set; }
        public bool Bloqueada { get; set; }
        public bool Adaptada { get; set; }
        public int CategoriaId { get; set; }

        public virtual Categorium Categoria { get; set; } = null!;
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
