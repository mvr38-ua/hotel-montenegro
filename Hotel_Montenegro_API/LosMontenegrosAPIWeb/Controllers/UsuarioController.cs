using Microsoft.AspNetCore.Mvc;
using LosMontenegrosAPIWeb.Repositories;
using LosMontenegrosAPIWeb.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace LosMontenegrosAPIWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly HotelBDContext _hotelBDContext;
        private readonly IConfiguration _configuration;

        public UsuarioController(UsuarioRepository usuarioRepository, HotelBDContext hotelBDContext, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _hotelBDContext = hotelBDContext;
            _configuration = configuration;
        }

        // POST: api/Usuario/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            // Buscar al usuario por el correo electrónico
            var usuario = await _hotelBDContext.Usuarios
                .Include(u => u.Contraseña) // Asegúrate de incluir la relación de Contraseña
                .FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if (usuario == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verificar si la contraseña está presente (en caso de que la relación no haya sido cargada correctamente)
            if (usuario.Contraseña == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verificar la contraseña usando BCrypt
            bool passwordValid = BCrypt.Net.BCrypt.Verify(loginModel.Password, usuario.Contraseña.Contraseña);

            if (!passwordValid)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generación del token JWT
            var token = GenerateJwtToken(usuario);

            return Ok(new { token });
        }

        // POST: api/Usuario/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            // Verificar si el email ya está registrado
            if (await _hotelBDContext.Usuarios.AnyAsync(u => u.Email == registerModel.Email))
            {
                return BadRequest("Email is already registered.");
            }

            // Crear el hash de la contraseña
            var contraseñaHash = BCrypt.Net.BCrypt.HashPassword(registerModel.Password);
            var contraseña = new ContraseñaUsuario
            {
                Contraseña = contraseñaHash,
                UltimaModi = DateTime.Now // Puedes asignar la fecha actual como fecha de la última modificación
            };

            // Añadir la contraseña a la base de datos
            await _hotelBDContext.ContraseñaUsuarios.AddAsync(contraseña);
            await _hotelBDContext.SaveChangesAsync();  // Guarda para obtener el ID de la contraseña

            // Verificar si el TipoUsuario existe
            var tipoUsuario = await _hotelBDContext.TipoUsuarios.FirstOrDefaultAsync(t => t.Id == registerModel.TipoUsuarioId);
            if (tipoUsuario == null)
            {
                return BadRequest("Tipo de usuario no válido.");
            }

            // Verificar si la Dirección existe
            var direccion = await _hotelBDContext.DireccionUsuarios.FirstOrDefaultAsync(d => d.Id == registerModel.DireccionId);
            if (direccion == null)
            {
                return BadRequest("Dirección no válida.");
            }

            // Crear el nuevo usuario
            var usuario = new Usuario
            {
                Email = registerModel.Email,
                Nombre = registerModel.Nombre,
                Apellidos = registerModel.Apellidos,
                Dni = registerModel.Dni,
                Movil = registerModel.Movil,
                Telefono = registerModel.Telefono,
                Genero = registerModel.Genero,
                FechaNac = registerModel.FechaNac,
                FechaAlta = DateTime.Now,
                TipoUsuarioId = tipoUsuario.Id,   // Asegúrate de usar el id del tipo de usuario válido
                DireccionId = direccion.Id,       // Asegúrate de usar el id de la dirección válida
                Activo = true,
                ContraseñaId = contraseña.Id     // Asocia el id de la contraseña
            };

            // Añadir el usuario a la base de datos
            await _hotelBDContext.Usuarios.AddAsync(usuario);
            await _hotelBDContext.SaveChangesAsync(); // Guarda el nuevo usuario en la base de datos

            // Devuelve una respuesta con el usuario creado
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
        
        }

        // POST: api/Usuario/logout
        [HttpPost("logout")]
        [Authorize] // Solo usuarios autenticados pueden cerrar sesión
        public IActionResult Logout()
        {
            // El token JWT es stateless, por lo que no necesitas invalidarlo explícitamente.
            return Ok(new { message = "Successfully logged out." });
        }

        // GET: api/Usuario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        // PUT: api/Usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuarioUpdateModel)
        {
            // Buscar el usuario existente
            var usuarioExistente = await _hotelBDContext.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound($"No se encontró un usuario con el ID {id}.");
            }

            // Actualizar solo los campos proporcionados
            if (!string.IsNullOrEmpty(usuarioUpdateModel.Nombre))
            {
                usuarioExistente.Nombre = usuarioUpdateModel.Nombre;
            }

            if (!string.IsNullOrEmpty(usuarioUpdateModel.Apellidos))
            {
                usuarioExistente.Apellidos = usuarioUpdateModel.Apellidos;
            }

            if (!string.IsNullOrEmpty(usuarioUpdateModel.Email))
            {
                usuarioExistente.Email = usuarioUpdateModel.Email;
            }

            if (usuarioUpdateModel.Movil.HasValue)
            {
                usuarioExistente.Movil = usuarioUpdateModel.Movil.Value;
            }

            if (usuarioUpdateModel.Telefono.HasValue)
            {
                usuarioExistente.Telefono = usuarioUpdateModel.Telefono.Value;
            }

            if (!string.IsNullOrEmpty(usuarioUpdateModel.Genero))
            {
                usuarioExistente.Genero = usuarioUpdateModel.Genero;
            }

            if (usuarioUpdateModel.FechaNac.HasValue)
            {
                usuarioExistente.FechaNac = usuarioUpdateModel.FechaNac.Value;
            }

            if (usuarioUpdateModel.TipoUsuarioId.HasValue)
            {
                usuarioExistente.TipoUsuarioId = usuarioUpdateModel.TipoUsuarioId.Value;
            }

            if (usuarioUpdateModel.DireccionId.HasValue)
            {
                usuarioExistente.DireccionId = usuarioUpdateModel.DireccionId.Value;
            }

            if (usuarioUpdateModel.ContraseñaId.HasValue)
            {
                usuarioExistente.ContraseñaId = usuarioUpdateModel.ContraseñaId.Value;
            }

            if (usuarioUpdateModel.Activo.HasValue)
            {
                usuarioExistente.Activo = usuarioUpdateModel.Activo.Value;
            }

            if (usuarioUpdateModel.FechaBaja.HasValue)
            {
                usuarioExistente.FechaBaja = usuarioUpdateModel.FechaBaja.Value;
            }

            // Guardar los cambios
            await _hotelBDContext.SaveChangesAsync();

            return Ok(usuarioExistente);
        }

        // DELETE: api/Usuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.DeleteUsuarioAsync(id);
            return NoContent();
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Name, usuario.Email),
        new Claim("TipoUsuario", usuario.TipoUsuarioId.ToString()),  // Puedes agregar más datos aquí
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("clave_super_secreta_muy_larga_que_asegura_256_bits_1234567890123456"));  // Cambia por tu propia clave secreta
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "tu_dominio.com",
                audience: "tu_dominio.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
