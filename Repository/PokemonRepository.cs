

using Microsoft.Data.SqlClient;
using Models;

namespace Pokemon_API.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly string _connectionString;
        private readonly  IHabilidadRepository _IHabilidadRepository;
        private readonly  IPokeballRepository _IPokeballRepository;
        private readonly IHabitatRepository _IHabitatRepository;
        private readonly IObjetoRepository _IObjetoRepository;
        
        //private string? connectionString;

        public PokemonRepository(IConfiguration configuration, IHabilidadRepository IHabilidadRepository, IPokeballRepository IPokeballRepository, IHabitatRepository IHabitatRepository, IObjetoRepository IObjetoRepository)
        {
            _connectionString = configuration.GetConnectionString("PokemonDB") ?? "Not found";
            _IHabilidadRepository = IHabilidadRepository;
            _IPokeballRepository = IPokeballRepository;
            _IHabitatRepository = IHabitatRepository;
            _IObjetoRepository = IObjetoRepository;
            


        }
        public async Task<List<Pokemon>> GetAllAsync()
        {
            var Pokemons = new List<Pokemon>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Region, Peso, Shiny, Tipo, Habilidad, Habitat, Pokeball, Objeto FROM Pokemon";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {

                            var Pokemon = new Pokemon
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Region = reader.GetString(2),
                                Peso = reader.GetDouble(3),
                                Shiny = reader.GetBoolean(4),
                                Tipo = reader.GetString(5),
                                Habilidad = await _IHabilidadRepository.GetByIdAsync(reader.GetInt32(6)),
                                Habitat = await _IHabitatRepository.GetByIdAsync(reader.GetInt32(7)),
                                Pokeball = await _IPokeballRepository.GetByIdAsync(reader.GetInt32(8)),
                                Objeto = await _IObjetoRepository.GetByIdAsync(reader.GetInt32(9))
                                
                            }; 

                            Pokemons.Add(Pokemon);
                        }
                    }
                }
            }
            return Pokemons;
        }

        public async Task<List<Pokemon>> GetAllFilteredAsync(string? Nombre, string? Tipo, string? orderBy, bool ascending)
        {
            var Pokemons = new List<Pokemon>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Region, Nombre, Peso, Shiny, Tipo, Habilidad, Pokeball, Habitat, Objeto, Reseña FROM Pokemon WHERE 1=1";
                var parameters = new List<SqlParameter>();

                // Filtros
                if (!string.IsNullOrWhiteSpace(Tipo))
                {
                    query += " AND Tipo = @Tipo";
                    parameters.Add(new SqlParameter("@Tipo", Tipo));
                }

                if (!string.IsNullOrWhiteSpace(Nombre))
                {
                    query += " AND Nombre = @Nombre";
                    parameters.Add(new SqlParameter("@Nombre", Nombre));
                }

                // Ordenación
                if (!string.IsNullOrWhiteSpace(orderBy))
                {
                    var validColumns = new[] { "region", "nombre", "peso", "shiny", "tipo", "habilidad", "pokeball", "habitat", "objeto" };
                    var orderByLower = orderBy.ToLower();
                    
                    if (validColumns.Contains(orderByLower))
                    {
                        var direction = ascending ? "ASC" : "DESC";
                        query += $" ORDER BY {orderByLower} {direction}";
                    }else
                {
                    query += " ORDER BY nombre ASC";
                }
                }
                

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Pokemon = new Pokemon
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Region = reader.GetString(2),
                                Peso = reader.GetDouble(3),
                                Shiny = reader.GetBoolean(4),
                                Tipo = reader.GetString(5),
                                Habilidad = await _IHabilidadRepository.GetByIdAsync(reader.GetInt32(6)),
                                Habitat = await _IHabitatRepository.GetByIdAsync(reader.GetInt32(7)),
                                Pokeball = await _IPokeballRepository.GetByIdAsync(reader.GetInt32(8)),
                                Objeto = await _IObjetoRepository.GetByIdAsync(reader.GetInt32(9)),
                                
                   
                            };

                            Pokemons.Add(Pokemon);
                        }
                    }
                }
            }
            return Pokemons;
        }

        public async Task<Pokemon> GetByIdAsync(int id)
        {
            Pokemon Pokemon = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Region, Peso, Shiny, Tipo, Habilidad, Habitat, Pokeball, Objeto  FROM Pokemon WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Pokemon = new Pokemon
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Region = reader.GetString(2),
                                Peso = reader.GetDouble(3),
                                Shiny = reader.GetBoolean(4),
                                Tipo = reader.GetString(5),
                                Habilidad = await _IHabilidadRepository.GetByIdAsync(reader.GetInt32(6)),
                                Habitat = await _IHabitatRepository.GetByIdAsync(reader.GetInt32(7)),
                                Pokeball = await _IPokeballRepository.GetByIdAsync(reader.GetInt32(8)),
                                Objeto = await _IObjetoRepository.GetByIdAsync(reader.GetInt32(9)),
                               
                   
                               
                            };
                        }
                    }
                }
            }
            return Pokemon;
        }

        public async Task AddAsync(Pokemon Pokemon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Pokemon (Nombre, Region, Peso, Shiny, Tipo, Habilidad, Habitat, Pokeball, Objeto) VALUES (@Nombre, @Region, @Peso, @Shiny, @Tipo, @Habilidad, @Habitat, @Pokeball, @Objeto)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", Pokemon.Nombre);
                    command.Parameters.AddWithValue("@Region", Pokemon.Region);
                    command.Parameters.AddWithValue("@Peso", Pokemon.Peso);
                    command.Parameters.AddWithValue("@Shiny", Pokemon.Shiny);
                    command.Parameters.AddWithValue("@Tipo", Pokemon.Tipo);
                    command.Parameters.AddWithValue("@Habilidad", Pokemon.Habilidad.Id);
                    command.Parameters.AddWithValue("@Habitat", Pokemon.Habitat.Id);
                    command.Parameters.AddWithValue("@Pokeball", Pokemon.Pokeball.Id);
                    command.Parameters.AddWithValue("@Objeto", Pokemon.Objeto.Id);
                
                   
                   

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Pokemon Pokemon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Pokemons SET Nombre = @Nombre, Region = @Region, Peso = @Peso, Shiny = @Shiny, Tipo = @Tipo, Habilidad = @Habilidad, Habitat = @Habitat, Pokeball = @Pokeball, Objeto = @Objeto WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", Pokemon.Nombre);
                    command.Parameters.AddWithValue("@Region", Pokemon.Region);
                    command.Parameters.AddWithValue("@Peso", Pokemon.Peso);
                    command.Parameters.AddWithValue("@Shiny", Pokemon.Shiny);
                    command.Parameters.AddWithValue("@Tipo", Pokemon.Tipo);
                    command.Parameters.AddWithValue("@Habilidad", Pokemon.Habilidad.Id);
                    command.Parameters.AddWithValue("@Habitat", Pokemon.Habitat.Id);
                    command.Parameters.AddWithValue("@Pokeball", Pokemon.Pokeball.Id);
                    command.Parameters.AddWithValue("@Objeto", Pokemon.Objeto.Id);
                    

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Pokemon WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Comando SQL para insertar datos iniciales
                var query = @"
                    INSERT INTO Pokemon (Nombre, Region, Peso, Shiny, Tipo, Habilidad, Habitat, Pokeball, Objeto)
                    VALUES 
                    (@Nombre1, @Region1, @Peso1, @Shiny1, @Tipo1, @Habilidad1, @Habitat1, @Pokeball1, @Objeto1),
                    (@Nombre2, @Region2, @Peso2, @Shiny2, @Tipo2, @Habilidad2, @Habitat2, @Pokeball2, @Objeto2)";
                 
                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer plato
                    command.Parameters.AddWithValue("@Nombre1", "Pikachu");
                    command.Parameters.AddWithValue("@Region1", "Kanto");
                    command.Parameters.AddWithValue("@Peso1", 1.23);
                    command.Parameters.AddWithValue("@Shiny1", 0);
                    command.Parameters.AddWithValue("@Tipo1", "Electrico");
                    command.Parameters.AddWithValue("@Habilidad1", 1);
                    command.Parameters.AddWithValue("@Habitat1", 1);
                    command.Parameters.AddWithValue("@Pokeball1", 1);
                    command.Parameters.AddWithValue("@Objeto1", 1);
                    
                    

                    // Parámetros para el segundo plato
                    command.Parameters.AddWithValue("@Nombre2", "Charizard");
                    command.Parameters.AddWithValue("@Region2", "Kanto");
                    command.Parameters.AddWithValue("@Peso2", 1.45);
                    command.Parameters.AddWithValue("@Shiny2", 1);
                    command.Parameters.AddWithValue("@Tipo2", "Volador/Fuego");
                    command.Parameters.AddWithValue("@Habilidad2", 2);
                    command.Parameters.AddWithValue("@Habitat2", 2);
                    command.Parameters.AddWithValue("@Pokeball2", 2);
                    command.Parameters.AddWithValue("@Objeto2", 2);
                    

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}