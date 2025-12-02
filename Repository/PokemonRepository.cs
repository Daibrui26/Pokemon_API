

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
        private string? connectionString;

        public PokemonRepository(string connectionString, IHabilidadRepository IHabilidadRepository, IPokeballRepository IPokeballRepository, IHabitatRepository IHabitatRepository)
        {
            _connectionString = connectionString;
            _IHabilidadRepository = IHabilidadRepository;
            _IPokeballRepository = IPokeballRepository;
            _IHabitatRepository = IHabitatRepository;

        }

        public PokemonRepository(string? connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<List<Pokemon>> GetAllAsync()
        {
            var Pokemons = new List<Pokemon>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Region, Peso, Shiny, Tipo, idHabilidad, idHabitat, idPokeball FROM Pokemon";
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

                string query = "SELECT Id, Nombre, Region, Peso, Shiny, Tipo, idHabilidad, idHabitat, idPokeball FROM Pokemon WHERE Id = @Id";
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

                string query = "INSERT INTO Pokemon (Nombre, Region, Peso, Shiny, Tipo, idHabilidad, idHabitat, idPokeball) VALUES (@Nombre, @Region, @Peso, @Shiny, @Tipo, @idHabilidad, @idHabitat, @idPokeball)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", Pokemon.Nombre);
                    command.Parameters.AddWithValue("@Region", Pokemon.Region);
                    command.Parameters.AddWithValue("@Peso", Pokemon.Peso);
                    command.Parameters.AddWithValue("@Shiny", Pokemon.Shiny);
                    command.Parameters.AddWithValue("@Tipo", Pokemon.Tipo);
                    command.Parameters.AddWithValue("@idHabilidad", Pokemon.Habilidad.Id);
                    command.Parameters.AddWithValue("@idHabitat", Pokemon.Habitat.Id);
                    command.Parameters.AddWithValue("@idPokeball", Pokemon.Pokeball.Id);
                   
                   

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Pokemon Pokemon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Pokemons SET Nombre = @Nombre, Region = @Region, Peso = @Peso, Shiny = @Shiny, Tipo = @Tipo, idHabilidad = @idHabilidad, idHabitat = @idHabitat, idPokeball = @idPokeball WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", Pokemon.Nombre);
                    command.Parameters.AddWithValue("@Region", Pokemon.Region);
                    command.Parameters.AddWithValue("@Peso", Pokemon.Peso);
                    command.Parameters.AddWithValue("@Shiny", Pokemon.Shiny);
                    command.Parameters.AddWithValue("@Tipo", Pokemon.Tipo);
                    command.Parameters.AddWithValue("@idHabilidad", Pokemon.Habilidad.Id);
                    command.Parameters.AddWithValue("@idHabitat", Pokemon.Habitat.Id);
                    command.Parameters.AddWithValue("@idPokeball", Pokemon.Pokeball.Id);
                   

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
                    INSERT INTO Pokemon (Nombre, Region, Peso, Shiny, Tipo, idHabilidad, idHabitat, idPokeball)
                    VALUES 
                    (@Nombre1, @Region1, @Peso1, @Shiny1, @Tipo1, @idHabilidad1, @idHabitat1, @idPokeball1),
                    (@Nombre2, @Region2, @Peso2, @Shiny2, @Tipo2, @idHabilidad2, @idHabitat2, @idPokeball2)";
                 
                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer plato
                    command.Parameters.AddWithValue("@Nombre1", "Pikachu");
                    command.Parameters.AddWithValue("@Region1", "Kanto");
                    command.Parameters.AddWithValue("@Peso1", 1.23);
                    command.Parameters.AddWithValue("@Shiny1", 0);
                    command.Parameters.AddWithValue("@Tipo1", "Electrico");
                    command.Parameters.AddWithValue("@idHabilidad1", 1);
                    command.Parameters.AddWithValue("@idHabitat1", 1);
                    command.Parameters.AddWithValue("@idPokeball1", 1);
                    

                    // Parámetros para el segundo plato
                    command.Parameters.AddWithValue("@Nombre2", "Charizard");
                    command.Parameters.AddWithValue("@Region2", "Kanto");
                    command.Parameters.AddWithValue("@Peso2", 1.45);
                    command.Parameters.AddWithValue("@Shiny2", 1);
                    command.Parameters.AddWithValue("@Tipo2", "Fuego, Volador");
                    command.Parameters.AddWithValue("@idHabilidad2", 2);
                    command.Parameters.AddWithValue("@idHabitat2", 2);
                    command.Parameters.AddWithValue("@idPokeball2", 2);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}