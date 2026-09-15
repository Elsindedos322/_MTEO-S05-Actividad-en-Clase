using Microsoft.EntityFrameworkCore;
using Caso04actividadclase.interfaces;
using Caso04actividadclase.Repositories;
using Caso04actividadclase.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Registrar DbContext con PostgreSQL / Supabase
builder.Services.AddDbContext<AgenciaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AgenciaDB")));

// 3. Registrar Repository Pattern y Unit of Work
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// 4. Configurar la canalización de solicitudes HTTP (Pipeline)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();