public class RegisterModel
{
    public string Email { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Dni { get; set; }
    public int Movil { get; set; }
    public int? Telefono { get; set; }
    public string Genero { get; set; }
    public DateTime FechaNac { get; set; }
    public int TipoUsuarioId { get; set; }   // Asegúrate de tener este campo
    public int DireccionId { get; set; }     // Asegúrate de tener este campo
    public string Password { get; set; }
}
