using UniversidadAPI.Modelos;
using UniversidadAPI.Servicios;
using UniversidadAPI.UniversidadDatabaseSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<MongoDBInstance>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new MongoDBInstance(configuration);
});

builder.Services.AddSingleton<AlumnosService>();
builder.Services.AddSingleton<CarrerasService>();
builder.Services.AddSingleton<CiclosService>();
builder.Services.AddSingleton<CursosService>();
builder.Services.AddSingleton<GruposService>();
builder.Services.AddSingleton<ProfesoresService>();
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
