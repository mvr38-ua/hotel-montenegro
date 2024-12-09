using System;
using System.Collections.Generic;

namespace LosMontenegrosAPIWeb.Entidades
{
    public partial class Temporadum
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public double Tarifa { get; set; }
    }
}
