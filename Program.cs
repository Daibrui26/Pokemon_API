using Pokemon_API.Controllers;
using Pokemon_API.Repositories;
using Pokemon_API.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PokemonDB");

builder.Services.AddScoped<IHabilidadRepository, HabilidadRepository>();

builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

builder.Services.AddScoped<IHabitatRepository, HabitatRepository>();

builder.Services.AddScoped<IOpinionRepository, OpinionRepository>();

builder.Services.AddScoped<IPokeballRepository, PokeballRepository>();
builder.Services.AddScoped<IObjetoRepository, ObjetoRepository>();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokeballService, PokeballService>();
builder.Services.AddScoped<IObjetoService, ObjetoService>();
builder.Services.AddScoped<IHabilidadService, HabilidadService>();
builder.Services.AddScoped<IHabitatService, HabitatService>();
builder.Services.AddScoped<IOpinionService, OpinionService>();



// Add services to the container.

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



//HabilidadController.InicializarDatos();
app.Run();
