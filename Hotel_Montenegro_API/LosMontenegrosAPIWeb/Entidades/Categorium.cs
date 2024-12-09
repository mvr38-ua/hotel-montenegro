using System;
using System.Collections.Generic;

namespace LosMontenegrosAPIWeb.Entidades
{
    public partial class Categorium
    {
        public Categorium()
        {
            Habitacions = new HashSet<Habitacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Habitacion> Habitacions { get; set; }
    }
}
