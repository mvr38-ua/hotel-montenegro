using Microsoft.EntityFrameworkCore;
using LosMontenegrosAPIWeb.Entidades;
using LosMontenegrosAPIWeb.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

// Configuración del contexto de base de datos
builder.Services.AddDbContext<HotelBDContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar los repositorios en el contenedor de dependencias
builder.Services.AddScoped<TemporadaRepository>();
builder.Services.AddScoped<DireccionUsuarioRepository>();
builder.Services.AddScoped<ServicioRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<ContraseñaUsuarioRepository>();
builder.Services.AddScoped<HabitacionRepository>();
builder.Services.AddScoped<TipoUsuarioRepository>();
builder.Services.AddScoped<ReservaRepository>();

// Configuración de controladores y serialización JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configuración para manejar referencias circulares en objetos complejos
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Configuración de Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar redirección de HTTP a HTTPS
app.UseHttpsRedirection();

// Usar autenticación y autorización en la aplicación
app.UseAuthentication();
app.UseAuthorization();

// Mapear los controladores a las rutas
app.MapControllers();

app.Run();
