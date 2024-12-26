using Microsoft.EntityFrameworkCore;
using LosMontenegrosAPIWeb.Entidades;
using LosMontenegrosAPIWeb.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregar el contexto de base de datos
builder.Services.AddDbContext<HotelBDContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el repositorio
builder.Services.AddScoped<TemporadaRepository>();
builder.Services.AddScoped<CategoriaRepository>();


// Agregar controladores
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
