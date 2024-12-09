using System;
using System.Collections.Generic;

namespace LosMontenegrosAPIWeb.Entidades
{
    public partial class ContraseñaUsuario
    {
        public ContraseñaUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Contraseña { get; set; } = null!;
        public DateTime UltimaModi { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
