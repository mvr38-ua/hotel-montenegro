namespace LosMontenegrosAPIWeb.Entidades
{
    public class FiltroHabitaciones
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? CapacidadMinima { get; set; }
        public int? CategoriaId { get; set; }
    }
}
