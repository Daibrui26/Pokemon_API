using Pokemon_API.Controllers;
using Pokemon_API.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PokemonDB");

builder.Services.AddScoped<IHabilidadRepository, HabilidadRepository>(provider =>
new HabilidadRepository(connectionString));

builder.Services.AddScoped<IPokemonRepository, PokemonRepository>(provider =>
new PokemonRepository(connectionString));

builder.Services.AddScoped<IHabitatRepository, HabitatRepository>(provider =>
new HabitatRepository(connectionString));

builder.Services.AddScoped<IPokeballRepository, PokeballRepository>(provider =>
new PokeballRepository(connectionString));

builder.Services.AddScoped<IObjetoRepository, ObjetoRepository>(provider =>
new ObjetoRepository(connectionString));


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
