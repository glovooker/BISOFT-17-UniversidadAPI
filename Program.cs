using UniversidadAPI.Modelos;
using UniversidadAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<UniversidadDatabaseSettings>(
    builder.Configuration.GetSection("UniversidadDatabase"));

builder.Services.AddSingleton<AlumnosService>();
builder.Services.AddSingleton<CarrerasService>();
builder.Services.AddSingleton<UsuariosService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
