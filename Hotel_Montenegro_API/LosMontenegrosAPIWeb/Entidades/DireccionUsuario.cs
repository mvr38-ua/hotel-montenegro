using System;
using System.Collections.Generic;

namespace LosMontenegrosAPIWeb.Entidades
{
    public partial class DireccionUsuario
    {
        public DireccionUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Direccion { get; set; } = null!;
        public string CodPostal { get; set; } = null!;
        public string Municipio { get; set; } = null!;
        public string Provincia { get; set; } = null!;
        public string Pais { get; set; } = null!;

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
